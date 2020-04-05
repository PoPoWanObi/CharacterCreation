using System;
using System.Reflection;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;
using TaleWorlds.Core;

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
                // We need to collect the native method variables and re-write the system.
                // Still learning Harmony myself, so if you feel up to the task, please go ahead.

                /*try
                {
                    bool CanBeEffectedByProperties(Hero hero)
                    {
                        return hero.IsAlive && !hero.IsNotable && (hero.Age.ApproximatelyEqualsTo((float)Campaign.Current.Models.AgeModel.HeroComesOfAge, 0.1f) || hero.Age > (float)Campaign.Current.Models.AgeModel.HeroComesOfAge) && hero.Clan != CampaignData.NeutralFaction && (hero.IsNoble || hero.IsPartyLeader || (hero.IsWanderer && hero.CompanionOf != null));
                    }

                    IDictionary dictionary = (IDictionary)(typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorRecords", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(__instance));
                    foreach (Hero hero in dictionary.Keys)
                    {

                        if (CanBeEffectedByProperties(hero))
                        {
                            float weight4 = hero.DynamicBodyProperties.Weight;
                            float build2 = hero.DynamicBodyProperties.Build;
                            hero.DynamicBodyProperties = new DynamicBodyProperties(hero.Age, weight4, MBMath.ClampFloat(build2 - 0.01f, 0f, maxValue2));
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An exception happened during the OnDailyTick patch:\n\n{ex.Message}\n\n{ex.InnerException?.Message}", "Exception");
                }*/
                return false; // We're just gonna basically NOP the function for now, so the DailyTick doesn't do anything.
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
    }
}
