using CharacterCreation.CampaignSystem;
using CharacterCreation.Settings;
using CharacterCreation.Util;
using SandBox.GauntletUI.Encyclopedia;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Library;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.UI
{
    public class UnitBuilderVm : ViewModel
    {
        public UnitBuilderVm(CharacterObject unit, EncyclopediaPageVM page)
        {
            _selectedUnit = unit;
            _selectedUnitPage = page;
            if (DccSettings.Instance!.DebugMode)
            {
                var msg = $"[CharacterCreation] {_selectedUnit.Name} loaded to unit builder";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Green));
            }
        }

        [DataSourceProperty]
        public string DccOptionsString => DccSettings.Instance!.ShowOptionsLabel ? DccOptionsText : "";

        [DataSourceProperty]
        public string EditAppearanceString => EditAppearanceText;

        [DataSourceProperty]
        public string ChangeNameString => ChangeNameText;

        [DataSourceProperty]
        public int AspectMarginTop => MarginUtil.GetTopMarginForAspectRatio();

        [DataSourceProperty]
        public int AspectMarginRight => MarginUtil.GetRightMarginForAspectRatio();

        [DataSourceProperty]
        public string UndoAppearanceString => UndoAppearanceText;

        [DataSourceProperty]
        public string UndoRenameString => UndoRenameText;

        [DataSourceProperty]
        public bool EnableRevertAppearance
        {
            get
            {
                return !_selectedUnit.IsHero && CharacterCreationCampaignBehavior.Instance != default
                    && CharacterCreationCampaignBehavior.Instance.HasBodyPropertiesOverride(_selectedUnit);
            }
        }

        [DataSourceProperty]
        public bool EnableRevertName
        {
            get
            {
                return !_selectedUnit.IsHero && CharacterCreationCampaignBehavior.Instance != default
                    && CharacterCreationCampaignBehavior.Instance.HasUnitNameOverride(_selectedUnit);
            }
        }

        public void ExecuteEdit() => UnitEditorFunctions.EditUnit(_selectedUnit);

        public void ExecuteName() => UnitEditorFunctions.RenameUnit(_selectedUnit);

        public void UndoEdit() => UnitEditorFunctions.UndoEdit(_selectedUnit);

        public void UndoRename() => UnitEditorFunctions.UndoRename(_selectedUnit);

        public void RefreshPage()
        {
            //if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager))
            //    return;

            //EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletMapEncyclopediaView), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            //EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;
            //encyclopediaPageVM?.Refresh();
            _selectedUnitPage.Refresh();
        }

        public void ClosePage()
        {
            if (MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager)
                gauntletEncyclopediaScreenManager.CloseEncyclopedia();
        }

        private CharacterObject _selectedUnit;
        private EncyclopediaPageVM _selectedUnitPage;
    }
}
