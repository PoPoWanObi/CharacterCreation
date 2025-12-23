using System.Collections.Generic;
using System.Globalization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Util
{
    public static class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("age_hero", "dcc")]
        public static string AgeHero(List<string> strings)
        {
            if (!CampaignCheats.CheckParameters(strings, 2) || CampaignCheats.CheckHelp(strings))
            {
                return $"{FormatMsgHeader} \"dcc.age_hero [{HeroNameText}] [{AgeText}]\".";
            }

            var hero = Hero.FindFirst(x => x.GetName().ToString() == strings[0].Replace('_', ' '));
            if (hero == null)
            {
                return HeroNotFoundMsg;
            }
            if (!int.TryParse(strings[1], out var num))
            {
                return EnterAgeMsg;
            }
            UnitEditorFunctions.ResetBirthDayForAge(hero.CharacterObject, num, true);
            return SuccessMsg;
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
                return EnterAgeMsg;
            }
            UnitEditorFunctions.ResetBirthDayForAge(Hero.MainHero.CharacterObject, num, true);
            return SuccessMsg;
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("aspect", "dcc")]
        public static string Aspect(List<string> strings)
        {
            return TaleWorlds.Engine.Screen.AspectRatio.ToString(CultureInfo.InvariantCulture);
        }
    }
}
