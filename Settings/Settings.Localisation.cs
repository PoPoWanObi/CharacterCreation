using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Localization;

namespace CharacterCreation
{
    public partial class DCCSettings
    {
        private const string DisplayNameText = "{=CharacterCration_ModNameText}Detailed Character Creation",
            
            Section0 = "{=CharacterCreation_Section0}Section 0: Debug Mode",
            DebugModeName = "{=CharacterCreation_DebugModeName}Enable debug output",
            DebugModeHint = "{=CharacterCreation_DebugModeHint}Enable DCC's debug output. Does NOT require restart.",
            
            Section1 = "{=CharacterCreation_Section1}Section 1: Overrides",
            IgnoreDailyTickName = "{=CharacterCreation_IgnoreDailyTickName}Overrides",
            IgnoreDailyTickHint = "{=CharacterCreation_IgnoreDailyTickHint}Keep this on to prevent the game from reverting your appearance. Does NOT require restart.",
            OverrideAgeName = "{=CharacterCreation_OverrideAgeName}Override Age",
            OverrideAgeHint = "{=CharacterCreation_OverrideAgeHint}When enabled, this will prevent FaceGen from changing a hero's age. Does NOT require restart.",
            DisableAutoAgingName = "{=CharacterCreation_DisableAutoAgingName}Disable Auto Aging",
            DisableAutoAgingHint = "{=CharacterCreation_DisableAutoAgingHint}Enable this to prevent the game from changing the age physical appearance. Does NOT require restart.",
            
            Section2 = "{=CharacterCreation_Section2}Section 2: Age Model",
            CustomAgeModelName = "{=CharacterCreation_CustomAgeModelName}Custom Age Model",
            CustomAgeModelHint = "{=CharacterCreation_CustomAgeModelHint}Enable this to use a custom age model. Disable if another mod uses a custom age model. REQUIRES restart.",
            BecomeInfantAgeName = "{=CharacterCreation_BecomeInfantAgeName}Infant Age Stage",
            BecomeInfantAgeHint = "{=CharacterCreation_BecomeInfantAgeHint}Set the default infant stage age. Does NOT require restart.",
            BecomeChildAgeName = "{=CharacterCreation_BecomeChildAgeName}Child Age Stage",
            BecomeChildAgeHint = "{=CharacterCreation_BecomeChildAgeHint}Set the default child stage age. Does NOT require restart.",
            BecomeTeenagerAgeName = "{=CharacterCreation_BecomeTeenagerAgeName}Teenager Age Stage",
            BecomeTeenagerAgeHint = "{=CharacterCreation_BecomeTeenagerAgeHint}Set the default teenager stage age. Does NOT require restart.",
            BecomeAdultAgeName = "{=CharacterCreation_BecomeAdultAgeName}Adult Age Stage",
            BecomeAdultAgeHint = "{=CharacterCreation_BecomeAdultAgeHint}Set the default adult stage age. Does NOT require restart.",
            BecomeOldAgeName = "{=CharacterCreation_BecomeOldAgeName}Old Age Stage",
            BecomeOldAgeHint = "{=CharacterCreation_BecomeOldAgeHint}Set the default old stage age. Does NOT require restart.",
            MaxAgeName = "{=CharacterCreation_MaxAgeName}Max Age Stage",
            MaxAgeHint = "{=CharacterCreation_MaxAgeHint}Set the default max age. Does NOT require restart.";

        private static readonly TextObject DisplayNameTextObject = new TextObject(DisplayNameText);
    }
}