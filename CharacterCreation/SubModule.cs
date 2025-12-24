using System;
using System.Collections.Generic;
using BUTR.MessageBoxPInvoke.Helpers;
using CharacterCreation.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using static CharacterCreation.Util.DccLocalization;

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

        private readonly CharacterCreationEntryPoint _entryPoint;
        private bool _isLoaded;
        
        public CharacterCreationEntryPoint EntryPoint => _entryPoint;

        public SubModule()
        {
            if (!new CharacterCreationAssemblyLoader().TryLoad(out var result, out var error))
                throw error!;

            _entryPoint = result.Instance;
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
                _entryPoint.OnBeforeInitialModuleScreenSetAsRoot();
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
        public override void OnGameLoaded(Game game, object initializerObject) =>
            _entryPoint.OnGameLoaded(game, initializerObject);

        // called after game is initialized
        public override void OnGameInitializationFinished(Game game) => _entryPoint.OnGameInitializationFinished(game);

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject) =>
            _entryPoint.OnGameStart(game, gameStarterObject);
    }
}