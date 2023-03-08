#nullable enable
using CharacterCreation;
using CharacterCreation.Models;
using HarmonyLib;
using SandBox.GauntletUI.Encyclopedia;
using SandBox.View.Map;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.UI
{
    public class EncyclopediaPageChangedAction
    {
        private UnitBuilderVM? viewModel;
        private EncyclopediaPageVM? selectedUnitPage;
        private CharacterObject? selectedUnit;
        private ScreenBase? gauntletLayerTopScreen;
        private GauntletLayer? gauntletLayer;
        private IGauntletMovie? gauntletMovie;

        //public static Type HeroBuilderVMType { get; internal set; } = typeof(HeroBuilderVM);

        public void OnEncyclopediaPageChanged(EncyclopediaPageChangedEvent e)
        {
            if (Mission.Current != null) return; // do not allow edit if in mission as it could screw things up

            EncyclopediaPages newPage = e.NewPage;
            if (newPage != EncyclopediaPages.Hero && newPage != EncyclopediaPages.Unit)
            {
                viewModel = null;
                selectedUnitPage = null;
                selectedUnit = null;
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

            GauntletMapEncyclopediaView? gauntletEncyclopediaScreenManager = MapScreen.Instance.EncyclopediaScreenManager as GauntletMapEncyclopediaView;
            if (gauntletEncyclopediaScreenManager == null)
            {
                return;
            }

            EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletMapEncyclopediaView), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;

            if (encyclopediaPageVM is EncyclopediaUnitPageVM unitPage)
                selectedUnit = unitPage.Obj as CharacterObject;
            else if (encyclopediaPageVM is EncyclopediaHeroPageVM heroPage && heroPage.Obj is Hero hero)
                selectedUnit = hero.CharacterObject;
            else return;
            selectedUnitPage = encyclopediaPageVM;

            gauntletLayer ??= new GauntletLayer(311);

            try
            {
                if (gauntletMovie != null)
                    gauntletLayer.ReleaseMovie(gauntletMovie);

                //static void Callback(Hero editHero) => InformationManager.DisplayMessage(new InformationMessage(SubModule.EditAppearanceForHeroMessage.ToString() + editHero));
                //ConstructorInfo constructor = HeroBuilderVMType.GetConstructor(new[] { typeof(Action<Hero>) });
                //viewModel = constructor?.Invoke(new[] {(Action<Hero>) Callback}) as ViewModel;
                //if (viewModel == default) return;
                //if (viewModel == null)
                //{
                //    viewModel = new HeroBuilderVM();
                //}
                viewModel = new UnitBuilderVM(selectedUnit, selectedUnitPage);
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] viewModel is of type {viewModel.GetType().FullName}");

                //AccessTools.Method(HeroBuilderVMType, "SetHero").Invoke(viewModel, new[] { selectedHero });
                //viewModel.SetHero(selectedHero);

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
                //selectedHeroPage.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }
    }
}