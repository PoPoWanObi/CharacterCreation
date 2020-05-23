using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Localization;

namespace CharacterCreation
{
    public static partial class Commands
    {
        private static readonly TextObject DccDisabledMsg = new TextObject("{=CharacterCreation_DccDisabledMsg}Detailed Character Creation disabled."),
            DccEnabledMsg = new TextObject("{=CharacterCreation_DccEnabledMsg}You have enabled Detailed Character Creation. Press V to access."),

            FormatMsgHeader = new TextObject("{=CharacterCreation_FormatMsgHeader}Format: "),
            HeroNameText = new TextObject("{=CharacterCreation_HeroNameText}HeroName"),
            AgeText = new TextObject("{=CharacterCreation_AgeText}Age"),

            HeroNotFoundMsg = new TextObject("{=CharacterCreation_HeroNotFoundMsg}Hero is not found"),
            EnterAgeMsg = new TextObject("{=CharacterCreation_EnterAgeMsg}Please enter an age."),
            SuccessMsg = new TextObject("{=CharacterCreation_SuccessMsg}Success");
    }
}
