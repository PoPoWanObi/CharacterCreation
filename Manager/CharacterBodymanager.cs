using TaleWorlds.Core;
using Helpers;
using TaleWorlds.CampaignSystem;

namespace CharacterCreation.Manager
{
    class CharacterBodyManager
    {
        public static void CopyDynamicBodyProperties(DynamicBodyProperties src, DynamicBodyProperties target)
        {
            target.Age = src.Age;
            target.Build = src.Build;
            target.Weight = src.Weight;

        }
        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge)
        {
            if (characterObject.IsHero)
            {
                Hero hero = characterObject.HeroObject;
                if (DCCSettings.Instance != null && !DCCSettings.Instance.OverrideAge)
                    hero.BirthDay = HeroHelper.GetRandomBirthDayForAge(targetAge);
            }
        }
    }
}