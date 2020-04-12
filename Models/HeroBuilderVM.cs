using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions;
using TaleWorlds.Localization;
using System.Collections.Generic;
using SandBox.GauntletUI;
using SandBox.View.Map;
using System.Reflection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;

namespace CharacterCreation.Models
{
    public class HeroBuilderVM : ViewModel
    {
        public void SetHero(Hero hero)
        {
            this.selectedHero = hero;
        }

        public HeroBuilderVM(HeroBuilderModel heroModel, Action<Hero> editCallback)
        {
            this.heroModel = heroModel;
            this.editCallback = editCallback;
        }

        public HeroBuilderVM(Action<Hero> nameCallback)
        {
            this.nameCallback = nameCallback;
        }

        public void ExecuteEdit()
        {
            if (this.selectedHero == null)
                return;

            this.Edit(this.selectedHero);
            Action<Hero> action = this.editCallback;
            if (action == null)
                return;

            action(this.selectedHero);
        }

        public void ExecuteName()
        {
            if (this.selectedHero == null)
                return;

            this.Name(this.selectedHero);
            Action<Hero> action = this.nameCallback;

            if (action == null)
                return;

            action(this.selectedHero);
        }

        public void Name(Hero hero)
        {
            if (hero.CharacterObject == null)
                return;

            if (Settings.Instance.DebugMode == true)
                InformationManager.DisplayMessage(new InformationMessage("Changing name for: " + hero.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData("Character Renamer", "Enter a new name", true, true, "Rename", "Cancel", new Action<string>(this.renameHero), InformationManager.HideInquiry, false));
        }

        private void renameHero(string heroName)
        {
            if (selectedHero.CharacterObject == null)
            {
                InformationManager.DisplayMessage(new InformationMessage("Character is not valid."));
                return;
            }
            
            if (!String.IsNullOrEmpty(heroName))
            {
                selectedHero.Name = new TextObject(heroName);
                ClosePage();
            }
            else
            {
                InformationManager.DisplayMessage(new InformationMessage("Name is not valid"));
                return;
            }
        }

        public void RefreshPage()
        {
            GauntletEncyclopediaScreenManager gauntletEncyclopediaScreenManager = MapScreen.Instance.EncyclopediaScreenManager as GauntletEncyclopediaScreenManager;
            if (gauntletEncyclopediaScreenManager == null)
                return;

            FieldInfo field = typeof(GauntletEncyclopediaScreenManager).GetField("_encyclopediaData", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo field2 = typeof(EncyclopediaData).GetField("_activeDatasource", BindingFlags.Instance | BindingFlags.NonPublic);
            EncyclopediaData encyclopediaData = (EncyclopediaData)field.GetValue(gauntletEncyclopediaScreenManager);
            EncyclopediaPageVM encyclopediaPageVM = (EncyclopediaPageVM)field2.GetValue(encyclopediaData);

            this.selectedHeroPage = (encyclopediaPageVM as EncyclopediaHeroPageVM);

            if (this.selectedHeroPage == null)
                return;

            this.selectedHeroPage.Refresh();
        }

        public void ClosePage()
        {
            GauntletEncyclopediaScreenManager gauntletEncyclopediaScreenManager = MapScreen.Instance.EncyclopediaScreenManager as GauntletEncyclopediaScreenManager;
            if (gauntletEncyclopediaScreenManager == null)
                return;

            FieldInfo field = typeof(GauntletEncyclopediaScreenManager).GetField("_encyclopediaData", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo field2 = typeof(EncyclopediaData).GetField("_activeDatasource", BindingFlags.Instance | BindingFlags.NonPublic);
            EncyclopediaData encyclopediaData = (EncyclopediaData)field.GetValue(gauntletEncyclopediaScreenManager);
            EncyclopediaPageVM encyclopediaPageVM = (EncyclopediaPageVM)field2.GetValue(encyclopediaData);

            this.selectedHeroPage = (encyclopediaPageVM as EncyclopediaHeroPageVM);

            if (this.selectedHeroPage == null)
                return;

            gauntletEncyclopediaScreenManager.CloseEncyclopedia();
        }

        public void Edit(Hero hero)
        {
            if (hero.CharacterObject == null)
                return;

            ClosePage();
            TaleWorlds.Core.FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(ViewCreator.CreateMBFaceGeneratorScreen(hero.CharacterObject, false));
        }
        
        //Game.Current.PlayerTroop -- ingore me
        private HeroBuilderModel heroModel;
        private Hero selectedHero;
        private Action<Hero> editCallback;
        private Action<Hero> nameCallback;
        private EncyclopediaHeroPageVM selectedHeroPage;
    }
}
