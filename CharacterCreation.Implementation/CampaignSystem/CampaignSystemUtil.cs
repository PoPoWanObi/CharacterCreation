using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CharacterCreation.Common.CampaignSystem;
using CharacterCreation.Common.Settings;
using CharacterCreation.Common.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.Implementation.CampaignSystem;

public static class CampaignSystemUtil
{
    private static readonly Action<BasicCharacterObject, MBBodyProperty> SetCharacterBodyPropertyRange;
    
    static CampaignSystemUtil()
    {
        var param1 = Expression.Parameter(typeof(BasicCharacterObject));
        var param2 = Expression.Parameter(typeof(MBBodyProperty));
        SetCharacterBodyPropertyRange = Expression.Lambda<Action<BasicCharacterObject, MBBodyProperty>>(
            Expression.Call(
                param1,
                AccessTools.PropertySetter(typeof(BasicCharacterObject), nameof(BasicCharacterObject.BodyPropertyRange)),
                param2
            ),
            param1,
            param2
        ).Compile();
    }
    
    public static CharacterCreationCampaignBehavior.DelegateRef GetDelegateRefs() => new(
        OnGameStart,
        SetBodyPropertiesOverride,
        SetTagOverride,
        UndoBodyPropertiesOverride,
        SetUnitNameOverride,
        UndoUnitNameOverride
    );
    
        
    private static void ApplyBodyPropertyOverride(CharacterObject character, in UnitBodyPropertiesOverride property, bool isMax = false)
    {
        var bodyProperty = MBBodyProperty.CreateFrom(character.BodyPropertyRange);
        SetCharacterBodyPropertyRange(character, bodyProperty);
        if (isMax) bodyProperty.Init(bodyProperty.BodyPropertyMin, property.BodyProperties);
        else bodyProperty.Init(property.BodyProperties, bodyProperty.BodyPropertyMax);
        character.Race = property.Race;
        character.IsFemale = property.IsFemale;
        if (DccSettings.Instance!.DebugMode)
        {
            var msg =
                $"[CharacterCreation] {character.Name} body properties overridden: {character.GetBodyProperties(character.Equipment).ToString()}, min {character.GetBodyPropertiesMin().ToString()}, max {character.GetBodyPropertiesMax().ToString()}";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
        }
    }

    private static void ApplyTagOverride(CharacterObject character, in UnitTagOverride tagOverride)
    {
        var bodyProperty = MBBodyProperty.CreateFrom(character.BodyPropertyRange);
        if (DccSettings.Instance!.DebugMode)
        {
            var msg =
                $"[CharacterCreation] overriding {character.Name} tags: hair {bodyProperty.HairTags}, beard {bodyProperty.BeardTags}, tattoo {bodyProperty.TattooTags}";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
        }
            
        SetCharacterBodyPropertyRange(character, bodyProperty);
        if (tagOverride.HairTags != null) bodyProperty.HairTags = tagOverride.HairTags;
        if (tagOverride.BeardTags != null) bodyProperty.BeardTags = tagOverride.BeardTags;
        if (tagOverride.TattooTags != null) bodyProperty.TattooTags = tagOverride.TattooTags;
            
        if (DccSettings.Instance.DebugMode)
        {
            var msg =
                $"[CharacterCreation] {character.Name} tags overridden: hair {bodyProperty.HairTags}, beard {bodyProperty.BeardTags}, tattoo {bodyProperty.TattooTags}";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
        }
    }

    private static void StoreBaseVersionOnly(CharacterCreationCampaignBehavior behavior, CharacterObject unit)
    {
        ref readonly var troopBaseVersion = ref behavior.TroopBaseVersion;
        if (troopBaseVersion.ContainsKey(unit.StringId)) return;
        troopBaseVersion[unit.StringId] = new UnitBodyPropertiesBase(unit.StringId, unit.BodyPropertyRange, unit.Race, unit.IsFemale);
    }
    
    private static void OnGameStart(CharacterCreationCampaignBehavior behavior, CampaignGameStarter gameStarter)
    {
        ref readonly var bodyPropertiesOverrideMin = ref behavior.BodyPropertiesOverrideMin;
        ref readonly var bodyPropertiesOverrideMax = ref behavior.BodyPropertiesOverrideMax;
        ref readonly var tagOverrides = ref behavior.TagOverrides;
        ref readonly var troopNameOverride = ref behavior.TroopNameOverride;
        
        // apply any min overrides
        Parallel.ForEach(bodyPropertiesOverrideMin, kv =>
        {
            try
            {
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                if (charObj != null)
                {
                    StoreBaseVersionOnly(behavior, charObj);
                    ApplyBodyPropertyOverride(charObj, kv.Value);
                }
            }
            catch (Exception e)
            {
                var msg = "[CharacterCreation] " + e;
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                Debug.Print(msg);
            }
        });
        // apply any max overrides
        Parallel.ForEach(bodyPropertiesOverrideMax, kv =>
        {
            try
            {
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                if (charObj != null)
                {
                    StoreBaseVersionOnly(behavior, charObj);
                    ApplyBodyPropertyOverride(charObj, kv.Value, true);
                }
            }
            catch (Exception e)
            {
                var msg = "[CharacterCreation] " + e;
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                Debug.Print(msg);
            }
        });
        // apply any tag overrides
        Parallel.ForEach(tagOverrides, kv =>
        {
            try
            {
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                if (charObj != null)
                {
                    StoreBaseVersionOnly(behavior, charObj);
                    ApplyTagOverride(charObj, kv.Value);
                }
            }
            catch (Exception e)
            {
                var msg = "[CharacterCreation] " + e;
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                Debug.Print(msg);
            }
        });
        // apply any name overrides
        Parallel.ForEach(troopNameOverride, kv =>
        {
            try
            {
                ref readonly var troopBaseName = ref behavior.TroopBaseName;
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                if (charObj != null)
                {
                    troopBaseName[charObj.StringId ?? kv.Value.UnitId] = charObj.Name;
                    charObj.UpdateName(new TextObject(kv.Value.UnitName));
                }
            }
            catch (Exception e)
            {
                var msg = "[CharacterCreation] " + e;
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                Debug.Print(msg);
            }
        });
    }
    
