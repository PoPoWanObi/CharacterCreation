#nullable enable
using HarmonyLib;
using SandBox.GauntletUI;
using SandBox.View.Map;
using System;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;

namespace CharacterCreation.Models
{
    public class EncyclopediaPageChangedAction
    {
        private HeroBuilderVM? viewModel;
        private EncyclopediaHeroPageVM? selectedHeroPage;
        private HeroBuilderModel? heroModel;
        private Hero? selectedHero;
        private ScreenBase? gauntletLayerTopScreen;
        private GauntletLayer? gauntletLayer;
        private IGauntletMovie? gauntletMovie;

        //public static Type HeroBuilderVMType { get; internal set; } = typeof(HeroBuilderVM);

        public EncyclopediaPageChangedAction(HeroBuilderModel model)
        {
            heroModel = model;
        }

        public void OnEncyclopediaPageChanged(EncyclopediaPageChangedEvent e)
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
            selectedHero = selectedHeroPage.Obj as Hero;
            if (selectedHero == null)
            {
                return;
            }
            gauntletLayer ??= new GauntletLayer(211);

            try
            {
                if (gauntletMovie != null)
                    gauntletLayer.ReleaseMovie(gauntletMovie);

                //static void Callback(Hero editHero) => InformationManager.DisplayMessage(new InformationMessage(SubModule.EditAppearanceForHeroMessage.ToString() + editHero));
                //ConstructorInfo constructor = HeroBuilderVMType.GetConstructor(new[] { typeof(Action<Hero>) });
                //viewModel = constructor?.Invoke(new[] {(Action<Hero>) Callback}) as ViewModel;
                //if (viewModel == default) return;
                if (viewModel == null)
                {
                    viewModel = new HeroBuilderVM(hero => InformationManager.DisplayMessage(new InformationMessage(SubModule.EditAppearanceForHeroMessage.ToString() + hero)));
                }
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] viewModel is of type {viewModel.GetType().FullName}");

                //AccessTools.Method(HeroBuilderVMType, "SetHero").Invoke(viewModel, new[] { selectedHero });
                viewModel.SetHero(selectedHero);

                //// BEGIN compatibility code block
                //if (viewModel.GetType() == typeof(HeroBuilderVM))
                //    gauntletMovie = gauntletLayer.LoadMovie("DCCHeroEditor", viewModel);
                //else if (viewModel.GetType().FullName == "CharacterReload.VM.HeroBuilderVM")
                //    gauntletMovie = gauntletLayer.LoadMovie("HeroEditor", viewModel);
                //else return;
                //// END
                gauntletMovie = gauntletLayer.LoadMovie("DCCHeroEditor", viewModel);

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
        }
    }
}