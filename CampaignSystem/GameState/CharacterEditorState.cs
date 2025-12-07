using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace CharacterCreation.CampaignSystem.GameState
{
    // Adapted from TaleWorlds.CampaignSystem.GameState.BarberState
    // each state can only have an associated screen, so this had to be cloned
    public class CharacterEditorState : TaleWorlds.Core.GameState
    {
        public BasicCharacterObject Character;
        public CharacterEditorStatePropertyType EditedPropertyType;

        public override bool IsMenuState => true;

        public CharacterEditorState() : this(Hero.MainHero.CharacterObject) {}

        public CharacterEditorState(BasicCharacterObject character) : this(character, CharacterEditorStatePropertyType.MinProperties) {}

        public CharacterEditorState(BasicCharacterObject character, CharacterEditorStatePropertyType editedPropertyType)
        {
            Character = character;
            EditedPropertyType = editedPropertyType;
        }
    }

    [Flags]
    public enum CharacterEditorStatePropertyType
    {
        MinProperties = 0b01,
        MaxProperties = 0b10
    }
}