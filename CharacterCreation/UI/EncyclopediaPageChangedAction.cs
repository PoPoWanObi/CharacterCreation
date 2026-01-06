using CharacterCreation.Util;
using SandBox.GauntletUI.Encyclopedia;
using SandBox.View.Map;
using System;
using CharacterCreation.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.UI
{
    public class EncyclopediaPageChangedAction
    {
        private const string GauntletMovieId = "DCCUnitEditor";
        
        private UnitBuilderVm? _viewModel;
        private EncyclopediaPageVM? _selectedUnitPage;
        private CharacterObject? _selectedUnit;
        private ScreenBase? _gauntletLayerTopScreen;
        private GauntletLayer? _gauntletLayer;

        //public static Type HeroBuilderVMType { get; internal set; } = typeof(HeroBuilderVM);

        public void OnEncyclopediaPageChanged(EncyclopediaPageChangedEvent e)
        {
            if (Mission.Current != null) return; // do not allow edit if in mission as it could screw things up

            // release the movie and layer every time something changes
            _selectedUnit = null;
            _selectedUnitPage = null;
            _viewModel = null;
            if (_gauntletLayerTopScreen != null)
            {
                if (_gauntletLayer != null)
                {
                    _gauntletLayerTopScreen.RemoveLayer(_gauntletLayer);
                    _gauntletLayer = null;
                }
                _gauntletLayerTopScreen = null;
            }

            if (e.NewPage != EncyclopediaPages.Hero && e.NewPage != EncyclopediaPages.Unit) return;
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager)) return;

            var encyclopediaData = QuickReflectionAccess.GauntletMapEncyclopediaViewData.GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            var encyclopediaPageVm = QuickReflectionAccess.EncyclopediaDataDatasource.GetValue(encyclopediaData) as EncyclopediaPageVM;

            switch (encyclopediaPageVm)
            {
                case EncyclopediaUnitPageVM unitPage:
                    _selectedUnit = unitPage.Obj as CharacterObject;
                    break;
                case EncyclopediaHeroPageVM { Obj: Hero hero }:
                    _selectedUnit = hero.CharacterObject;
                    break;
                default:
                    return;
            }
            _selectedUnitPage = encyclopediaPageVm;

            try
            {
                _viewModel = new UnitBuilderVm(_selectedUnit, _selectedUnitPage);
                if (DccSettings.Instance!.DebugMode)
                {
                    var msg = $"[CharacterCreation] viewModel is of type {_viewModel.GetType().FullName}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
                }

                _gauntletLayer = new GauntletLayer(GauntletMovieId, 311);
                _gauntletLayer.InputRestrictions.SetInputRestrictions(mask: InputUsageMask.MouseButtons);
                var gauntletMovie = _gauntletLayer.LoadMovie(_selectedUnit.IsHero ? "DCCHeroEditor" : "DCCTroopEditor", _viewModel);
                if (DccSettings.Instance.DebugMode)
                {
                    var msg = $"[CharacterCreation] Movie loaded: {gauntletMovie.MovieName}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Green));
                }

                _gauntletLayerTopScreen = ScreenManager.TopScreen;
                if (DccSettings.Instance.DebugMode)
                {
                    var msg = $"top layer: {_gauntletLayerTopScreen.GetType().Name}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
                }

                _gauntletLayerTopScreen.AddLayer(_gauntletLayer);
                _gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.MouseButtons);
            }
            catch (Exception ex)
            {
                var msg = $"[CharacterCreation]{ex}";
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                Debug.Print(msg);
            }
        }
    }
}