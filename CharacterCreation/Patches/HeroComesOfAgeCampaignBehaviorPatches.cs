using System.Collections.Generic;
using System.Reflection;
using CharacterCreation.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Library;

namespace CharacterCreation.Patches
{
    public static class HeroComesOfAgeCampaignBehaviorPatches
    {
        [HarmonyPatch]
        static class GenericCampaignBehaviorPatch
        {
            static bool Prepare()
            {
                if (DccSettings.Instance!.PatchPlayerComingOfAgeIssues)
                {
                    Debug.Print("[CharacterCreation] Will target player coming-of-age events that are like to be problematic.");
                    return true;
                }

                return false;
            }
            
            static IEnumerable<MethodBase> TargetMethods()
            {
                // targeted to avoid possible equipment override
                yield return AccessTools.Method(typeof(AgingCampaignBehavior), "OnHeroComesOfAge");
                // targeted to avoid possible unwanted banners
                yield return AccessTools.Method(typeof(BannerCampaignBehavior), "OnHeroComesOfAge");
                // EducationCampaignBehavior not targeted - go to school I guess?
                // by far most critical to target because otherwise it risks the player getting teleported
                yield return AccessTools.Method(typeof(HeroSpawnCampaignBehavior), "OnHeroComesOfAge");
                // unlikely due to checks but still targeted for the same reason, just in case
                yield return AccessTools.Method(typeof(TeleportationCampaignBehavior), "OnHeroComesOfAge");
                // DefaultCutscenesCampaignBehavior not targeted - good place for a parent-child moment I guess?
            }

            static bool Prefix(Hero hero) => !hero.IsHumanPlayerCharacter;
        }
    }
}