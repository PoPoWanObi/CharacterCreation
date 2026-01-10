using TaleWorlds.Localization;

namespace CharacterCreation.Util
{
    public static class DccLocalization
    {
        public const string
            // Buttons
            NativeYes = "{=aeouhelq}Yes",
            NativeNo = "{=8OkPHu4f}No",
            NativeCancel = "{=3CpNUnVl}Cancel",
            NativeContinue = "{=DM6luo3c}Continue",
            
            // SubModule
            LoadedModMessage = "{=CharacterCreation_LoadedModMessage}Loaded Detailed Character Creation.",
            EditAppearanceForHeroMessage = "{=CharacterCreation_EditAppearanceForHeroMessage}Entering edit appearance for: ",
            ErrorLoadingDccMessage = "{=CharacterCreation_ErrorLoadingDccMessage}Error initializing Detailed Character Creation:",
            
            ExpectedActualAgeMessage = "{=CharacterCreation_ExpectedActualAgeMessage}[Debug] Hero {HERO_NAME} expected age: {AGE1}, actual age: {AGE2}",
            
            // Commands
            DccDisabledMsg = "{=CharacterCreation_DccDisabledMsg}Detailed Character Creation disabled.",
            DccEnabledMsg = "{=CharacterCreation_DccEnabledMsg}You have enabled Detailed Character Creation. Press V to access.",
            
            FormatMsgHeader = "{=CharacterCreation_FormatMsgHeader}Format: ",
            HeroNameText = "{=CharacterCreation_HeroNameText}HeroName",
            AgeText = "{=CharacterCreation_AgeText}Age",
            
            HeroNotFoundMsg = "{=CharacterCreation_HeroNotFoundMsg}Hero is not found",
            EnterAgeMsg = "{=CharacterCreation_EnterAgeMsg}Please enter an age.",
            SuccessMsg = "{=CharacterCreation_SuccessMsg}Success",
            HeroUpdatedMsg = "{=CharacterCreation_HeroUpdatedMsg}Hero updated: ",
            
            // VM and UI
            DccOptionsText = "{=CharacterCreation_DCCOptionsText}DCC Options: ",
            EditAppearanceText = "{=CharacterCreation_EditAppearanceText}Edit Appearance",
            ChangeNameText = "{=CharacterCreation_ChangeNameText}Change Name",
            UndoAppearanceText = "{=CharacterCreation_UndoAppearanceText}Undo Appearance",
            UndoRenameText = "{=CharacterCreation_UndoRenameText}Undo Rename",
            
            ChangingNameForText = "{=CharacterCreation_ChangingNameForText}Changing name for: ",
            InvalidNameText = "{=CharacterCreation_InvalidNameText}Name is not valid.",
            CharacterRenamerText = "{=CharacterCreation_CharacterRenamerText}Character Renamer",
            EnterNewNameText = "{=CharacterCreation_EnterNewNameText}Enter a new name",
            RenameText = "{=CharacterCreation_RenameText}Rename",
            CharacterUnrenamerText = "{=CharacterCreation_CharacterUnrenamerText}Undo Troop Rename",
            UnrenameWarningText = "{=CharacterCreation_UnrenameWarningText}Do you want to undo the renaming of this troop unit? This will revert their name to what they were at game load.",
            CharacterUneditText = "{=CharacterCreation_CharacterUneditText}Undo Troop Edit",
            UneditWarningText = "{=CharacterCreation_UneditWarningText}Do you want to undo the body edit of this troop unit? This will revert their appears to what they were at game load.",
            
