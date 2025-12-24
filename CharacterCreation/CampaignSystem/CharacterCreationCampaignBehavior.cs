using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.CampaignSystem
{
    public class CharacterCreationCampaignBehavior : CampaignBehaviorBase
    {
        public static CharacterCreationCampaignBehavior? Instance { get; private set; }

        private readonly ICampaignBehaviorImplementation _implementation;
        private CampaignBehaviorData _data;

        public CharacterCreationCampaignBehavior()
        {
            _data = CampaignBehaviorData.Create();
            _implementation = SubModule.Instance!.EntryPoint.InitializeCampaignBehavior();
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

        private void OnGameStart(CampaignGameStarter gameStarter) => _implementation.OnGameStart(_data, gameStarter);

        public override void SyncData(IDataStore dataStore)
        {
            dataStore.SyncData("dcc_propertiesMin", ref _data.BodyPropertiesOverrideMin);
            dataStore.SyncData("dcc_propertiesMax", ref _data.BodyPropertiesOverrideMax);
            dataStore.SyncData("dcc_tags", ref _data.TagOverrides);
            dataStore.SyncData("dcc_troopnames", ref _data.TroopNameOverride);
        }

        public void SetBodyPropertiesOverride(CharacterObject unit, BodyProperties bodyProperties, int race,
            bool isFemale, CharacterEditorStatePropertyType editPropertyType) =>
            _implementation.SetBodyPropertiesOverride(_data, unit, bodyProperties, race, isFemale, editPropertyType);

        public void SetTagOverride(CharacterObject unit, string? hairTags = null, string? beardTags = null,
            string? tattooTags = null) => _implementation.SetTagOverride(_data, unit, hairTags, beardTags, tattooTags);

        public bool HasBodyPropertiesOverride(CharacterObject unit) =>
            _implementation.HasBodyPropertiesOverride(_data, unit);

        public bool UndoBodyPropertiesOverride(CharacterObject unit) =>
            _implementation.UndoBodyPropertiesOverride(_data, unit);

        public void SetUnitNameOverride(CharacterObject unit, string name) =>
            _implementation.SetUnitNameOverride(_data, unit, name);

        public bool HasUnitNameOverride(CharacterObject unit) => _implementation.HasUnitNameOverride(_data, unit);

        public bool UndoUnitNameOverride(CharacterObject unit) => _implementation.UndoUnitNameOverride(_data, unit);
    }
}
