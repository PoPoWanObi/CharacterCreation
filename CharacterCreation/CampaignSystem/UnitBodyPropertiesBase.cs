using TaleWorlds.Core;

namespace CharacterCreation.CampaignSystem
{
    public readonly struct UnitBodyPropertiesBase
    {
        public string UnitId { get; }
        public MBBodyProperty BodyPropertyRange { get; }
        public int Race { get; }
        public bool IsFemale { get; }

        public UnitBodyPropertiesBase(string unitId, MBBodyProperty bodyPropertyRange, int race, bool isFemale)
        {
            UnitId = unitId;
            BodyPropertyRange = bodyPropertyRange;
            Race = race;
            IsFemale = isFemale;
        }
    }
}