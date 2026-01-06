using System;
using System.Collections.Generic;
using BUTR.MessageBoxPInvoke.Helpers;
using CharacterCreation.CampaignSystem;
using CharacterCreation.Compatibility;
using CharacterCreation.Models;
using CharacterCreation.Settings;
using CharacterCreation.UI;
using CharacterCreation.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using static CharacterCreation.Util.DccLocalization;
using FaceGen = TaleWorlds.Core.FaceGen;

namespace CharacterCreation
{
    public class SubModule : MBSubModuleBase
    {
        public static string GetFormattedAgeDebugMessage(Hero hero, float expectedAge)
        {
            var attributes = new Dictionary<string, object>
            {
                ["HERO_NAME"] = hero.Name,
                ["AGE1"] = expectedAge,
                ["AGE2"] = hero.Age
            };
            return new TextObject(ExpectedActualAgeMessage, attributes).ToString();
        }
        
        public static SubModule? Instance { get; private set; }

        private bool _isLoaded;

        private readonly Harmony _harmony;

        public SubModule()
        {
            _harmony = new Harmony("mod.bannerlord.popowanobi.dcc");
            Instance = this;
        }

        //Registers before the first module appears (main menu)
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            if (_isLoaded) return;

            base.OnSubModuleLoad();
            try
            {
                _harmony.PatchAll();

                // apply compatibility patches
                if (DccSettings.Instance!.EnableCompatibility)
                    CompatibilityPatch.PatchAll(_harmony);

                FaceGen.ShowDebugValues = true; // Developer facegen
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show(
                    $"{ErrorLoadingDccMessageTextObject}\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }

            InformationManager.DisplayMessage(new InformationMessage(LoadedModMessageTextObject.ToString(),
                ColorManager.Orange));
            _isLoaded = true;
        }

        // Called when loading save game
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            if (!(game.GameType is Campaign) || !DccSettings.Instance!.DebugMode) return;

            var player = Hero.MainHero;
            if (player is null) return;
            InformationManager.DisplayMessage(
                new InformationMessage(GetFormattedAgeDebugMessage(player, player.Age),
                    ColorManager.Red));
            Debug.Print(GetFormattedAgeDebugMessage(player, player.Age));
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
    }
}