using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using SandBox.GauntletUI;
using SandBox.View.Map;

using HarmonyLib;
using CharacterCreation.Models;
using CharacterCreation.Manager;

namespace CharacterCreation
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "zzCharacterCreation";
        public static readonly string strings = "strings";

        private static readonly TextObject LoadedModMessage = new TextObject("{=CharacterCreation_LoadedModMessage}Loaded Detailed Character Creation."),
            EditAppearanceForHeroMessage = new TextObject("{=CharacterCreation_EditAppearanceForHeroMessage}Entering edit appearance for: "),
            ErrorLoadingDccMessage = new TextObject("{=CharacterCreation_ErrorLoadingDccMessage}Error initializing Detailed Character Creation:");

        private const string ExpectedActualAgeMessage = "{=CharacterCreation_ExpectedActualAgeMessage}[Debug] Hero {HERO_NAME} expected age: {AGE1}, actual age: {AGE2}";

        public static CampaignTime TimeSinceLastSave { get; private set; }

        public static CampaignTime GetDeltaTime(bool update = false)
        {
            CampaignTime deltaTime = CampaignTime.Now - TimeSinceLastSave;
            if (update) TimeSinceLastSave = CampaignTime.Now;
            return deltaTime;
        }

        public static string GetFormattedAgeDebugMessage(Hero hero, float expectedAge)
        {
            var attributes = new Dictionary<string, TextObject>();
            attributes["HERO_NAME"] = hero.Name;
            attributes["AGE1"] = new TextObject(expectedAge);
            attributes["AGE2"] = new TextObject(hero.Age);
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

                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Developer facegen
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ErrorLoadingDccMessage.ToString()}\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }

        //Registers before the first module appears (main menu)
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            if (!this._isLoaded)
            {
                InformationManager.DisplayMessage(new InformationMessage(LoadedModMessage.ToString(), ColorManager.Orange));
                this._isLoaded = true;
            }
        }

        // Load our XML files
        private void LoadXMLFiles(CampaignGameStarter gameInitializer)
        {
            // Load our additional strings
            gameInitializer.LoadGameTexts(BasePath.Name + "Modules/" + ModuleFolderName + "/ModuleData/" + strings + ".xml");
        }

        // Called when loading save game
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
            LoadXMLFiles(gameInitializer);
            TaleWorlds.Core.FaceGen.ShowDebugValues = true;

            if (game.GameType is Campaign && DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
            {
                foreach (Hero hero in game.ObjectManager.GetObjectTypeList<Hero>())
                {

                    if (hero.IsHumanPlayerCharacter)
                    {
                        InformationManager.DisplayMessage(new InformationMessage(GetFormattedAgeDebugMessage(hero, hero.Age), ColorManager.Red));
                        Debug.Print(GetFormattedAgeDebugMessage(hero, hero.Age));
                        break;
                    }
                }
            }
        }

        // Called when starting new campaign
        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
            LoadXMLFiles(gameInitializer);
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            if (!(game.GameType is Campaign))
                return;
            if (!(gameStarterObject is CampaignGameStarter))
                return;

            AddModels(gameStarterObject as CampaignGameStarter);

            TimeSinceLastSave = CampaignTime.Now;
            game.AddGameHandler<AgingGameHandler>();

            game.EventManager.RegisterEvent(delegate (EncyclopediaPageChangedEvent e)
            {
                EncyclopediaData.EncyclopediaPages newPage = e.NewPage;
                if ((int)newPage != 12)
                {
                    selectedHeroPage = null;
                    selectedHero = null;
                    if (gauntletLayerTopScreen != null && gauntletLayer != null)
                    {
                        gauntletLayerTopScreen.RemoveLayer(gauntletLayer);
                        if (gauntletMovie != null)
                        {
                            gauntletLayer.ReleaseMovie(gauntletMovie);
                        }
                        gauntletLayerTopScreen = null;
                        gauntletMovie = null;
                    }
                    return;
                }
                GauntletEncyclopediaScreenManager? gauntletEncyclopediaScreenManager = MapScreen.Instance.EncyclopediaScreenManager as GauntletEncyclopediaScreenManager;
                if (gauntletEncyclopediaScreenManager == null)
                {
                    return;
                }

                EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletEncyclopediaScreenManager), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
                EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;
                selectedHeroPage = (encyclopediaPageVM as EncyclopediaHeroPageVM);

                if (selectedHeroPage == null)
                {
                    return;
                }
                selectedHero = (selectedHeroPage.Obj as Hero);
                if (selectedHero == null)
                {
                    return;
                }
                if (gauntletLayer == null)
                {
                    gauntletLayer = new GauntletLayer(211, "GauntletLayer");
                }

                try
                {
                    if (viewModel == null)
                    {
                        viewModel = new HeroBuilderVM(heroModel, delegate (Hero editHero)
                        {
                            InformationManager.DisplayMessage(new InformationMessage(EditAppearanceForHeroMessage.ToString() + editHero));
                        });
                    }
                    viewModel.SetHero(selectedHero);
                    gauntletMovie = gauntletLayer.LoadMovie("HeroEditor", viewModel);
                    gauntletLayerTopScreen = ScreenManager.TopScreen;
                    gauntletLayerTopScreen.AddLayer(gauntletLayer);
                    gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.MouseButtons);

                    // Refresh
                    selectedHeroPage.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
                }
            });
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
            if (gameStarter != null)
            {
                gameStarter.AddModel(heroModel = new HeroBuilderModel());

                if (DCCSettings.Instance != null && DCCSettings.Instance.CustomAgeModel)
                    gameStarter.AddModel(new Models.AgeModel());
            }
        }

        private HeroBuilderVM? viewModel;
        private EncyclopediaHeroPageVM? selectedHeroPage;
        private HeroBuilderModel? heroModel;
        private Hero? selectedHero;
        private ScreenBase? gauntletLayerTopScreen;
        private GauntletLayer? gauntletLayer;
        private GauntletMovie? gauntletMovie;

        private bool _isLoaded;

        private class AgingGameHandler : GameHandler
        {
            public override void OnAfterSave()
            {
            }

            public override void OnBeforeSave()
            {
                if (Game.Current != null && Game.Current.GameType is Campaign)
                {
                    //CampaignTime deltaTime = CampaignTime.Now - TimeSinceLastSave;
                    CampaignTime deltaTime = SubModule.GetDeltaTime(true);
                    //double yearsElapsed = deltaTime.ToYears;
                    //TimeSinceLastSave = CampaignTime.Now;

                    foreach (Hero hero in Game.Current.ObjectManager.GetObjectTypeList<Hero>())
                    {
                        //TODO:: Why is this conflicting now???
                        /*ddouble newAge = hero.Age + yearsElapsed;
                        DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties((float)newAge, hero.Weight, hero.Build);*/

                        DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                        BodyProperties heroBodyProperties = new BodyProperties(dynamicBodyProperties, hero.BodyProperties.StaticProperties);
                        //BodyProperties heroBodyProperties = hero.BodyProperties;
                        //CharacterBodyManager.CopyDynamicBodyProperties(dynamicBodyProperties, heroBodyProperties.DynamicProperties);
                        hero.CharacterObject.UpdatePlayerCharacterBodyProperties(heroBodyProperties, hero.IsFemale);

                        if (hero.IsHumanPlayerCharacter && DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                            InformationManager.DisplayMessage(new InformationMessage(GetFormattedAgeDebugMessage(hero, hero.Age), ColorManager.Red));
                    }
                }
            }
        }
    }
}