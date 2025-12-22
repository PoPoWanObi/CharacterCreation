using TaleWorlds.SaveSystem;

namespace CharacterCreation.CampaignSystem
{
    internal class UnitTagOverride
    {
        [SaveableProperty(0)]
        public string UnitId { get; private set; }
        
        [SaveableProperty(1)]
        public string? HairTags { get; set; }
        
        [SaveableProperty(2)]
        public string? BeardTags { get; set; }
        
        [SaveableProperty(3)]
        public string? TattooTags { get; set; }
        
        public UnitTagOverride(string unitId) => UnitId = unitId;
    }
}