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
                return
                    $"{FormatMsgHeaderTextObject} \"dcc.age_hero [{HeroNameTextTextObject}] [{AgeTextTextObject}]\".";
            }

            var hero = Hero.FindFirst(x => x.GetName().ToString() == strings[0].Replace('_', ' '));
            if (hero == null)
            {
                return HeroNotFoundMsgTextObject.ToString();
            }

            if (!int.TryParse(strings[1], out var num))
            {
                return EnterAgeMsgTextObject.ToString();
            }

            UnitEditorFunctions.ResetBirthDayForAge(hero.CharacterObject, num, true);
            return SuccessMsgTextObject.ToString();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("age", "dcc")]
        public static string Age(List<string> strings)
        {
            if (CampaignCheats.CheckParameters(strings, 0) || CampaignCheats.CheckHelp(strings))
            {
                return $"{FormatMsgHeaderTextObject} \"dcc.age [{AgeTextTextObject}]\".";
            }

            if (!int.TryParse(strings[0], out var num))
            {
                return EnterAgeMsgTextObject.ToString();
            }
            UnitEditorFunctions.ResetBirthDayForAge(Hero.MainHero.CharacterObject, num, true);
            return SuccessMsgTextObject.ToString();
        }

        [CommandLineFunctionality.CommandLineArgumentFunction("aspect", "dcc")]
        public static string Aspect(List<string> strings)
        {
            return TaleWorlds.Engine.Screen.AspectRatio.ToString(CultureInfo.InvariantCulture);
        }
    }
}
