using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CharacterCreation.Settings;
using CharacterCreation.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.CampaignSystem
{
    public class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        private static readonly Action<BasicCharacterObject, MBBodyProperty> SetCharacterBodyPropertyRange;
        
        public static CharacterCreationCampaignBehavior? Instance { get; private set; }

        static CharacterCreationCampaignBehavior()
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

        private CampaignBehaviorData _data;
        
        // for internal backup only
        private readonly Dictionary<string, UnitBodyPropertiesBase> _troopBaseVersion;
        private readonly Dictionary<string, TextObject> _troopBaseName;

        public CharacterCreationCampaignBehavior()
        {
            _data = CampaignBehaviorData.Create();
            _troopBaseVersion = new Dictionary<string, UnitBodyPropertiesBase>();
            _troopBaseName = new Dictionary<string, TextObject>();
            Instance = this;
        }

        ~CharacterCreationCampaignBehavior()
        {
            if (Instance == this) Instance = null;
        }
        
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

        private void StoreBaseVersionOnly(CharacterObject unit)
        {
            if (_troopBaseVersion.ContainsKey(unit.StringId)) return;
            _troopBaseVersion[unit.StringId] = new UnitBodyPropertiesBase(unit.StringId, unit.BodyPropertyRange, unit.Race, unit.IsFemale);
        }

        public override void RegisterEvents()
        {
            CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, OnGameStart);
        }

        private void OnGameStart(CampaignGameStarter gameStarter)
        {
            // apply any min overrides
            Parallel.ForEach(_data.BodyPropertiesOverrideMin, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                    if (charObj != null)
                    {
                        StoreBaseVersionOnly(charObj);
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
            Parallel.ForEach(_data.BodyPropertiesOverrideMax, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                    if (charObj != null)
                    {
                        StoreBaseVersionOnly(charObj);
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
            Parallel.ForEach(_data.TagOverrides, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                    if (charObj != null)
                    {
                        StoreBaseVersionOnly(charObj);
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
            Parallel.ForEach(_data.TroopNameOverride, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                    if (charObj != null)
                    {
                        _troopBaseName[charObj.StringId ?? kv.Value.UnitId] = charObj.Name;
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

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("dcc_propertiesMin", ref _data.BodyPropertiesOverrideMin);
            dataStore.SyncData("dcc_propertiesMax", ref _data.BodyPropertiesOverrideMax);
            dataStore.SyncData("dcc_tags", ref _data.TagOverrides);
            dataStore.SyncData("dcc_troopnames", ref _data.TroopNameOverride);
        }

        public void SetBodyPropertiesOverride(CharacterObject unit, BodyProperties bodyProperties, int race,
            bool isFemale, CharacterEditorStatePropertyType editPropertyType)
        {
            if (unit.IsHero) return;

            // save the base version and override
            StoreBaseVersionOnly(unit);
            var propertiesOverride = new UnitBodyPropertiesOverride(unit.StringId, bodyProperties, race, isFemale);

            if ((editPropertyType & CharacterEditorStatePropertyType.MinProperties) == CharacterEditorStatePropertyType.MinProperties)
            {
                _data.BodyPropertiesOverrideMax[unit.StringId] = propertiesOverride;
                ApplyBodyPropertyOverride(unit, propertiesOverride, true);
            }
            if ((editPropertyType & CharacterEditorStatePropertyType.MaxProperties) == CharacterEditorStatePropertyType.MaxProperties)
            {
                _data.BodyPropertiesOverrideMin[unit.StringId] = propertiesOverride;
                ApplyBodyPropertyOverride(unit, propertiesOverride);
            }
        }

        public void SetTagOverride(CharacterObject unit, string? hairTags = null, string? beardTags = null,
            string? tattooTags = null)
        {
            if (unit.IsHero) return;
            
            // save the base version and override
            StoreBaseVersionOnly(unit);
            if (!_data.TagOverrides.TryGetValue(unit.StringId, out var tagOverride))
            {
                tagOverride = new UnitTagOverride(unit.StringId);
                _data.TagOverrides[unit.StringId] = tagOverride;
            }
            
            if (hairTags != null) tagOverride.HairTags = hairTags;
            if (beardTags != null) tagOverride.BeardTags = beardTags;
            if (tattooTags != null) tagOverride.TattooTags = tattooTags;
            ApplyTagOverride(unit, tagOverride);
        }

        public bool HasBodyPropertiesOverride(CharacterObject unit) => _troopBaseVersion.ContainsKey(unit.StringId);

        public bool UndoBodyPropertiesOverride(CharacterObject unit)
        {
            if (!unit.IsHero && _troopBaseVersion.TryGetValue(unit.StringId, out var original))
            {
                SetCharacterBodyPropertyRange(unit, original.BodyPropertyRange);
                unit.Race = original.Race;
                unit.IsFemale = original.IsFemale;
                _data.BodyPropertiesOverrideMin.Remove(unit.StringId);
                _data.BodyPropertiesOverrideMax.Remove(unit.StringId);
                _troopBaseVersion.Remove(unit.StringId);
                return true;
            }
            else
            {
                var msg = $"[CharacterCreation] Unit {unit.Name} is not previously overridden.";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                return false;
            }
        }

        public void SetUnitNameOverride(CharacterObject unit, string name)
        {
            if (unit.IsHero) return;

            if (!_troopBaseName.ContainsKey(unit.StringId))
                _troopBaseName[unit.StringId] = unit.Name;
            _data.TroopNameOverride[unit.StringId] = new TroopNameOverride(unit.StringId, name);

            unit.UpdateName(new TextObject(name));
        }

        public bool HasUnitNameOverride(CharacterObject unit) => _data.TroopNameOverride.ContainsKey(unit.StringId);

        public bool UndoUnitNameOverride(CharacterObject unit)
        {
            if (!unit.IsHero && _troopBaseName.TryGetValue(unit.StringId, out var original))
            {
                unit.UpdateName(original);
                _data.TroopNameOverride.Remove(unit.StringId);
                return true;
            }
            else
            {
                var msg = $"[CharacterCreation] Unit {unit.Name} is not previously renamed.";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                return false;
            }
        }
    }
}
