using CharacterCreation.CampaignSystem.GameState;
using CharacterCreation.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.UI
{
    // Adapted from SandBox.GauntletUI.GauntletBarberScreen
    // the original version is for some reason hardcoded to use the player character (probably because the barber state no-arg constructor is malformed)
    [GameStateScreen(typeof(CharacterEditorState))]
    public class GauntletCharacterEditorScreen : ScreenBase, IGameStateListener, IFaceGeneratorScreen
    {
        private readonly BodyGeneratorView _facegenLayer;
        private bool _useMaxProperties;

        public IFaceGeneratorHandler Handler => _facegenLayer;

        public GauntletCharacterEditorScreen(CharacterEditorState state)
        {
            LoadingWindow.EnableGlobalLoadingWindow();
            _useMaxProperties = state.EditMaxProperties;
            _facegenLayer = new BodyGeneratorView(OnDone, GameTexts.FindText("str_done"), OnExit,
                GameTexts.FindText("str_cancel"), state.Character, false, state.Filter);
        }

        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
            _facegenLayer.OnTick(dt);
        }

        public void OnDone()
        {
            var bodyGenerator = _facegenLayer.BodyGen;
            if (DCCSettingsUtil.Instance.PatchAgeNotUpdatingOnCharEdit && bodyGenerator.Character is CharacterObject characterObject)
            {
                var bodyAge = bodyGenerator.CurrentBodyProperties.DynamicProperties.Age;
                UnitEditorFunctions.ResetBirthDayForAge(characterObject, bodyAge);
                if (DCCSettingsUtil.Instance.DebugMode)
                {
                    var msg =
                        $"[CharacterCreation] Character {characterObject.Name} expected age: {bodyAge}, actual: {characterObject.Age}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                }
            }
            
            Game.Current.GameStateManager.PopState();
        }

        public void OnExit()
        {
            Game.Current.GameStateManager.PopState();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
            AddLayer(_facegenLayer.GauntletLayer);
            if (!DCCSettingsUtil.Instance.DebugMode) InformationManager.HideAllMessages();
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            if (LoadingWindow.IsLoadingWindowActive)
                LoadingWindow.DisableGlobalLoadingWindow();
            Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            AddLayer(_facegenLayer.SceneLayer);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _facegenLayer.SceneLayer.SceneView.SetEnable(false);
            _facegenLayer.OnFinalize();
            LoadingWindow.EnableGlobalLoadingWindow();
            if (!DCCSettingsUtil.Instance.DebugMode) MBInformationManager.HideInformations();
        }
        
        void IGameStateListener.OnActivate() {}
        void IGameStateListener.OnDeactivate() {}
        void IGameStateListener.OnInitialize() {}
        void IGameStateListener.OnFinalize() {}
    }
}