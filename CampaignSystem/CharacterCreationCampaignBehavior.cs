using CharacterCreation.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CharacterCreation.CampaignSystem
{
    internal class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        public static CharacterCreationCampaignBehavior Instance { get; private set; }

        private Dictionary<string, UnitBodyPropertiesOverride> bodyPropertiesOverride;
        private Dictionary<string, TroopNameOverride> troopNameOverride;

        public CharacterCreationCampaignBehavior()
        {
            bodyPropertiesOverride = new Dictionary<string, UnitBodyPropertiesOverride>();
            troopNameOverride = new Dictionary<string, TroopNameOverride>();
            Instance = this;
        }

        ~CharacterCreationCampaignBehavior()
        {
            if (Instance == this) Instance = null;
        }

        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnGameStart);
        }

        private void OnGameStart(CampaignGameStarter gameStarter)
        {
            Parallel.ForEach(bodyPropertiesOverride, kv =>
            {
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitID);
                if (charObj != default)
                {
                    charObj.BodyPropertyRange.Init(kv.Value.BodyProperties, kv.Value.BodyProperties);
                    charObj.Race = kv.Value.Race;
                    charObj.IsFemale = kv.Value.IsFemale;
                }
            });
            Parallel.ForEach(troopNameOverride, kv =>
            {
                var charObj = Game.Current.ObjectManager.GetObject<CharacterObject>(kv.Value.UnitId);
                if (charObj != default)
                {
                    charObj.UpdateName(new TextObject(kv.Value.UnitName));
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
            bodyPropertiesOverride[unit.StringId] = new UnitBodyPropertiesOverride(unit.StringId, bodyProperties, race, isFemale);
        }

        public void SetUnitNameOverride(CharacterObject unit, string name)
        {
            if (unit.IsHero) return;
            troopNameOverride[unit.StringId] = new TroopNameOverride(unit.StringId, name);
        }
    }
}
