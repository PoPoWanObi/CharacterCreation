using HarmonyLib;
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            static bool Prefix(ref Dictionary<Hero, object> ____heroBehaviorsDictionary)
            {
                if (Settings.Instance.IgnoreDailyTick == true)
                {
                    IDictionary dictionary = (IDictionary)typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorsDictionary", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(____heroBehaviorsDictionary);

                    foreach (object obj in dictionary.Keys)
                    {
                        Hero hero = (Hero)obj;

                        if (Settings.Instance.DisableAutoAging == false)
                        {
                            if (hero.IsHumanPlayerCharacter)
                            {
                                if (Settings.Instance.DebugMode == true)
                                    InformationManager.DisplayMessage(new InformationMessage("[Debug] Set appearance for: " + hero.Name, ColorManager.Red));
                                var test = new DynamicBodyProperties(hero.DynamicBodyProperties.Age + 12f, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);

                                if (Settings.Instance.DebugMode == true)
                                    InformationManager.DisplayMessage(new InformationMessage("[Debug] Result: " + test, ColorManager.Red)); 
                                hero.DynamicBodyProperties.Equals(test);

                                // TODO: Get access to keyValuePair w/ Reflection

                                /*float weight = hero.DynamicBodyProperties.Weight;
                                float build = hero.DynamicBodyProperties.Build;
                                ____heroBehaviorsDictionary.Key.DynamicBodyProperties = new DynamicBodyProperties(____heroBehaviorsDictionary.Key.Age, weight, build;*/
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
