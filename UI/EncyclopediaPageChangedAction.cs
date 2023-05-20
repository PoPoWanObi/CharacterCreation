#nullable enable
using CharacterCreation;
using CharacterCreation.Models;
using CharacterCreation.Util;
using HarmonyLib;
using SandBox.GauntletUI.Encyclopedia;
using SandBox.View.Map;
using System;
using System.Reflection;
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
            if (gauntletEncyclopediaScreenManager == null) return;

            EncyclopediaData? encyclopediaData = QuickReflectionAccess.GauntletMapEncyclopediaViewData.GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = QuickReflectionAccess.EncyclopediaDataDatasource.GetValue(encyclopediaData) as EncyclopediaPageVM;

            if (encyclopediaPageVM is EncyclopediaUnitPageVM unitPage)
                selectedUnit = unitPage.Obj as CharacterObject;
            else
            if (encyclopediaPageVM is EncyclopediaHeroPageVM heroPage && heroPage.Obj is Hero hero)
                selectedUnit = hero.CharacterObject;
            else return;
            selectedUnitPage = encyclopediaPageVM;

            gauntletLayer ??= new GauntletLayer(311);

            try
            {
                if (gauntletMovie != null)
                    gauntletLayer.ReleaseMovie(gauntletMovie);

                viewModel = new UnitBuilderVM(selectedUnit, selectedUnitPage);
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] viewModel is of type {viewModel.GetType().FullName}");

                if (selectedUnit.IsHero) gauntletMovie = gauntletLayer.LoadMovie("DCCHeroEditor", viewModel);
                else gauntletMovie = gauntletLayer.LoadMovie("DCCTroopEditor", viewModel);

                gauntletLayerTopScreen = ScreenManager.TopScreen;
                gauntletLayerTopScreen.AddLayer(gauntletLayer);
                gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.MouseButtons);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }
    }
}