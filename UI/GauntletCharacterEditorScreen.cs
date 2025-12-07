using CharacterCreation.CampaignSystem;
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

using static CharacterCreation.DccLocalization;

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
            ApplyChanges();
            Game.Current.GameStateManager.PopState();
        }

        private void ApplyChanges()
        {
            if (!DCCSettingsUtil.Instance.FixCharEditEffectOnNPC) return;
            if (!(_facegenLayer.BodyGen.Character is CharacterObject character)) return;
            var properties = _facegenLayer.BodyGen.CurrentBodyProperties;
            var race = _facegenLayer.BodyGen.Race;
            var isFemale = _facegenLayer.BodyGen.IsFemale;

            // apply age changes
            if (DCCSettingsUtil.Instance.PatchAgeNotUpdatingOnCharEdit)
            {
                var bodyAge = properties.DynamicProperties.Age;
                UnitEditorFunctions.ResetBirthDayForAge(character, bodyAge);
                if (DCCSettingsUtil.Instance.DebugMode)
                {
                    var msg =
                        $"[CharacterCreation] Character {character.Name} expected age: {bodyAge}, actual: {character.Age}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                }
            }
            
            // apply body changes
            if (DCCSettingsUtil.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Preparing edit to {character.GetName()}. Properties: ({character.GetBodyProperties(character.Equipment).ToString()}), Race: {character.Race.ToString()}, Female: {character.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
                msg =
                    $"[CharacterCreation] Applying changes to {character.GetName()}. Properties: ({properties.ToString()}), Race: {race.ToString()}, Female: {isFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
            
            if (character.IsHero)
            {
                //PropertyInfo staticBodyPropertyInfoOnHero = AccessTools.Property(typeof(Hero), "StaticBodyProperties");
                //staticBodyPropertyInfoOnHero.SetValue(__instance.HeroObject, properties.StaticProperties);
                character.HeroObject.StaticBodyProperties = properties.StaticProperties;
                character.HeroObject.Weight = properties.Weight;
                character.HeroObject.Build = properties.Build;
                character.Race = race;
                character.HeroObject.IsFemale = isFemale;
                CampaignEventDispatcher.Instance.OnPlayerBodyPropertiesChanged();
                
                if (DCCSettingsUtil.Instance.DebugMode)
                {
                    var msg = HeroUpdatedMsg.ToString() + character.HeroObject.Name;
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Purple));
                    Debug.Print(msg);
                }
            }
            else
            {
                CharacterCreationCampaignBehavior.Instance?.SetBodyPropertiesOverride(character, properties, race, isFemale);
            }
            
            if (DCCSettingsUtil.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Changes applied to {character.GetName()}. Properties: ({character.GetBodyProperties(character.Equipment).ToString()}), Race: {character.Race.ToString()}, Female: {character.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
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