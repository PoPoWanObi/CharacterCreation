using System;
using CharacterCreation.Common.CampaignSystem;
using SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Common.Editor;

public static class CharacterEditorUtil
{
    private static CharacterEditorDelegateRef? _delegateRef;

    public static void SetDelegateRef(in CharacterEditorDelegateRef delegateRef)
    {
        if (_delegateRef.HasValue) return;
        _delegateRef = delegateRef;
    }

    public static void EditUnit(CharacterObject character, Action? postAction = null) =>
        _delegateRef?.EditUnitAction(character);

    public static void RenameUnit(CharacterObject character, Action? postAction = null) =>
        _delegateRef?.RenameUnitAction(character);

    public static void UndoEdit(CharacterObject character, Action? postAction = null) =>
        _delegateRef?.UndoEditAction(character);

    public static void UndoRename(CharacterObject character, Action? postAction = null) =>
        _delegateRef?.UndoRenameAction(character);

    public static void ApplyCharacterChanges(
        CharacterObject character,
        BodyProperties properties,
        int race,
        bool isFemale,
        BodyPropertyType propertyType = default
    ) => _delegateRef?.ApplyCharacterChangesAction(character, properties, race, isFemale, propertyType);
}