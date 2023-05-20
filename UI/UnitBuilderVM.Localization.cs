using System.Collections.Generic;
using TaleWorlds.Localization;

namespace CharacterCreation.UI
{
    public partial class UnitBuilderVM
    {
        internal static readonly TextObject
            DCCOptionsText = new TextObject("{=CharacterCreation_DCCOptionsText}DCC Options: "),
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
            UneditWarningText = new TextObject("{=CharacterCreation_UneditWarningText}Do you want to undo the body edit of this troop unit? This will revert their appears to what they were at game load.");
    }
}
