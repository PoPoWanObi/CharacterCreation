using CharacterCreation.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.CampaignSystem
{
    internal class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        public static CharacterCreationCampaignBehavior? Instance { get; private set; }

        private Dictionary<string, UnitBodyPropertiesOverride> bodyPropertiesOverride;
        private Dictionary<string, TroopNameOverride> troopNameOverride;

        // for internal backup only
        private Dictionary<string, UnitBodyPropertiesOverride> troopBaseVersion;
        private Dictionary<string, TextObject> troopBaseName;

        public CharacterCreationCampaignBehavior()
        {
            bodyPropertiesOverride = new Dictionary<string, UnitBodyPropertiesOverride>();
            troopNameOverride = new Dictionary<string, TroopNameOverride>();
            troopBaseVersion = new Dictionary<string, UnitBodyPropertiesOverride>();
            troopBaseName = new Dictionary<string, TextObject>();
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

        private void OnGameStart(CampaignGameStarter gameStarter)
        {
            Parallel.ForEach(bodyPropertiesOverride, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitID);
                    if (charObj != default)
                    {
                        troopBaseVersion[charObj.StringId ?? kv.Value.UnitID] =
                            new UnitBodyPropertiesOverride(charObj.StringId, charObj.GetBodyProperties(charObj.Equipment), charObj.Race, charObj.IsFemale);
                        charObj.BodyPropertyRange.Init(kv.Value.BodyProperties, kv.Value.BodyProperties);
                        charObj.Race = kv.Value.Race;
                        charObj.IsFemale = kv.Value.IsFemale;
                    }
                }
                catch (Exception e)
                {
                    InformationManager.DisplayMessage(new InformationMessage("[CharacterCreation] " + e.ToString(), new Color(1f, 0f, 0f)));
                    Debug.Print("[CharacterCreation] " + e.ToString());
                }
            });
            Parallel.ForEach(troopNameOverride, kv =>
            {
                try
                {
                    var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                    if (charObj != default)
                    {
                        troopBaseName[charObj.StringId ?? kv.Value.UnitId] = charObj.Name;
                        charObj.UpdateName(new TextObject(kv.Value.UnitName));
                    }
                }
                catch (Exception e)
                {
                    InformationManager.DisplayMessage(new InformationMessage("[CharacterCreation] " + e.ToString(), new Color(1f, 0f, 0f)));
                    Debug.Print("[CharacterCreation] " + e.ToString());
                }
            });
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("dcc_properties", ref bodyPropertiesOverride);
            dataStore.SyncData("dcc_troopnames", ref troopNameOverride);
        }

        public void SetBodyPropertiesOverride(CharacterObject unit, BodyProperties bodyProperties, int race, bool isFemale)
        {
            if (unit.IsHero) return;

            // save base version and override
            if (!troopBaseVersion.ContainsKey(unit.StringId))
                troopBaseVersion[unit.StringId] =
                    new UnitBodyPropertiesOverride(unit.StringId, unit.GetBodyProperties(unit.Equipment), unit.Race, unit.IsFemale);
            bodyPropertiesOverride[unit.StringId] = new UnitBodyPropertiesOverride(unit.StringId, bodyProperties, race, isFemale);
            // apply override
            unit.BodyPropertyRange.Init(bodyProperties, bodyProperties);
            unit.Race = race;
            unit.IsFemale = isFemale;
        }

        public bool HasBodyPropertiesOverride(CharacterObject unit) => bodyPropertiesOverride.ContainsKey(unit.StringId);

        public bool UndoBodyPropertiesOverride(CharacterObject unit)
        {
            if (!unit.IsHero && troopBaseVersion.TryGetValue(unit.StringId, out var @override))
            {
                unit.BodyPropertyRange.Init(@override.BodyProperties, @override.BodyProperties);
                unit.Race = @override.Race;
                unit.IsFemale = @override.IsFemale;
                bodyPropertiesOverride.Remove(unit.StringId);
                return true;
            }
            else
            {
                Debug.Print($"[CharacterCreation] Unit {unit.Name} is not previously overridden.");
                return false;
            }
        }

        public void SetUnitNameOverride(CharacterObject unit, string name)
        {
            if (unit.IsHero) return;

            if (!troopBaseName.ContainsKey(unit.StringId))
                troopBaseName[unit.StringId] = unit.Name;
            troopNameOverride[unit.StringId] = new TroopNameOverride(unit.StringId, name);

            unit.UpdateName(new TextObject(name));
        }

        public bool HasUnitNameOverride(CharacterObject unit) => troopNameOverride.ContainsKey(unit.StringId);

        public bool UndoUnitNameOverride(CharacterObject unit)
        {
            if (!unit.IsHero && troopBaseName.TryGetValue(unit.StringId, out var @override))
            {
                unit.UpdateName(@override);
                troopNameOverride.Remove(unit.StringId);
                return true;
            }
            else
            {
                Debug.Print($"[CharacterCreation] Unit {unit.Name} is not previously renamed.");
                return false;
            }
        }
    }
}
