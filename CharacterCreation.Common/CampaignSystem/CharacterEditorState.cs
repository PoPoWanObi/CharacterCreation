using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.Common.CampaignSystem;

// Adapted from TaleWorlds.CampaignSystem.GameState.BarberState
// each state can only have an associated screen, so this had to be cloned
public class CharacterEditorState : GameState
{
    public BasicCharacterObject Character;
    public BodyPropertyType EditedPropertyType;

    public override bool IsMenuState => true;

    public CharacterEditorState() : this(Hero.MainHero.CharacterObject) {}

    public CharacterEditorState(BasicCharacterObject character) : this(character, BodyPropertyType.MinProperties) {}

    public CharacterEditorState(BasicCharacterObject character, BodyPropertyType editedPropertyType)
    {
        Character = character;
        EditedPropertyType = editedPropertyType;
    }
}

[Flags]
public enum BodyPropertyType
{
    MinProperties = 0b01,
    MaxProperties = 0b10
}