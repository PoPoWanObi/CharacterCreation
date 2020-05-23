using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Localization;

namespace CharacterCreation
{
    public partial class Settings
    {
        private const string ModNameText = "{=CharacterCration_ModNameText}Detailed Character Creation",
            
            Section0 = "{=CharacterCration_Section0}Section 0: Debug Mode",
            DebugModeName = "{=CharacterCration_DebugModeName}Enable debug output",
            DebugModeHint = "{=CharacterCration_DebugModeHint}Enable DCC's debug output. Does NOT require restart.",
            
            Section1 = "{=CharacterCration_Section1}Section 1: Overrides",
            IgnoreDailyTickName = "{=CharacterCration_IgnoreDailyTickName}Overrides",
            IgnoreDailyTickHint = "{=CharacterCration_IgnoreDailyTickHint}Keep this on to prevent the game from reverting your appearance. REQUIRES restart.",
            OverrideAgeName = "{=CharacterCration_OverrideAgeName}Override Age",
            OverrideAgeHint = "{=CharacterCration_OverrideAgeHint}When enabled, this will prevent FaceGen from changing a hero's age. REQUIRES restart.",
            DisableAutoAgingName = "{=CharacterCration_DisableAutoAgingName}Disable Auto Aging",
            DisableAutoAgingHint = "{=CharacterCration_DisableAutoAgingHint}Enable this to prevent the game from changing the age physical appearance. Does NOT require restart.",
            
            Section2 = "{=CharacterCration_Section2}Section 2: Age Model",
            CustomAgeModelName = "{=CharacterCration_CustomAgeModelName}Custom Age Model",
            CustomAgeModelHint = "{=CharacterCration_CustomAgeModelHint}Enable this to use a custom age model. Disable if another mod uses a custom age model. REQUIRES restart.",
            BecomeInfantAgeName = "{=CharacterCration_BecomeInfantAgeName}Infant Age Stage",
            BecomeInfantAgeHint = "{=CharacterCration_BecomeInfantAgeHint}Set the default infant stage age. Does NOT require restart.",
            BecomeChildAgeName = "{=CharacterCration_BecomeChildAgeName}Child Age Stage",
            BecomeChildAgeHint = "{=CharacterCration_BecomeChildAgeHint}Set the default child stage age. Does NOT require restart.",
            BecomeTeenagerAgeName = "{=CharacterCration_BecomeTeenagerAgeName}Teenager Age Stage",
            BecomeTeenagerAgeHint = "{=CharacterCration_BecomeTeenagerAgeHint}Set the default teenager stage age. Does NOT require restart.",
            BecomeAdultAgeName = "{=CharacterCration_BecomeAdultAgeName}Adult Age Stage",
            BecomeAdultAgeHint = "{=CharacterCration_BecomeAdultAgeHint}Set the default adult stage age. Does NOT require restart.",
            BecomeOldAgeName = "{=CharacterCration_BecomeOldAgeName}Old Age Stage",
            BecomeOldAgeHint = "{=CharacterCration_BecomeOldAgeHint}Set the default old stage age. Does NOT require restart.",
            MaxAgeName = "{=CharacterCration_MaxAgeName}Max Age Stage",
            MaxAgeHint = "{=CharacterCration_MaxAgeHint}Set the default max age. Does NOT require restart.";

        private static readonly TextObject ModNameTextObject = new TextObject(ModNameText);
    }
}
