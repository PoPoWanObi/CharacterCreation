using HarmonyLib;
using Helpers;
using System.Collections;
using System.Reflection;
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
                        hero.DynamicBodyProperties = new DynamicBodyProperties(age, weight, build);
                        //InformationManager.DisplayMessage(new InformationMessage("Aged: " + hero.Name, Color.FromUint(4282569842U)));
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

        public void UpdateAge(float age)
        {
            return;
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
        
    }
}
