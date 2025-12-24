using CharacterCreation.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.UI
{
    // Shell code
    // (implementation is in CharacterCreation.Implementation:CharacterCreation.UI.CurrentCharacterEditorImplementation)
    [GameStateScreen(typeof(CharacterEditorState))]
    public class GauntletCharacterEditorScreen : ScreenBase, IGameStateListener, IFaceGeneratorScreen
    {
        private readonly ICharacterEditorImplementation _implementation;

        public IFaceGeneratorHandler Handler => _implementation.Handler;

        public GauntletCharacterEditorScreen(CharacterEditorState state) =>
            _implementation = SubModule.Instance!.EntryPoint.InitializeCharacterEditor(state);

        protected override void OnFrameTick(float dt) => _implementation.OnFrameTick(dt);

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _implementation.OnInitialize(this);
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            _implementation.OnFinalize(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _implementation.OnActivate(this);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _implementation.OnDeactivate(this);
        }
        
        void IGameStateListener.OnActivate() => _implementation.OnActivate();
        void IGameStateListener.OnDeactivate() => _implementation.OnDeactivate();
        void IGameStateListener.OnInitialize() => _implementation.OnInitialize(this);
        void IGameStateListener.OnFinalize() => _implementation.OnFinalize(this);
    }
}