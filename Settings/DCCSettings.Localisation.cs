﻿using System;
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
            OptionsLabelName = "{=CharacterCreation_OptionsLabelName}Enable options label.",
            OptionsLabelHint = "{=CharacterCreation_OptionsLabelHint}Show DCC Options string in Encyclopedia.",

            Section1 = "{=CharacterCreation_Section1}Section 1: Overrides",
            IgnoreDailyTickName = "{=CharacterCreation_IgnoreDailyTickName}Suppress Daily Tick",
            IgnoreDailyTickHint = "{=CharacterCreation_IgnoreDailyTickHint}Keep this on to prevent the game from reverting your appearance. Does NOT require restart.",
            PatchAgeNotUpdatingOnCharEditName = "{=CharacterCreation_PatchAgeNotUpdatingOnCharEditName}Patch Age Not Updating On Character Edit",
            PatchAgeNotUpdatingOnCharEditHint = "{=CharacterCreation_PatchAgeNotUpdatingOnCharEditHint}Enable age to be updated upon character edit. Does NOT require restart.",
            FixCharEditEffectOnNPCName = "{=CharacterCreation_FixCharEditEffectOnNPCName}Fix Character Edit Effect on NPCs",
            FixCharEditEffectOnNPCHint = "{=CharacterCreation_FixCharEditEffectOnNPCHint}Enable edits to NPC heroes to take affect. Does NOT require restart.",
            AddFaceGenValuesName = "{=CharacterCreation_AddFaceGenValuesName}Add FaceGen Values",
            AddFaceGenValuesHint = "{=CharacterCreation_AddFaceGenValuesHint}Add values to sliders in FaceGen so you can kind-of fine tune it. Does NOT require restart.",
            PatchSavePreviewGenderBugName = "{=CharacterCreation_PatchSavePreviewGenderBugName}Fix Incorrect Save Preview",
            PatchSavePreviewGenderBugHint = "{=CharacterCreation_PatchSavePreviewGenderBugHint}Enable this to fix heroes in save preview having incorrect morphs for certain edge cases. Disable if another mod also does this.",

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
            MaxAgeHint = "{=CharacterCreation_MaxAgeHint}Set the default max age. Does NOT require restart.",
            
            Section3 = "{=CharacterCreation_Section3}Section 3: Compatibility",
            EnableCompatibilityName = "{=CharacterCreation_EnableCompatibilityName}Enable Compatibility",
            EnableCompatibilityHint = "{=CharacterCreation_EnableCompatibilityHint}Enable compatibility patches to resolve mod conflicts. ALL options REQUIRES restart.",
            EnableCharacterReloadCompatibilityName = "{=CharacterCreation_EnableCharacterReloadCompatibilityName}Enable Character Reload Compatibility",
            EnableCharacterReloadCompatibilityHint = "{=CharacterCreation_EnableCharacterReloadCompatibilityHint}Enable compatibility patch to resolve mod conflicts with Character Reload.";

        private static readonly TextObject DisplayNameTextObject = new TextObject(DisplayNameText);
    }
}