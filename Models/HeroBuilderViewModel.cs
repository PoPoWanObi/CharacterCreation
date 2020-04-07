using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.MountAndBlade.LegacyGUI.Missions;

namespace CharacterCreation.Models
{
    public class HeroBuilderViewModel : ViewModel
    {
        public void SetHero(Hero hero)
        {
            this.selectedHero = hero;
        }

        public HeroBuilderViewModel(HeroBuilderModel heroModel, Action<Hero> editCallback)
        {
            this.heroModel = heroModel;
            this.editCallback = editCallback;
        }

        public void ExecuteEdit()
        {
            if (this.selectedHero == null)
            {
                return;
            }
            this.Edit(this.selectedHero);
            Action<Hero> action = this.editCallback;
            if (action == null)
            {
                return;
            }
            action(this.selectedHero);
        }

        public void Edit(Hero hero)
        {
            if (hero.CharacterObject == null)
                return;

            ScreenManager.PushScreen(ViewCreator.CreateMBFaceGeneratorScreen(hero.CharacterObject, false));
        }

        //Game.Current.PlayerTroop -- ingore me
        private HeroBuilderModel heroModel;
        private Hero selectedHero;
        private Action<Hero> editCallback;
    }
}
