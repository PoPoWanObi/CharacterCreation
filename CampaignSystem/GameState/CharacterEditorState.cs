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
        public bool EditMaxProperties;

        public override bool IsMenuState => true;
        
        public IFaceGeneratorCustomFilter Filter { get; private set; }

        public CharacterEditorState() : this(Hero.MainHero.CharacterObject) {}

        public CharacterEditorState(BasicCharacterObject character) : this(character,
            CharacterHelper.GetFaceGeneratorFilter())
        {
        }
        
        public CharacterEditorState(BasicCharacterObject character, IFaceGeneratorCustomFilter filter) : this(character,
            filter, false)
        {
        }

        public CharacterEditorState(BasicCharacterObject character, IFaceGeneratorCustomFilter filter,
            bool editMaxProperties)
        {
            Character = character;
            Filter = filter;
            EditMaxProperties = editMaxProperties;
        }
    }
}