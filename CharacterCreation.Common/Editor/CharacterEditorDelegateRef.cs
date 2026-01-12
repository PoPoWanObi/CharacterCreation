using System;
using CharacterCreation.Common.CampaignSystem;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Common.Editor;

public delegate void UnitChangeAction(CharacterObject character, Action? postAction = null);

public readonly struct CharacterEditorDelegateRef
{
    public readonly UnitChangeAction
        EditUnitAction,
        RenameUnitAction,
        UndoEditAction,
        UndoRenameAction;

    public readonly Action<CharacterObject, BodyProperties, int, bool, BodyPropertyType>
        ApplyCharacterChangesAction;

    public CharacterEditorDelegateRef(
        UnitChangeAction editUnitAction,
        UnitChangeAction renameUnitAction,
        UnitChangeAction undoEditAction,
        UnitChangeAction undoRenameAction,
        Action<CharacterObject, BodyProperties, int, bool, BodyPropertyType> applyCharacterChangesAction
    )
    {
        EditUnitAction = editUnitAction;
        RenameUnitAction = renameUnitAction;
        UndoEditAction = undoEditAction;
        UndoRenameAction = undoRenameAction;
        ApplyCharacterChangesAction = applyCharacterChangesAction;
    }
}