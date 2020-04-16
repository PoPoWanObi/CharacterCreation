using HarmonyLib;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using static TaleWorlds.CampaignSystem.Hero;

namespace CharacterCreation.Patches
{
    public class TweakedDynamicBody : DynamicBodyCampaignBehavior
    {
        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "OnDailyTick")]
        public class OnDailyTick
        {
            static bool Prefix(DynamicBodyCampaignBehavior __instance)
            {
                /*IDictionary dictionary = (IDictionary)typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorsDictionary", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);

                foreach (object obj in dictionary.Keys)
                {
                    Hero hero = (Hero)obj;

                    if (hero.Age >= 19)
                    {

                    }

                    if (hero.Age < 19)
                    {
                        if (Settings.Instance.DebugMode == true)
                            InformationManager.DisplayMessage(new InformationMessage("Hero updated: " + hero.Name, Color.FromUint(4282569842U)));
                        hero.DynamicBodyProperties = new DynamicBodyProperties(hero.DynamicBodyProperties.Age, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                    }
            }
                return true;*/

                if (Settings.Instance.IgnoreDailyTick == true)
                {
                    foreach (Hero hero in Hero.All)
                    {
                        if (Settings.Instance.DisableAutoAging == false)
                        {
                            //if (hero.Age < 19)'
                            if (hero.IsHumanPlayerCharacter)
                            {
                                if (Settings.Instance.DebugMode == true)
                                    InformationManager.DisplayMessage(new InformationMessage("Set appearance for: " + hero.Name));
                                hero.DynamicBodyProperties = new DynamicBodyProperties(hero.DynamicBodyProperties.Age, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                                

                            }
                        }
                    }
                    return false;
                }
                else
                    return true;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
    }
}
