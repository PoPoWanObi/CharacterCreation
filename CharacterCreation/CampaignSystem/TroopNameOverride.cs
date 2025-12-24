using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    public class TroopNameOverride
    {
        [SaveableProperty(0)]
        public string UnitId { get; private set; }

        [SaveableProperty(1)]
        public string UnitName { get; private set; }

        public TroopNameOverride(string unitId, string unitName)
        {
            UnitId = unitId;
            UnitName = unitName;
        }
    }
}
