using CharacterCreation.Common.CampaignSystem;
using CharacterCreation.Common.Editor;
using CharacterCreation.Common.Settings;
using CharacterCreation.Common.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.Common.UI;

[GameStateScreen(typeof(CharacterEditorState))]
public class GauntletCharacterEditorScreen : ScreenBase, IGameStateListener, IFaceGeneratorScreen
{
    private readonly BodyGeneratorView _facegenLayer;
    private readonly BodyPropertyType _editedPropertyType;

    public IFaceGeneratorHandler Handler => _facegenLayer;

    public GauntletCharacterEditorScreen(CharacterEditorState state)
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

    protected override void OnFrameTick(float dt) => _facegenLayer.OnTick(dt);

    public void OnDone()
    {
        if (_facegenLayer.BodyGen.Character is CharacterObject character)
            // yes, it's possible this has already been done when BodyGenerator.SaveCharacter was called
            // but doing it again doesn't hurt, just in case
            CharacterEditorUtil.ApplyCharacterChanges(character, _facegenLayer.BodyGen.CurrentBodyProperties,
                _facegenLayer.BodyGen.Race, _facegenLayer.BodyGen.IsFemale, _editedPropertyType);
        Game.Current.GameStateManager.PopState();
    }

    public void OnExit() => Game.Current.GameStateManager.PopState();

    protected override void OnInitialize()
    {
        base.OnInitialize();
        Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
        AddLayer(_facegenLayer.GauntletLayer);
        if (!DccSettings.Instance!.DebugMode) InformationManager.HideAllMessages();
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
        if (!DccSettings.Instance!.DebugMode) MBInformationManager.HideInformations();
    }
        
    void IGameStateListener.OnActivate() {}
    void IGameStateListener.OnDeactivate() {}
    void IGameStateListener.OnInitialize() {}
    void IGameStateListener.OnFinalize() {}
}