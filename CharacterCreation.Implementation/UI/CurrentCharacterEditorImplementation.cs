using CharacterCreation.CampaignSystem;
using CharacterCreation.Settings;
using CharacterCreation.Util;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.ScreenSystem;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.UI
{
    // Adapted from SandBox.GauntletUI.GauntletBarberScreen
    // the original version is for some reason hardcoded to use the player character (probably because the barber state no-arg constructor is malformed)
    public sealed class CurrentCharacterEditorImplementation : ICharacterEditorImplementation
    {
        private readonly BodyGeneratorView _facegenLayer;
        private readonly CharacterEditorStatePropertyType _editedPropertyType;
        
        public IFaceGeneratorHandler Handler => _facegenLayer;
        
        public CurrentCharacterEditorImplementation(CharacterEditorState state)
        {
            LoadingWindow.EnableGlobalLoadingWindow();
            _editedPropertyType = state.EditedPropertyType;
            _facegenLayer = new BodyGeneratorView(OnDone, GameTexts.FindText("str_done"), OnExit,
                GameTexts.FindText("str_cancel"), state.Character, false, new FaceGeneratorFilter(state.Character));
            if (DccSettings.Instance!.DebugMode)
            {
                var msg = $"[CharacterCreation] Initializing character editor screen, character: {state.Character}, which to edit: {_editedPropertyType}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
        }
        
        void ICharacterEditorImplementation.OnFrameTick(float dt)
        {
            _facegenLayer.OnTick(dt);
        }

        public void OnDone()
        {
            ApplyChanges();
            Game.Current.GameStateManager.PopState();
        }

        private void ApplyChanges()
        {
            if (!DccSettings.Instance!.FixCharEditEffectOnNpc) return;
            if (!(_facegenLayer.BodyGen.Character is CharacterObject character)) return;
            var properties = _facegenLayer.BodyGen.CurrentBodyProperties;
            var race = _facegenLayer.BodyGen.Race;
            var isFemale = _facegenLayer.BodyGen.IsFemale;

            // apply age changes
            if (DccSettings.Instance.PatchAgeNotUpdatingOnCharEdit)
            {
                var bodyAge = properties.DynamicProperties.Age;
                UnitEditorFunctions.ResetBirthDayForAge(character, bodyAge);
                if (DccSettings.Instance.DebugMode)
                {
                    var msg =
                        $"[CharacterCreation] Character {character.Name} expected age: {bodyAge}, actual: {character.Age}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                }
            }
            
            // apply body changes
            if (DccSettings.Instance.DebugMode)
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
                
                if (DccSettings.Instance.DebugMode)
                {
                    var msg = HeroUpdatedMsgTextObject.ToString() + character.HeroObject.Name;
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Purple));
                    Debug.Print(msg);
                }
            }
            else
            {
                CharacterCreationCampaignBehavior.Instance?.SetBodyPropertiesOverride(character, properties, race,
                    isFemale, _editedPropertyType);
            }
            
            if (DccSettings.Instance.DebugMode)
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

        void ICharacterEditorImplementation.OnInitialize(ScreenBase screen)
        {
            Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
            screen.AddLayer(_facegenLayer.GauntletLayer);
            if (!DccSettings.Instance!.DebugMode) InformationManager.HideAllMessages();
        }

        void ICharacterEditorImplementation.OnFinalize(ScreenBase screen)
        {
            if (LoadingWindow.IsLoadingWindowActive)
                LoadingWindow.DisableGlobalLoadingWindow();
            Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
        }

        void ICharacterEditorImplementation.OnActivate(ScreenBase screen) => screen.AddLayer(_facegenLayer.SceneLayer);

        void ICharacterEditorImplementation.OnDeactivate(ScreenBase screen)
        {
            _facegenLayer.SceneLayer.SceneView.SetEnable(false);
            _facegenLayer.OnFinalize();
            LoadingWindow.EnableGlobalLoadingWindow();
            if (!DccSettings.Instance!.DebugMode) MBInformationManager.HideInformations();
        }
        
        void IGameStateListener.OnActivate() {}
        void IGameStateListener.OnDeactivate() {}
        void IGameStateListener.OnInitialize() {}
        void IGameStateListener.OnFinalize() {}
    }
}