            TroopEditTitle = "{=CharacterCreation_TroopEditTitle}Edit Troop",
            TroopEditText = "{=CharacterCreation_TroopEditText}Bannerlord randomly generates a troop's body properties based on the range of body properties and the tags the troop are allowed to have. You can change either body properties or tags, but only one at a time. Avoid editing tags if you don't know what you are doing.",
            BodyPropertiesButton = "{=CharacterCreation_BodyPropertiesButton}Body Properties",
            TagsButton = "{=CharacterCreation_TagsButton}Tags (Advanced)",
            TroopEditPropertiesText = "{=CharacterCreation_TroopEditPropertiesText}You can edit the 'minimum' or 'maximum' body properties of a troop. The troop as shown in the editor will have the average of both properties. NOTE: Beard, hair, and tattoo changes are ignored, but hair color changes are still respected.",
            MinPropertiesButton = "{=CharacterCreation_MinPropertiesButton}Minimum",
            MaxPropertiesButton = "{=CharacterCreation_MaxPropertiesButton}Maximum",
            TroopEditTagsText = "{=CharacterCreation_TroopEditTagsText}THIS IS AN ADVANCED FEATURE! You can edit one of hair, beard, or tattoo tags. Ensure that the tags exist and are separated by a comma (,). (Space is fine if it's part of a tag.)",
            HairTagsButton = "{=CharacterCreation_HairTagsButton}Hair",
            BeardTagsButton = "{=CharacterCreation_BeardTagsButton}Beard",
            TattooTagsButton = "{=CharacterCreation_TattooTagsButton}Tattoo",
            TagEditText = "{=CharacterCreation_TagEditText}Ensure that the tags exist and are separated by a comma (,). (Space is fine if it's part of a tag.) Currently editing: {TAG_TYPE}",
            
            // Settings
            DisplayNameText = "{=CharacterCration_ModNameText}Detailed Character Creation",
            
            Section0 = "{=CharacterCreation_Section0}Section 0: Debug Mode",
            DebugModeName = "{=CharacterCreation_DebugModeName}Enable debug output",
            DebugModeHint = "{=CharacterCreation_DebugModeHint}Enable DCC's debug output. Does NOT require restart.",
            OptionsLabelName = "{=CharacterCreation_OptionsLabelName}Enable options label",
            OptionsLabelHint = "{=CharacterCreation_OptionsLabelHint}Show DCC Options string in Encyclopedia.",

            Section1 = "{=CharacterCreation_Section1}Section 1: Overrides",
            IgnoreDailyTickName = "{=CharacterCreation_IgnoreDailyTickName}Suppress Daily Tick",
            IgnoreDailyTickHint = "{=CharacterCreation_IgnoreDailyTickHint}Keep this on to prevent the game from reverting your appearance. Global settings only apply to new saves (and old saves not already using DCC). Does NOT require restart.",
            PatchPlayerComingOfAgeIssuesName = "{=CharacterCreation_PatchPlayerComingOfAgeIssuesName}Fix Issues from Player Coming of Age",
            PatchPlayerComingOfAgeIssuesHint = "{=CharacterCreation_PatchPlayerComingOfAgeIssuesHint}Corrects issues related to the player coming of age, specifically over equipment and locations. Does NOT require restart.",
            AddFaceGenValuesName = "{=CharacterCreation_AddFaceGenValuesName}Add FaceGen Values",
            AddFaceGenValuesHint = "{=CharacterCreation_AddFaceGenValuesHint}Add values to sliders in FaceGen so you can kind-of fine tune it. Does NOT require restart.",
            PatchSavePreviewGenderBugName = "{=CharacterCreation_PatchSavePreviewGenderBugName}Fix Incorrect Save Preview",
            PatchSavePreviewGenderBugHint = "{=CharacterCreation_PatchSavePreviewGenderBugHint}Enable this to fix heroes in save preview having incorrect morphs for certain edge cases. REQUIRES restart.",
            
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
            EnableCharacterReloadCompatibilityHint = "{=CharacterCreation_EnableCharacterReloadCompatibilityHint}Enable compatibility patch to resolve mod conflicts with Character Reload.",
            
            // Per-Save Settings
            PerSaveDisplayNameText = "{=CharacterCration_ModNameText}Detailed Character Creation (Per-Save)",
            OverrideAgeName = "{=CharacterCreation_OverrideAgeName}Override Age",
            OverrideAgeHint = "{=CharacterCreation_OverrideAgeHint}When enabled, this will prevent the game from aging the player hero. Does NOT require restart and takes effect upon save load or daily. Overridden by 'Disable Auto Aging'.",
            DisableAutoAgingName = "{=CharacterCreation_DisableAutoAgingName}Disable Auto Aging",
            DisableAutoAgingHint = "{=CharacterCreation_DisableAutoAgingHint}Enable this to prevent the game from changing the age physical appearance. Does NOT require restart and takes effect upon save load or hour tick.",
            
