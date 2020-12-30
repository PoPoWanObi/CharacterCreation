using CharacterCreation.Manager;
using CharacterCreation.Models;
using CharacterCreation.Patches;
using CharacterCreation.Util;
using HarmonyLib;
using Helpers;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

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
            var attributes = new Dictionary<string, TextObject>
            {
                ["HERO_NAME"] = hero.Name,
                ["AGE1"] = new TextObject(expectedAge),
                ["AGE2"] = new TextObject(hero.Age)
            };
            return new TextObject(ExpectedActualAgeMessage, attributes).ToString();
        }

        // Main
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                var harmony = new Harmony("mod.bannerlord.popowanobi.dcc");
                harmony.PatchAll();

                // apply compatibility patches
                CompatibilityPatch.CreateCompatibilityPatches(harmony);

                // properly patch CharacterObject
                var dailyTickMethod = AccessTools.Method(typeof(DynamicBodyCampaignBehavior), "OnDailyTick");
                if (dailyTickMethod == default) dailyTickMethod = AccessTools.Method(typeof(DynamicBodyCampaignBehavior), "DailyTick");
                if (dailyTickMethod != default) harmony.Patch(dailyTickMethod,
                    prefix: new HarmonyMethod(AccessTools.Method(typeof(DynamicBodyPatch), nameof(DynamicBodyPatch.Prefix))));
                Debug.Print("[CharacterCreation] DynamicBodyCampaignBehavior.(On)DailyTick patched");

                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Developer facegen
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ErrorLoadingDccMessage}\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }

        //Registers before the first module appears (main menu)
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            if (_isLoaded) return;

            InformationManager.DisplayMessage(new InformationMessage(LoadedModMessage.ToString(), ColorManager.Orange));
            _isLoaded = true;
        }

        // Load our XML files
        //private static void LoadXMLFiles(CampaignGameStarter gameInitializer)
        //{
        //    // Load our additional strings
        //    gameInitializer.LoadGameTexts(Path.Combine(BasePath.Name, "Modules", "zzCharacterCreation", "ModuleData", "strings.xml"));
        //}

        // Called when loading save game
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            //CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
            //LoadXMLFiles(gameInitializer);

            if (!(game.GameType is Campaign) || !DCCSettingsUtil.Instance.DebugMode) return;

            // print player age if debug is on
            var player = game.ObjectManager.GetObjectTypeList<Hero>().FirstOrDefault(hero => hero.IsHumanPlayerCharacter);
            InformationManager.DisplayMessage(new InformationMessage(GetFormattedAgeDebugMessage(player, player.Age), ColorManager.Red));
            Debug.Print(GetFormattedAgeDebugMessage(player, player.Age));
        }

        // Called when starting new campaign
        //public override void OnNewGameCreated(Game game, object initializerObject)
        //{
        //    CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
        //    LoadXMLFiles(gameInitializer);
        //}

        // called after game is initialized
        public override void OnGameInitializationFinished(Game game)
        {
            InformationManager.DisplayMessage(
                new InformationMessage($"DisableAutoAging: {DCCSettingsUtil.Instance.DisableAutoAging}, IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));

            // just to make sure facegen is set
            TaleWorlds.Core.FaceGen.ShowDebugValues = true;
            // check game options and handle appropriately
            var heroList = game.ObjectManager.GetObjectTypeList<Hero>();
            // pseudocode time
            // if mod setting enables aging (and aging is previously disabled), set everyone's birthday by using default age
            // if mod setting disables aging (and aging is previously enabled), set everyone's birthday to update default age
            if (DCCSettingsUtil.Instance.DisableAutoAging && !CampaignOptions.IsLifeDeathCycleDisabled)
            {
                CampaignOptions.IsLifeDeathCycleDisabled = false;
                foreach (var hero in heroList)
                {
                    var age = hero.Age;
                    CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, age);
                }
                CampaignOptions.IsLifeDeathCycleDisabled = true;
            }
            else if (!DCCSettingsUtil.Instance.DisableAutoAging && CampaignOptions.IsLifeDeathCycleDisabled)
            {
                CampaignOptions.IsLifeDeathCycleDisabled = true;
                foreach (var hero in heroList)
                {
                    var age = hero.Age;
                    CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, age);
                }
                CampaignOptions.IsLifeDeathCycleDisabled = false;
            }

            InformationManager.DisplayMessage(
                new InformationMessage($"DisableAutoAging: {DCCSettingsUtil.Instance.DisableAutoAging}, IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (!(game.GameType is Campaign) || !(gameStarterObject is CampaignGameStarter))
                return;
            CampaignGameStarter gameStarter = (CampaignGameStarter)gameStarterObject;

            // add strings
            gameStarter.LoadGameTexts(Path.Combine(BasePath.Name, "Modules", "zzCharacterCreation", "ModuleData", "strings.xml"));

            // add game models
            gameStarter.AddModel(heroModel = new HeroBuilderModel());
            if (DCCSettingsUtil.Instance.CustomAgeModel)
                gameStarter.AddModel(new Models.AgeModel());

            // add event handlers
            game.AddGameHandler<AgingGameHandler>();
            game.EventManager.RegisterEvent<EncyclopediaPageChangedEvent>(new EncyclopediaPageChangedAction(heroModel).OnEncyclopediaPageChanged);
        }

        private HeroBuilderModel heroModel;

        private bool _isLoaded;

        private class AgingGameHandler : GameHandler
        {
            public override void OnAfterSave()
            {
            }

            public override void OnBeforeSave()
            {
                if (Game.Current == null || !(Game.Current.GameType is Campaign)) return;

                foreach (Hero hero in Game.Current.ObjectManager.GetObjectTypeList<Hero>())
                {
                    hero.FirstName = hero.Name;
                    if (hero.IsPartyLeader)
                        hero.PartyBelongedTo.Name = MobilePartyHelper.GeneratePartyName(hero.CharacterObject);

                    DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                    BodyProperties heroBodyProperties = new BodyProperties(dynamicBodyProperties, hero.BodyProperties.StaticProperties);
                    hero.CharacterObject.UpdatePlayerCharacterBodyProperties(heroBodyProperties, hero.IsFemale);

                    if (hero.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                        InformationManager.DisplayMessage(new InformationMessage(GetFormattedAgeDebugMessage(hero, hero.Age), ColorManager.Red));
                }
            }
        }
    }
}