﻿using CharacterCreation.Models;
using CharacterCreation.Util;
using CharacterCreation.UI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using Bannerlord.BUTR.Shared.Helpers;
using BUTR.MessageBoxPInvoke.Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using CharacterCreation.CampaignSystem;

namespace CharacterCreation
{
    public class SubModule : MBSubModuleBase
    {
        internal static readonly TextObject LoadedModMessage = new TextObject("{=CharacterCreation_LoadedModMessage}Loaded Detailed Character Creation."),
            EditAppearanceForHeroMessage = new TextObject("{=CharacterCreation_EditAppearanceForHeroMessage}Entering edit appearance for: "),
            ErrorLoadingDccMessage = new TextObject("{=CharacterCreation_ErrorLoadingDccMessage}Error initializing Detailed Character Creation:");

        private const string ExpectedActualAgeMessage = "{=CharacterCreation_ExpectedActualAgeMessage}[Debug] Hero {HERO_NAME} expected age: {AGE1}, actual age: {AGE2}";

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

        //Registers before the first module appears (main menu)
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            if (_isLoaded) return;

            base.OnSubModuleLoad();
            try
            {
                var harmony = new Harmony("mod.bannerlord.popowanobi.dcc");
                harmony.PatchAll();

                // apply compatibility patches
                CompatibilityPatch.CreateCompatibilityPatches(harmony);

                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Developer facegen
            }
            catch (Exception ex)
            {
                MessageBoxDialog.Show($"{ErrorLoadingDccMessage}\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }

            InformationManager.DisplayMessage(new InformationMessage(LoadedModMessage.ToString(), ColorManager.Orange));
            _isLoaded = true;
        }

        // Called when loading save game
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            if (!(game.GameType is Campaign) || !DCCSettingsUtil.Instance.DebugMode) return;

            var player = Hero.MainHero;
            if (player != default)
            {
                InformationManager.DisplayMessage(new InformationMessage(GetFormattedAgeDebugMessage(player, player.Age), ColorManager.Red));
                Debug.Print(GetFormattedAgeDebugMessage(player, player.Age));
            }
        }

        // called after game is initialized
        public override void OnGameInitializationFinished(Game game)
        {
            // just to make sure facegen is set
            TaleWorlds.Core.FaceGen.ShowDebugValues = true;
            // make sure to call this and other daily tick events on... well, daily tick
            if (game.GameType is Campaign)
                SettingsEffects.Initialize(true);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (!(game.GameType is Campaign) || !(gameStarterObject is CampaignGameStarter gameStarter))
                return;

            // add behaviors
            gameStarter.AddBehavior(new CharacterCreationCampaignBehavior());

            // add game models
            if (DCCSettingsUtil.Instance.CustomAgeModel)
                gameStarter.AddModel(new DCCAgeModel());

            // add event handlers
            game.AddGameHandler<AgingGameHandler>();
            game.EventManager.RegisterEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedAction().OnEncyclopediaPageChanged);
        }

        private bool _isLoaded;

        private class AgingGameHandler : GameHandler
        {
            public override void OnAfterSave()
            {
            }

            public override void OnBeforeSave()
            {
                if (Game.Current == null || !(Game.Current.GameType is Campaign)) return;
                SettingsEffects.Instance.UpdateAllHeroes(Game.Current, true);
            }
        }
    }
}