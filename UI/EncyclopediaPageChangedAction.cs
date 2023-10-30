#nullable enable
using CharacterCreation;
using CharacterCreation.Models;
using CharacterCreation.Util;
using HarmonyLib;
using SandBox.GauntletUI.Encyclopedia;
using SandBox.View.Map;
using System;
using System.Reflection;
using Bannerlord.BUTR.Shared.Helpers;
using BUTR.MessageBoxPInvoke.Helpers;
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

            // release the movie and layer every time something changes
            selectedUnit = default;
            selectedUnitPage = default;
            viewModel = default;
            if (gauntletLayerTopScreen != default)
            {
                if (gauntletLayer != default)
                {
                    if (gauntletMovie != default)
                    {
                        gauntletLayer.ReleaseMovie(gauntletMovie);
                        gauntletMovie = default;
                    }
                    gauntletLayerTopScreen.RemoveLayer(gauntletLayer);
                    gauntletLayer = default;
                }
                gauntletLayerTopScreen = default;
            }

            if (e.NewPage != EncyclopediaPages.Hero && e.NewPage != EncyclopediaPages.Unit) return;
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager)) return;

            EncyclopediaData? encyclopediaData = QuickReflectionAccess.GauntletMapEncyclopediaViewData.GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = QuickReflectionAccess.EncyclopediaDataDatasource.GetValue(encyclopediaData) as EncyclopediaPageVM;

            if (encyclopediaPageVM is EncyclopediaUnitPageVM unitPage)
                selectedUnit = unitPage.Obj as CharacterObject;
            else if (encyclopediaPageVM is EncyclopediaHeroPageVM heroPage && heroPage.Obj is Hero hero)
                selectedUnit = hero.CharacterObject;
            else return;
            selectedUnitPage = encyclopediaPageVM;

            try
            {
                viewModel = new UnitBuilderVM(selectedUnit, selectedUnitPage);
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] viewModel is of type {viewModel.GetType().FullName}");

                gauntletLayer = new GauntletLayer(311);
                if (selectedUnit.IsHero) gauntletMovie = gauntletLayer.LoadMovie("DCCHeroEditor", viewModel);
                else gauntletMovie = gauntletLayer.LoadMovie("DCCTroopEditor", viewModel);
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] Movie loaded: {gauntletMovie.MovieName}");

                gauntletLayerTopScreen = ScreenManager.TopScreen;
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"top layer: {gauntletLayerTopScreen.GetType().Name}");
                gauntletLayerTopScreen.AddLayer(gauntletLayer);
                gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.MouseButtons);
            }
            catch (Exception ex)
            {
                InformationManager.DisplayMessage(new InformationMessage($"Error :\n{ex}"));
                Debug.Print($"[CharacterCreation]{ex}");
            }
        }
    }
}