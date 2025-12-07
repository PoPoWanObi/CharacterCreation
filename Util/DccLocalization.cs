using TaleWorlds.Localization;
using TaleWorlds.TwoDimension;

namespace CharacterCreation
{
    internal static class DccLocalization
    {
        internal static readonly TextObject
            // Commands
            DccDisabledMsg = new TextObject("{=CharacterCreation_DccDisabledMsg}Detailed Character Creation disabled."),
            DccEnabledMsg = new TextObject("{=CharacterCreation_DccEnabledMsg}You have enabled Detailed Character Creation. Press V to access."),

            FormatMsgHeader = new TextObject("{=CharacterCreation_FormatMsgHeader}Format: "),
            HeroNameText = new TextObject("{=CharacterCreation_HeroNameText}HeroName"),
            AgeText = new TextObject("{=CharacterCreation_AgeText}Age"),

            HeroNotFoundMsg = new TextObject("{=CharacterCreation_HeroNotFoundMsg}Hero is not found"),
            EnterAgeMsg = new TextObject("{=CharacterCreation_EnterAgeMsg}Please enter an age."),
            SuccessMsg = new TextObject("{=CharacterCreation_SuccessMsg}Success"),
            
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
            TroopEditTitle = new TextObject("{=CharacterCreation_TroopEditTitle}A Note"),
            TroopEditText = new TextObject("{=CharacterCreation_TroopEditText}For troop editing, DCC will open the editor twice. The first time is for editing the minimum properties. The second time is for editing maximum properties. You can cancel editing to avoid editing one or the other, or both.");
    }
}