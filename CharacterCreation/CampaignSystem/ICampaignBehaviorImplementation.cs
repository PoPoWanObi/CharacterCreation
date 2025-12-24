using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.CampaignSystem
{
    public interface ICampaignBehaviorImplementation
    {
        void OnGameStart(CampaignBehaviorData data, CampaignGameStarter gameStarter);

        void SetBodyPropertiesOverride(CampaignBehaviorData data, CharacterObject unit, BodyProperties bodyProperties,
            int race, bool isFemale, CharacterEditorStatePropertyType editPropertyType);

        void SetTagOverride(CampaignBehaviorData data, CharacterObject unit, string? hairTags = null,
            string? beardTags = null, string? tattooTags = null);

        bool HasBodyPropertiesOverride(CampaignBehaviorData data, CharacterObject unit);

        bool UndoBodyPropertiesOverride(CampaignBehaviorData data, CharacterObject unit);

        void SetUnitNameOverride(CampaignBehaviorData data, CharacterObject unit, string name);

        bool HasUnitNameOverride(CampaignBehaviorData data, CharacterObject unit);

        bool UndoUnitNameOverride(CampaignBehaviorData data, CharacterObject unit);
    }
}