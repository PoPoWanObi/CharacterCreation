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

namespace CharacterCreation.Models
{
    public partial class UnitBuilderVM : ViewModel
    {
        public UnitBuilderVM(CharacterObject unit, EncyclopediaPageVM page)
        {
            selectedUnit = unit;
            selectedUnitPage = page;
        }

        public void ExecuteEdit() => UnitEditorFunctions.EditUnit(selectedUnit, ClosePage);

        public void ExecuteName() => UnitEditorFunctions.RenameUnit(selectedUnit, RefreshPage);

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
