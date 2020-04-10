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

                        if (hero.IsHumanPlayerCharacter && Settings.Instance.OverrideAge == true)
                        {
                            // We'll do nothing for now.
                            return false;
                        }
                        else
                        {
                            // Well this doesn't work.
                            if (hero.Age <= 17)
                            {
                                hero.DynamicBodyProperties = new DynamicBodyProperties(hero.DynamicBodyProperties.Age, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                            }
                            else
                            {
                                if (Settings.Instance.DebugMode == true)
                                    InformationManager.DisplayMessage(new InformationMessage("[Debug]: Should Age " + hero.Name, Color.FromUint(4282569842U)));
                            }
                        } return true;
                    } return true;
                }
                else
                {
                    foreach (object obj in dictionary.Keys)
                    {
                        Hero hero = (Hero)obj;

                        if (hero.IsHumanPlayerCharacter && Settings.Instance.OverrideAge == true)
                        {
                            // We'll do nothing for now.
                            return false;
                        }
                        else
                        {
                            //InformationManager.DisplayMessage(new InformationMessage("[Debug] Aged hero: " + hero.Name, Color.FromUint(4282569842U)));
                            hero.DynamicBodyProperties = new DynamicBodyProperties(hero.DynamicBodyProperties.Age, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                        }
                    }
                    return false;
                }
            }
        }
    }
}
