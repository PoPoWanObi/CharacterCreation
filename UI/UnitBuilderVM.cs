#nullable enable
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using SandBox.GauntletUI;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using SandBox.GauntletUI.Encyclopedia;
using CharacterCreation.Util;
using CharacterCreation.CampaignSystem;

namespace CharacterCreation.UI
{
    public partial class UnitBuilderVM : ViewModel
    {
        public UnitBuilderVM(CharacterObject unit, EncyclopediaPageVM page)
        {
            selectedUnit = unit;
            selectedUnitPage = page;
            if (DCCSettingsUtil.Instance.DebugMode) Debug.Print($"[CharacterCreation] {selectedUnit.Name} loaded to unit builder");
        }

        [DataSourceProperty]
        public string DCCOptionsString
        { get
            {
                if (DCCSettingsUtil.Instance.ShowOptionsLabel)
                    return DCCOptionsText.ToString();
                else
                    return "";
            } 
        }

        [DataSourceProperty]
        public string EditAppearanceString => EditAppearanceText.ToString();

        [DataSourceProperty]
        public string ChangeNameString => ChangeNameText.ToString();

        [DataSourceProperty]
        public string UndoAppearanceString => UndoAppearanceText.ToString();

        [DataSourceProperty]
        public string UndoRenameString => UndoRenameText.ToString();

        [DataSourceProperty]
        public bool EnableRevertAppearance
        {
            get
            {
                return !selectedUnit.IsHero && CharacterCreationCampaignBehavior.Instance != default
                    && CharacterCreationCampaignBehavior.Instance.HasBodyPropertiesOverride(selectedUnit);
            }
        }

        [DataSourceProperty]
        public bool EnableRevertName
        {
            get
            {
                return !selectedUnit.IsHero && CharacterCreationCampaignBehavior.Instance != default
                    && CharacterCreationCampaignBehavior.Instance.HasUnitNameOverride(selectedUnit);
            }
        }

        public void ExecuteEdit() => UnitEditorFunctions.EditUnit(selectedUnit);

        public void ExecuteName() => UnitEditorFunctions.RenameUnit(selectedUnit);

        public void UndoEdit() => UnitEditorFunctions.UndoEdit(selectedUnit);

        public void UndoRename() => UnitEditorFunctions.UndoRename(selectedUnit);

        public void RefreshPage()
        {
            //if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager))
            //    return;

            //EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletMapEncyclopediaView), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            //EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;
            //encyclopediaPageVM?.Refresh();
            selectedUnitPage.Refresh();
        }

        public void ClosePage()
        {
            if (MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager)
                gauntletEncyclopediaScreenManager.CloseEncyclopedia();
        }

        private CharacterObject selectedUnit;
        private EncyclopediaPageVM selectedUnitPage;
    }
}
