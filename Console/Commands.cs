using CharacterCreation.Manager;
using Helpers;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CharacterCreation
{
    public static partial class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("toggle", "dcc")]
        public static string Override(List<string> strings)
        {
            if (Campaign.Current == null || TaleWorlds.Core.FaceGen.ShowDebugValues)
            {
                //InformationManager.DisplayMessage(new InformationMessage("Campaign not loaded.", Color.FromUint(4282569842U)));
                TaleWorlds.Core.FaceGen.ShowDebugValues = false; // Enable developer facegen
                return DccDisabledMsg.ToString();
            }
            
            TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Enable developer facegen
            return DccEnabledMsg.ToString();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("age_hero", "dcc")]
        public static string AgeHero(List<string> strings)
        {
            if (!CampaignCheats.CheckParameters(strings, 2) || CampaignCheats.CheckHelp(strings))
            {
                return $"{FormatMsgHeader} \"dcc.age_hero [{HeroNameText}] [{AgeText}]\".";
            }
            Hero hero = CampaignCheats.GetHero(strings[0].Replace('_', ' '));
            if (hero == null)
            {
                return HeroNotFoundMsg.ToString();
            }
            if (!int.TryParse(strings[1], out int num))
            {
                return EnterAgeMsg.ToString();
            }
            CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, num, true);
            return SuccessMsg.ToString();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("age", "dcc")]
        public static string Age(List<string> strings)
        {
            if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
            {
                return $"{FormatMsgHeader} \"dcc.age [{AgeText}]\".";
            }

            if (!int.TryParse(strings[0], out int num))
            {
                return EnterAgeMsg.ToString();
            }
            CharacterBodyManager.ResetBirthDayForAge(Hero.MainHero.CharacterObject, num, true);
            return SuccessMsg.ToString();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("reset_exception_count", "dcc")]
        public static string ResetExceptionCount(List<string> strings)
        {
            DCCSettingsUtil.ExceptionCount = 0;
            return SuccessMsg.ToString();
        }
    }
}
