using System.Collections.Generic;
using TaleWorlds.Localization;

namespace CharacterCreation.Models
{
    public partial class UnitBuilderVM
    {
        internal static readonly TextObject
            ChangingNameForText = new TextObject("{=CharacterCreation_ChangingNameForText}Changing name for: "),
            InvalidCharacterText = new TextObject("{=CharacterCreation_InvalidCharacterText}Character is not valid."),
            InvalidNameText = new TextObject("{=CharacterCreation_InvalidNameText}Name is not valid."),
            CharacterRenamerText = new TextObject("{=CharacterCreation_CharacterRenamerText}Character Renamer"),
            EnterNewNameText = new TextObject("{=CharacterCreation_EnterNewNameText}Enter a new name"),
            RenameText = new TextObject("{=CharacterCreation_RenameText}Rename"),
            CannotRenamePlayerText = new TextObject("{=CharacterCreation_CannotRenamePlayerText}Cannot rename player hero until further notice."),
            EditBodyText = new TextObject("{=CharacterCreation_EditBodyText}Edit Body"),
            EditMinBodyText = new TextObject("{=CharacterCreation_EditMinBodyText}Edit minimum body properties?\nIf not, you will be asked about maximum body properties."),
            EditMaxBodyText = new TextObject("{=CharacterCreation_EditMaxBodyText}Edit maximum body properties?");
    }
}
