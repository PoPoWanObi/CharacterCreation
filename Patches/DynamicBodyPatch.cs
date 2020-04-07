using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;

namespace CharacterCreation.Patches
{
    public class TweakedDynamicBody : DynamicBodyCampaignBehavior
    {
        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "OnDailyTick")]
        private class OnDailyTick
        {
            static bool Prefix(DynamicBodyCampaignBehavior __instance)
            {
                
                if (Settings.Instance.IgnoreDailyTick == false)
                {
                    // Run vanilla code
                    return false; // TODO: Implement native calls
                }
                else
                    return false; // We're just gonna basically NOP the function for now, so the DailyTick doesn't do anything.
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
    }
}
