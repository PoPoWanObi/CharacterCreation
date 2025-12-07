using CharacterCreation.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CharacterCreation.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.CampaignSystem
{
    internal class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        private static readonly Action<BasicCharacterObject, MBBodyProperty> SetCharacterBodyPropertyRange;

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

        public static CharacterCreationCampaignBehavior? Instance { get; private set; }

        private Dictionary<string, UnitBodyPropertiesOverride> _bodyPropertiesOverrideMin;
        private Dictionary<string, UnitBodyPropertiesOverride> _bodyPropertiesOverrideMax;
        private Dictionary<string, TroopNameOverride> _troopNameOverride;

        // for internal backup only
        private readonly Dictionary<string, UnitBodyPropertiesBase> _troopBaseVersion;
        private readonly Dictionary<string, TextObject> _troopBaseName;

        public CharacterCreationCampaignBehavior()
        {
            _bodyPropertiesOverrideMin = new Dictionary<string, UnitBodyPropertiesOverride>();
            _bodyPropertiesOverrideMax = new Dictionary<string, UnitBodyPropertiesOverride>();
            _troopNameOverride = new Dictionary<string, TroopNameOverride>();
            _troopBaseVersion = new Dictionary<string, UnitBodyPropertiesBase>();
            _troopBaseName = new Dictionary<string, TextObject>();
            Instance = this;
        }

        ~CharacterCreationCampaignBehavior()
        {
            if (Instance == this) Instance = null;
        }

        public override void RegisterEvents()
        {
            CampaignEvents.OnAfterSessionLaunchedEvent.AddNonSerializedListener(this, OnGameStart);
        }

        private static void ApplyBodyPropertyOverride(CharacterObject character, in UnitBodyPropertiesOverride property, bool isMax = false)
        {
            var bodyProperty = MBBodyProperty.CreateFrom(character.BodyPropertyRange);
            SetCharacterBodyPropertyRange(character, bodyProperty);
            if (isMax) bodyProperty.Init(bodyProperty.BodyPropertyMin, property.BodyProperties);
            else bodyProperty.Init(property.BodyProperties, bodyProperty.BodyPropertyMax);
            character.Race = property.Race;
            character.IsFemale = property.IsFemale;
            if (DCCSettingsUtil.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] {character.Name} body properties overridden: {character.GetBodyProperties(character.Equipment).ToString()}, min {character.GetBodyPropertiesMin().ToString()}, max {character.GetBodyPropertiesMax().ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
        }

        private void StoreBaseVersionOnly(CharacterObject unit)
        {
            if (_troopBaseVersion.ContainsKey(unit.StringId)) return;
            _troopBaseVersion[unit.StringId] = new UnitBodyPropertiesBase(unit.StringId, unit.BodyPropertyRange, unit.Race, unit.IsFemale);
        }

        private void OnGameStart(CampaignGameStarter gameStarter)
        {
            Parallel.ForEach(_bodyPropertiesOverrideMin, kv =>
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
            Parallel.ForEach(_bodyPropertiesOverrideMax, kv =>
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
            Parallel.ForEach(_troopNameOverride, kv =>
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
            dataStore.SyncData("dcc_propertiesMin", ref _bodyPropertiesOverrideMin);
            dataStore.SyncData("dcc_propertiesMax", ref _bodyPropertiesOverrideMax);
            dataStore.SyncData("dcc_troopnames", ref _troopNameOverride);
        }

        public void SetBodyPropertiesOverride(CharacterObject unit, BodyProperties bodyProperties, int race, bool isFemale, CharacterEditorStatePropertyType editPropertyType)
        {
            if (unit.IsHero) return;

            // save the base version and override
            StoreBaseVersionOnly(unit);
            var propertiesOverride = new UnitBodyPropertiesOverride(unit.StringId, bodyProperties, race, isFemale);

            if ((editPropertyType & CharacterEditorStatePropertyType.MinProperties) == CharacterEditorStatePropertyType.MinProperties)
            {
                _bodyPropertiesOverrideMax[unit.StringId] = propertiesOverride;
                ApplyBodyPropertyOverride(unit, propertiesOverride, true);
            }
            if ((editPropertyType & CharacterEditorStatePropertyType.MaxProperties) == CharacterEditorStatePropertyType.MaxProperties)
            {
                _bodyPropertiesOverrideMin[unit.StringId] = propertiesOverride;
                ApplyBodyPropertyOverride(unit, propertiesOverride);
            }
        }

        public bool HasBodyPropertiesOverride(CharacterObject unit) => _troopBaseVersion.ContainsKey(unit.StringId);

        public bool UndoBodyPropertiesOverride(CharacterObject unit)
        {
            if (!unit.IsHero && _troopBaseVersion.TryGetValue(unit.StringId, out var original))
            {
                SetCharacterBodyPropertyRange(unit, original.BodyPropertyRange);
                unit.Race = original.Race;
                unit.IsFemale = original.IsFemale;
                _bodyPropertiesOverrideMin.Remove(unit.StringId);
                _bodyPropertiesOverrideMax.Remove(unit.StringId);
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
            _troopNameOverride[unit.StringId] = new TroopNameOverride(unit.StringId, name);

            unit.UpdateName(new TextObject(name));
        }

        public bool HasUnitNameOverride(CharacterObject unit) => _troopNameOverride.ContainsKey(unit.StringId);

        public bool UndoUnitNameOverride(CharacterObject unit)
        {
            if (!unit.IsHero && _troopBaseName.TryGetValue(unit.StringId, out var original))
            {
                unit.UpdateName(original);
                _troopNameOverride.Remove(unit.StringId);
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
