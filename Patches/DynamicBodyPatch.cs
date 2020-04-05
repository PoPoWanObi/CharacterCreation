using System;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;


namespace CharacterCreation
{
    public class TweakedDynamicBody : DynamicBodyCampaignBehavior
    {

        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "OnDailyTick")]
        private class OnDailyTick
        {
            static bool Prefix(DynamicBodyCampaignBehavior __instance)
            {
                try
                {
                    IDictionary dictionary = (IDictionary)typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorsRecords", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance));
                    foreach (Hero hero in dictionary.Keys)
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An exception happened during the OnDailyTick patch:\n\n{ex.Message}\n\n{ex.InnerException?.Message}", "Exception");
                }
                return false;
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.IgnoreDailyTick;
        }
    }
}
