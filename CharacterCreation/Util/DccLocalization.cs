using TaleWorlds.Localization;
using TaleWorlds.TwoDimension;

namespace CharacterCreation
{
    internal static class DccLocalization
    {
        internal static readonly TextObject
            // Buttons
            NativeYes = new TextObject("{=aeouhelq}Yes"),
            NativeNo = new TextObject("{=8OkPHu4f}No"),
            NativeCancel = new TextObject("{=3CpNUnVl}Cancel"),
            NativeContinue = new TextObject("{=DM6luo3c}Continue"),
            
            // Commands
            DccDisabledMsg = new TextObject("{=CharacterCreation_DccDisabledMsg}Detailed Character Creation disabled."),
            DccEnabledMsg = new TextObject("{=CharacterCreation_DccEnabledMsg}You have enabled Detailed Character Creation. Press V to access."),

            FormatMsgHeader = new TextObject("{=CharacterCreation_FormatMsgHeader}Format: "),
            HeroNameText = new TextObject("{=CharacterCreation_HeroNameText}HeroName"),
            AgeText = new TextObject("{=CharacterCreation_AgeText}Age"),

            HeroNotFoundMsg = new TextObject("{=CharacterCreation_HeroNotFoundMsg}Hero is not found"),
            EnterAgeMsg = new TextObject("{=CharacterCreation_EnterAgeMsg}Please enter an age."),
            SuccessMsg = new TextObject("{=CharacterCreation_SuccessMsg}Success"),
            HeroUpdatedMsg = new TextObject("{=CharacterCreation_HeroUpdatedMsg}Hero updated: "),
            
            // VM and UI
            DccOptionsText = new TextObject("{=CharacterCreation_DCCOptionsText}DCC Options: "),
            EditAppearanceText = new TextObject("{=CharacterCreation_EditAppearanceText}Edit Appearance"),
            ChangeNameText = new TextObject("{=CharacterCreation_ChangeNameText}Change Name"),
            UndoAppearanceText = new TextObject("{=CharacterCreation_UndoAppearanceText}Undo Appearance"),
            UndoRenameText = new TextObject("{=CharacterCreation_UndoRenameText}Undo Rename"),

            ChangingNameForText = new TextObject("{=CharacterCreation_ChangingNameForText}Changing name for: "),
            InvalidNameText = new TextObject("{=CharacterCreation_InvalidNameText}Name is not valid."),
            CharacterRenamerText = new TextObject("{=CharacterCreation_CharacterRenamerText}Character Renamer"),
            EnterNewNameText = new TextObject("{=CharacterCreation_EnterNewNameText}Enter a new name"),
            RenameText = new TextObject("{=CharacterCreation_RenameText}Rename"),
            CharacterUnrenamerText = new TextObject("{=CharacterCreation_CharacterUnrenamerText}Undo Troop Rename"),
            UnrenameWarningText = new TextObject("{=CharacterCreation_UnrenameWarningText}Do you want to undo the renaming of this troop unit? This will revert their name to what they were at game load."),
            CharacterUneditText = new TextObject("{=CharacterCreation_CharacterUneditText}Undo Troop Edit"),
            UneditWarningText = new TextObject("{=CharacterCreation_UneditWarningText}Do you want to undo the body edit of this troop unit? This will revert their appears to what they were at game load."),
            
            TroopEditTitle = new TextObject("{=CharacterCreation_TroopEditTitle}Edit Troop"),
            TroopEditText = new TextObject("{=CharacterCreation_TroopEditText}Bannerlord randomly generates a troop's body properties based on the range of body properties and the tags the troop are allowed to have. You can change either body properties or tags, but only one at a time. Avoid editing tags if you don't know what you are doing."),
            BodyPropertiesButton = new TextObject("{=CharacterCreation_BodyPropertiesButton}Body Properties"),
            TagsButton = new TextObject("{=CharacterCreation_TagsButton}Tags (Advanced)"),
            TroopEditPropertiesText = new TextObject("{=CharacterCreation_TroopEditPropertiesText}You can edit the 'minimum' or 'maximum' body properties of a troop. The troop as shown in the editor will have the average of both properties. NOTE: Beard, hair, and tattoo changes are ignored, but hair color changes are still respected."),
            MinPropertiesButton = new TextObject("{=CharacterCreation_MinPropertiesButton}Minimum"),
            MaxPropertiesButton = new TextObject("{=CharacterCreation_MaxPropertiesButton}Maximum"),
            TroopEditTagsText = new TextObject("{=CharacterCreation_TroopEditTagsText}THIS IS AN ADVANCED FEATURE! You can edit one of hair, beard, or tattoo tags. Ensure that the tags exist and are separated by a comma (,). (Space is fine if it's part of a tag.)"),
            HairTagsButton = new TextObject("{=CharacterCreation_HairTagsButton}Hair"),
            BeardTagsButton = new TextObject("{=CharacterCreation_BeardTagsButton}Beard"),
            TattooTagsButton = new TextObject("{=CharacterCreation_TattooTagsButton}Tattoo"),
            TagEditText = new TextObject("{=CharacterCreation_TagEditText}Ensure that the tags exist and are separated by a comma (,). (Space is fine if it's part of a tag.) Currently editing: {TAG_TYPE}");
    }
}