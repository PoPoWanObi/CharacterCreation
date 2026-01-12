using System;
using CharacterCreation.Common.CampaignSystem;
using CharacterCreation.Common.Util;
using CharacterCreation.Implementation.CampaignSystem;
using CharacterCreation.Implementation.Editor;
using CharacterCreation.Implementation.UI;
using CharacterCreation.Implementation.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using static CharacterCreation.Common.Util.DccLocalization;
using FaceGen = TaleWorlds.Core.FaceGen;

namespace CharacterCreation.Implementation;

public class SubModule : MBSubModuleBase
{
    private bool _isLoaded;
    private readonly Harmony _harmony;
    
    public SubModule()
    {
        CharacterCreationCampaignBehavior.SetDelegateRef(CampaignSystemUtil.GetDelegateRefs());
        Common.Editor.CharacterEditorUtil.SetDelegateRef(CharacterEditorUtil.GetDelegateRefs());
        _harmony = new Harmony("mod.bannerlord.popowanobi.dcc");
    }
    
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
    }

    protected override void OnSubModuleUnloaded()
    {
        base.OnSubModuleUnloaded();
    }

    protected override void OnBeforeInitialModuleScreenSetAsRoot()
    {
        base.OnBeforeInitialModuleScreenSetAsRoot();
        if (_isLoaded) return;

        base.OnSubModuleLoad();
        try
        {
            _harmony.PatchAll();

            // apply compatibility patches - remake when new patches show up
            // if (DccSettings.Instance!.EnableCompatibility)
            //     CompatibilityPatch.PatchAll(_harmony);

            FaceGen.ShowDebugValues = true; // Developer facegen
        }
        catch (Exception ex)
        {
            var msg = $"[CharacterCreation] {ErrorLoadingDccMessageTextObject}\n{ex.Message} \n\n{ex.InnerException?.Message}";
            Debug.Print(msg);
            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
        }

        InformationManager.DisplayMessage(new InformationMessage(LoadedModMessageTextObject.ToString(),
            ColorManager.Orange));
        _isLoaded = true;
    }

    // called after game is initialized
    public override void OnGameInitializationFinished(Game game)
    {
        // just to make sure facegen is set
        FaceGen.ShowDebugValues = true;
        // make sure to call this and other daily tick events on... well, daily tick
        if (game.GameType is Campaign)
            SettingsEffects.Initialize(true);
    }

    protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
    {
        base.OnGameStart(game, gameStarterObject);
        
        // add event handlers
        game.AddGameHandler<AgingGameHandler>();
        game.EventManager.RegisterEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedAction()
            .OnEncyclopediaPageChanged);
    }
}