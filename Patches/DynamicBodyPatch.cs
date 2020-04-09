using HarmonyLib;
using Helpers;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

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
                IDictionary dictionary = (IDictionary)typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorsDictionary", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

                if (Settings.Instance.IgnoreDailyTick == true)
                {
                    foreach (object obj in dictionary.Keys)
                    {
                        Hero hero = (Hero)obj;
                        float age = hero.Age;
                        float weight = hero.DynamicBodyProperties.Weight;
                        float build = hero.DynamicBodyProperties.Build;

                        if (hero.IsHumanPlayerCharacter && Settings.Instance.OverrideAge == true)
                        {
                            // We'll do nothing for now.
                            return false;
                        }
                        else
                        {
                            InformationManager.DisplayMessage(new InformationMessage("[Debug] Aged hero: " + hero.Name, Color.FromUint(4282569842U)));
                            hero.DynamicBodyProperties = new DynamicBodyProperties(age, weight, build);
                        }
                    }
                    return true;
                }
                else
                {
                    // Run Vanilla method
                    return false;
                }
            }
        }
    }
}
