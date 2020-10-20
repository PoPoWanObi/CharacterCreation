#nullable enable
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using SandBox.GauntletUI;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using HarmonyLib;

namespace CharacterCreation.Models
{
    public partial class HeroBuilderVM : ViewModel
    {

        public void SetHero(Hero hero)
        {
            selectedHero = hero;
        }

        public HeroBuilderVM()
        {

        }

        public HeroBuilderVM(Action<Hero> editCallback)
        {
            this.editCallback = editCallback;
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
            HeroEditorFunctions.EditHero(selectedHero, ClosePage, nameCallback);

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
            HeroEditorFunctions.RenameHero(selectedHero, ClosePage, nameCallback);

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
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletEncyclopediaScreenManager gauntletEncyclopediaScreenManager))
                return;

            EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletEncyclopediaScreenManager), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;

            selectedHeroPage = encyclopediaPageVM as EncyclopediaHeroPageVM;

            selectedHeroPage?.Refresh();
        }

        public void ClosePage()
        {
            if (!(MapScreen.Instance.EncyclopediaScreenManager is GauntletEncyclopediaScreenManager gauntletEncyclopediaScreenManager))
                return;

            EncyclopediaData? encyclopediaData = AccessTools.Field(typeof(GauntletEncyclopediaScreenManager), "_encyclopediaData").GetValue(gauntletEncyclopediaScreenManager) as EncyclopediaData;
            EncyclopediaPageVM? encyclopediaPageVM = AccessTools.Field(typeof(EncyclopediaData), "_activeDatasource").GetValue(encyclopediaData) as EncyclopediaPageVM;

            this.selectedHeroPage = (encyclopediaPageVM as EncyclopediaHeroPageVM);

            if (this.selectedHeroPage == null)
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
        private HeroBuilderModel? heroModel;
        private Hero? selectedHero;
        private EncyclopediaHeroPageVM? selectedHeroPage;

        public Action<Hero>? editCallback;

        public Action<Hero>? nameCallback;
    }
}
