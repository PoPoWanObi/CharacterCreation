using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace CharacterCreation
{
    public static class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("check", "state")]
        public static string Override(List<string> strings)
        {
            if (Campaign.Current == null)
            {
                InformationManager.DisplayMessage(new InformationMessage("Campaign not loaded.", Color.FromUint(4282569842U)));
                return "Campaign was not started.";
            }
            else
            {
                DynamicBodyCampaignBehavior behaviour = Campaign.Current.GetCampaignBehavior<DynamicBodyCampaignBehavior>();

                MethodInfo method = behaviour.GetType().GetMethod("OnDailyTick", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (method == null)
                {
                    return "Failed to get method for OnDailyTick.";
                }
            }
            return "Finished.";
        }
    }
}
