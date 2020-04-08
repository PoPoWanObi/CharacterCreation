using HarmonyLib;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;

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
                {
                    //InformationManager.DisplayMessage(new InformationMessage("[Debug] Daily tick ignored.", Color.FromUint(4282569842U)));
                    return false; // We're just gonna basically NOP the function for now, so the DailyTick doesn't do anything.
                }
                    
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
    }
}
