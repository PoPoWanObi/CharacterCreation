using Helpers;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CharacterCreation
{
    public static class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("toggle", "dcc")]
        public static string Override(List<string> strings)
        {
            if (Campaign.Current == null)
            {
                //InformationManager.DisplayMessage(new InformationMessage("Campaign not loaded.", Color.FromUint(4282569842U)));
                TaleWorlds.Core.FaceGen.ShowDebugValues = false; // Enable developer facegen
                return "Detailed Character Creation disabled.";
            }
            else
            {
                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Enable developer facegen
                return "You have enabled Detailed Character Creation. Press V to access.";
            }
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("age", "dcc")]
        public static string Age(List<string> strings)
        {
            if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
            {
                return "Format is \"dcc.age [Age]\".";
            }
            int num = 1;
            if (!int.TryParse(strings[0], out num))
            {
                return "Please enter a number";
            }
            Hero.MainHero.BirthDay = HeroHelper.GetRandomBirthDayForAge((float)num);
            return "Success";
        }
    }
}
