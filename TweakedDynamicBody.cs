using System;
using System.Xml;
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
        [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "OnDailyTick()")]
        private void OnDailyTick()
        {
            IDictionary dictionary = (IDictionary)typeof(DynamicBodyCampaignBehavior).GetField("_heroBehaviorsRecords", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (object obj in dictionary.Keys)
            {
                Hero hero = (Hero)obj;
                // Code to execute?
            }
        }
    }
}
