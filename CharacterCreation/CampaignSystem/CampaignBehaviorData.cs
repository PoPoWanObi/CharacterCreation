using System.Collections.Generic;

namespace CharacterCreation.CampaignSystem
{
    public struct CampaignBehaviorData
    {
        public Dictionary<string, UnitBodyPropertiesOverride> BodyPropertiesOverrideMin;
        public Dictionary<string, UnitBodyPropertiesOverride> BodyPropertiesOverrideMax;
        public Dictionary<string, UnitTagOverride> TagOverrides;
        public Dictionary<string, TroopNameOverride> TroopNameOverride;

        public static CampaignBehaviorData Create()
        {
            return new CampaignBehaviorData
            {
                BodyPropertiesOverrideMin = new Dictionary<string, UnitBodyPropertiesOverride>(),
                BodyPropertiesOverrideMax = new Dictionary<string, UnitBodyPropertiesOverride>(),
                TagOverrides = new Dictionary<string, UnitTagOverride>(),
                TroopNameOverride = new Dictionary<string, TroopNameOverride>()
            };
        }
    }
}