            // Settings Effect
            WarningTitle = "{=CharacterCreation_WarningTitle}WARNING",
            WarningAck = "{=CharacterCreation_WarningAck}Acknowledged",
            WarningConflictText = "{=CharacterCreation_WarningConflictText}Override Age is currently set to {OA}, but Disable Life and Death Cycle {LDC} is turned on. This settings will not be applied.";
        
        public static readonly TextObject
            // Buttons
            NativeYesTextObject = new TextObject(NativeYes),
            NativeNoTextObject = new TextObject(NativeNo),
            NativeCancelTextObject = new TextObject(NativeCancel),
            NativeContinueTextObject = new TextObject(NativeContinue),
            
            // SubModule
            LoadedModMessageTextObject = new TextObject(LoadedModMessage),
            EditAppearanceForHeroMessageTextObject = new TextObject(EditAppearanceForHeroMessage),
            ErrorLoadingDccMessageTextObject = new TextObject(ErrorLoadingDccMessage),

            // Commands
            DccDisabledMsgTextObject = new TextObject(DccDisabledMsg),
            DccEnabledMsgTextObject = new TextObject(DccEnabledMsg),

            FormatMsgHeaderTextObject = new TextObject(FormatMsgHeader),
            HeroNameTextTextObject = new TextObject(HeroNameText),
            AgeTextTextObject = new TextObject(AgeText),

            HeroNotFoundMsgTextObject = new TextObject(HeroNotFoundMsg),
            EnterAgeMsgTextObject = new TextObject(EnterAgeMsg),
            SuccessMsgTextObject = new TextObject(SuccessMsg),
            HeroUpdatedMsgTextObject = new TextObject(HeroUpdatedMsg),

            // VM and UI
            DccOptionsTextTextObject = new TextObject(DccOptionsText),
            EditAppearanceTextTextObject = new TextObject(EditAppearanceText),
            ChangeNameTextTextObject = new TextObject(ChangeNameText),
            UndoAppearanceTextTextObject = new TextObject(UndoAppearanceText),
            UndoRenameTextTextObject = new TextObject(UndoRenameText),

            ChangingNameForTextTextObject = new TextObject(ChangingNameForText),
            InvalidNameTextTextObject = new TextObject(InvalidNameText),
            CharacterRenamerTextTextObject = new TextObject(CharacterRenamerText),
            EnterNewNameTextTextObject = new TextObject(EnterNewNameText),
            RenameTextTextObject = new TextObject(RenameText),
            CharacterUnrenamerTextTextObject = new TextObject(CharacterUnrenamerText),
            UnrenameWarningTextTextObject = new TextObject(UnrenameWarningText),
            CharacterUneditTextTextObject = new TextObject(CharacterUneditText),
            UneditWarningTextTextObject = new TextObject(UneditWarningText),

            TroopEditTitleTextObject = new TextObject(TroopEditTitle),
            TroopEditTextTextObject = new TextObject(TroopEditText),
            BodyPropertiesButtonTextObject = new TextObject(BodyPropertiesButton),
            TagsButtonTextObject = new TextObject(TagsButton),
            TroopEditPropertiesTextTextObject = new TextObject(TroopEditPropertiesText),
            MinPropertiesButtonTextObject = new TextObject(MinPropertiesButton),
            MaxPropertiesButtonTextObject = new TextObject(MaxPropertiesButton),
            TroopEditTagsTextTextObject = new TextObject(TroopEditTagsText),
            HairTagsButtonTextObject = new TextObject(HairTagsButton),
            BeardTagsButtonTextObject = new TextObject(BeardTagsButton),
            TattooTagsButtonTextObject = new TextObject(TattooTagsButton),
            TagEditTextTextObject = new TextObject(TagEditText),

            // Settings
            DisplayNameTextObject = new TextObject(DisplayNameText),
            PerSaveDisplayNameTextObject = new TextObject(PerSaveDisplayNameText),

            // Settings Effects
            WarningTitleTextObject = new TextObject(WarningTitle),
            WarningAckTextObject = new TextObject(WarningAck);
    }
}