    public static void SetBodyPropertiesOverride(
        CharacterCreationCampaignBehavior.BehaviorActionData data,
        BodyProperties properties, int race,
        bool isFemale,
        BodyPropertyType editPropertyType
    )
    {
        ref readonly var behavior = ref data.Behavior;
        ref readonly var bodyPropertiesOverrideMax = ref behavior.BodyPropertiesOverrideMax;
        ref readonly var bodyPropertiesOverrideMin = ref behavior.BodyPropertiesOverrideMin;
        ref readonly var unit = ref data.Character;
        
        if (unit.IsHero) return;

        // save the base version and override
        StoreBaseVersionOnly(behavior, unit);
        var propertiesOverride = new UnitBodyPropertiesOverride(unit.StringId, properties, race, isFemale);

        if ((editPropertyType & BodyPropertyType.MinProperties) == BodyPropertyType.MinProperties)
        {
            bodyPropertiesOverrideMax[unit.StringId] = propertiesOverride;
            ApplyBodyPropertyOverride(unit, propertiesOverride, true);
        }
        if ((editPropertyType & BodyPropertyType.MaxProperties) == BodyPropertyType.MaxProperties)
        {
            bodyPropertiesOverrideMin[unit.StringId] = propertiesOverride;
            ApplyBodyPropertyOverride(unit, propertiesOverride);
        }
    }

    public static void SetTagOverride(
        CharacterCreationCampaignBehavior.BehaviorActionData data,
        string? hairTags = null,
        string? beardTags = null,
        string? tattooTags = null
    )
    {
        ref readonly var behavior = ref data.Behavior;
        ref readonly var tagOverrides = ref behavior.TagOverrides;
        ref readonly var unit = ref data.Character;
        
        if (unit.IsHero) return;
            
        // save the base version and override
        StoreBaseVersionOnly(behavior, unit);
        if (!tagOverrides.TryGetValue(unit.StringId, out var tagOverride))
        {
            tagOverride = new UnitTagOverride(unit.StringId);
            tagOverrides[unit.StringId] = tagOverride;
        }
            
        if (hairTags != null) tagOverride.HairTags = hairTags;
        if (beardTags != null) tagOverride.BeardTags = beardTags;
        if (tattooTags != null) tagOverride.TattooTags = tattooTags;
        ApplyTagOverride(unit, tagOverride);
    }

    public static void UndoBodyPropertiesOverride(CharacterCreationCampaignBehavior.BehaviorActionData data)
    {
        ref readonly var behavior = ref data.Behavior;
        ref readonly var bodyPropertiesOverrideMax = ref behavior.BodyPropertiesOverrideMax;
        ref readonly var bodyPropertiesOverrideMin = ref behavior.BodyPropertiesOverrideMin;
        ref readonly var troopBaseVersion = ref behavior.TroopBaseVersion;
        ref readonly var unit = ref data.Character;
        
        if (!unit.IsHero && troopBaseVersion.TryGetValue(unit.StringId, out var original))
        {
            SetCharacterBodyPropertyRange(unit, original.BodyPropertyRange);
            unit.Race = original.Race;
            unit.IsFemale = original.IsFemale;
            bodyPropertiesOverrideMin.Remove(unit.StringId);
            bodyPropertiesOverrideMax.Remove(unit.StringId);
            troopBaseVersion.Remove(unit.StringId);
        }
        else
        {
            var msg = $"[CharacterCreation] Unit {unit.Name} is not previously overridden.";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
        }
    }

    public static void SetUnitNameOverride(CharacterCreationCampaignBehavior.BehaviorActionData data, string name)
    {
        ref readonly var behavior = ref data.Behavior;
        ref readonly var troopNameOverride = ref behavior.TroopNameOverride;
        ref readonly var troopBaseName = ref behavior.TroopBaseName;
        ref readonly var unit = ref data.Character;
        
        if (unit.IsHero) return;

        if (!troopBaseName.ContainsKey(unit.StringId))
            troopBaseName[unit.StringId] = unit.Name;
        troopNameOverride[unit.StringId] = new TroopNameOverride(unit.StringId, name);

        unit.UpdateName(new TextObject(name));
    }

    public static void UndoUnitNameOverride(CharacterCreationCampaignBehavior.BehaviorActionData data)
    {
        ref readonly var behavior = ref data.Behavior;
        ref readonly var troopNameOverride = ref behavior.TroopNameOverride;
        ref readonly var troopBaseName = ref behavior.TroopBaseName;
        ref readonly var unit = ref data.Character;
        
        if (!unit.IsHero && troopBaseName.TryGetValue(unit.StringId, out var original))
        {
            unit.UpdateName(original);
            troopNameOverride.Remove(unit.StringId);
        }
        else
        {
            var msg = $"[CharacterCreation] Unit {unit.Name} is not previously renamed.";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
        }
    }
}