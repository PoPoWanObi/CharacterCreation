using TaleWorlds.Localization;

namespace CharacterCreation.Models
{
    public partial class HeroBuilderVM
    {
        internal static readonly TextObject 
            ChangingNameForText = new TextObject("{=CharacterCreation_ChangingNameForText}Changing name for: "),
            InvalidCharacterText = new TextObject("{=CharacterCreation_InvalidCharacterText}Character is not valid."),
            InvalidNameText = new TextObject("{=CharacterCreation_InvalidNameText}Name is not valid."),
            CharacterRenamerText = new TextObject("{=CharacterCreation_CharacterRenamerText}Character Renamer"),
            EnterNewNameText = new TextObject("{=CharacterCreation_EnterNewNameText}Enter a new name"),
            RenameText = new TextObject("{=CharacterCreation_RenameText}Rename"),
            CancelText = new TextObject("{=CharacterCreation_CancelText}Cancel"),
            CannotRenamePlayerText = new TextObject("{=CharacterCreation_CannotRenamePlayerText}Cannot rename player hero until further notice.");
    }
}
