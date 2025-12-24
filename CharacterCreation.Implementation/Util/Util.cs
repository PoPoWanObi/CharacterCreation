using System.Globalization;
using TaleWorlds.CampaignSystem;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Util
{
    public static class Util
    {
        public static CommandImplementation GetCommandImplementation()
        {
            var implementation = new CommandImplementation
            {
                AgeHeroCallback = strings =>
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
                },
                AgeCallback = strings =>
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
                },
                AspectCallback = strings => TaleWorlds.Engine.Screen.AspectRatio.ToString(CultureInfo.InvariantCulture)
            };
            return implementation;
        }
    }
}