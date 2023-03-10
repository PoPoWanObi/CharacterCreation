using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.CampaignSystem
{
    internal class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        public static CharacterCreationCampaignBehavior Instance { get; private set; }

        private Dictionary<string, UnitBodyPropertiesOverride> bodyPropertiesOverride;

        public CharacterCreationCampaignBehavior()
        {
            bodyPropertiesOverride = new Dictionary<string, UnitBodyPropertiesOverride>();
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
                charObj?.BodyPropertyRange.Init(kv.Value.BodyProperties, kv.Value.BodyProperties);
            });
        }

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("dcc_properties", ref bodyPropertiesOverride);
        }

        public void SetBodyPropertiesOverride(CharacterObject unit, BodyProperties bodyProperties)
        {
            bodyPropertiesOverride[unit.StringId] = new UnitBodyPropertiesOverride(unit.StringId, bodyProperties);
        }
    }
}
