using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.UI
{
    public interface ICharacterEditorImplementation : IFaceGeneratorScreen, IGameStateListener
    {
        void OnInitialize(ScreenBase screen);

        void OnFinalize(ScreenBase screen);

        void OnActivate(ScreenBase screen);

        void OnDeactivate(ScreenBase screen);
        
        void OnFrameTick(float dt);
    }
}