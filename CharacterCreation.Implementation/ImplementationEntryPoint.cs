using CharacterCreation.CampaignSystem;
using CharacterCreation.Compatibility;
using CharacterCreation.Models;
using CharacterCreation.Settings;
using CharacterCreation.UI;
using CharacterCreation.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace CharacterCreation
{
    public sealed class ImplementationEntryPoint : CharacterCreationEntryPoint
    {
        public override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            HarmonyInstance.PatchAll();

            // apply compatibility patches
            if (DccSettings.Instance!.EnableCompatibility)
                CompatibilityPatch.PatchAll(HarmonyInstance);

            FaceGen.ShowDebugValues = true; // Developer facegen
        }

        public override void OnGameLoaded(Game game, object initializerObject)
        {
            if (!(game.GameType is Campaign) || !DccSettings.Instance!.DebugMode) return;

            var player = Hero.MainHero;
            if (player is null) return;
            InformationManager.DisplayMessage(
                new InformationMessage(SubModule.GetFormattedAgeDebugMessage(player, player.Age),
                    ColorManager.Red));
            Debug.Print(SubModule.GetFormattedAgeDebugMessage(player, player.Age));
        }

        public override void OnGameInitializationFinished(Game game)
        {
            // just to make sure facegen is set
            FaceGen.ShowDebugValues = true;
            // make sure to call this and other daily tick events on... well, daily tick
            if (game.GameType is Campaign)
                SettingsEffects.Initialize(true);
        }

        public override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (!(game.GameType is Campaign) || !(gameStarterObject is CampaignGameStarter gameStarter))
                return;

            // add behaviors
            gameStarter.AddBehavior(new CharacterCreationCampaignBehavior());

            // add game models
            if (DccSettings.Instance!.CustomAgeModel)
                gameStarter.AddModel(new DccAgeModel());

            // add event handlers
            game.AddGameHandler<AgingGameHandler>();
            game.EventManager.RegisterEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedAction()
                .OnEncyclopediaPageChanged);
        }

        public override ICharacterEditorImplementation InitializeCharacterEditor(CharacterEditorState state) =>
            new CurrentCharacterEditorImplementation(state);

        public override ICampaignBehaviorImplementation InitializeCampaignBehavior() =>
            new CurrentCampaignBehaviorImplementation();
    }
}