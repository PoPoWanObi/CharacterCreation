using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Manager
{
    class CharacterBodymanager
    {
        public static void copyDynamicBodyProperties(DynamicBodyProperties src, DynamicBodyProperties target)
        {
            target.Age = src.Age;
            target.Build = src.Build;
            target.Weight = src.Weight;
        }

        public static void resetBirthDayForAge(CharacterObject characterObject, float targetAge)
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
