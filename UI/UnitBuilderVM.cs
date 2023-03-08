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

        //public HeroBuilderVM(HeroBuilderModel heroModel, Action<Hero> editCallback)
        //{
        //    this.heroModel = heroModel;
        //    this.editCallback = editCallback;
        //}

        //public HeroBuilderVM(Action<Hero> nameCallback)
        //{
        //    this.nameCallback = nameCallback;
        //}

        public void ExecuteEdit()
        {
            UnitEditorFunctions.EditUnit(selectedUnit, RefreshPage);

            //if (selectedHero == null)
            //    return;

            //Edit(selectedHero);
            //Action<Hero> action = this.editCallback;
            //if (action == null)
            //    return;

            //action(selectedHero);
        }

        public void ExecuteName()
        {
            UnitEditorFunctions.RenameUnit(selectedUnit, ClosePage);

            //if (selectedHero == null)
            //    return;

            //if (selectedHero.IsHumanPlayerCharacter) // until I find out how player character names are handled, no name change for main hero :(
            //{
            //    InformationManager.DisplayMessage(new InformationMessage(CannotRenamePlayerText.ToString()));
            //    return;
            //}

            //Name(selectedHero);
            //Action<Hero> action = nameCallback;

            //if (action == null)
            //    return;

            //action(selectedHero);
        }

        //public void Name(Hero hero)
        //{
        //    if (hero.CharacterObject == null)
        //        return;

        //    if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
        //        InformationManager.DisplayMessage(new InformationMessage(ChangingNameForText.ToString() + hero.Name));

        //    InformationManager.ShowTextInquiry(new TextInquiryData(CharacterRenamerText.ToString(), EnterNewNameText.ToString(),
        //        true, true, RenameText.ToString(), CancelText.ToString(), new Action<string>(RenameHero), InformationManager.HideInquiry, false));
        //}

        //private void RenameHero(string heroName)
        //{
        //    if (selectedHero.CharacterObject == null)
        //    {
        //        InformationManager.DisplayMessage(new InformationMessage(InvalidCharacterText.ToString(), ColorManager.Red));
        //        return;
        //    }
            
        //    if (!string.IsNullOrEmpty(heroName))
        //    {
        //        selectedHero.Name = new TextObject(heroName);
        //        ClosePage();
        //    }
        //    else
        //    {
        //        InformationManager.DisplayMessage(new InformationMessage(InvalidNameText.ToString(), ColorManager.Red));
        //        return;
        //    }
        //}

        public void RefreshPage()
        {
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager))
                return;

            EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletMapEncyclopediaView), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;
            encyclopediaPageVM?.Refresh();
        }

        public void ClosePage()
        {
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletMapEncyclopediaView gauntletEncyclopediaScreenManager))
                return;
            gauntletEncyclopediaScreenManager.CloseEncyclopedia();
        }

        //public void Edit(Hero hero)
        //{
        //    if (hero.CharacterObject == null)
        //        return;

        //    ClosePage();
        //    TaleWorlds.Core.FaceGen.ShowDebugValues = true;
        //    //ScreenManager.PushScreen(ViewCreator.CreateMBFaceGeneratorScreen(hero.CharacterObject, false));
        //    ScreenManager.PushScreen(new MBFaceGeneratorGauntletScreen(hero.CharacterObject, false, null));
        //}

        //Game.Current.PlayerTroop -- ingore me
        //private HeroBuilderModel? heroModel;
        private CharacterObject selectedUnit;
        private EncyclopediaPageVM selectedUnitPage;
    }
}
