using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.GauntletUI;

namespace CharacterCreation.Models
{
    public static class HeroEditorFunctions
    {
        public static bool RenameHero(Hero hero, Action action)
        {
            if (hero == null || hero.CharacterObject == null)
                return false;

            if (hero.IsHumanPlayerCharacter) // until I find out how player character names are handled, no name change for main hero :(
            {
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.CannotRenamePlayerText.ToString()));
                return false;
            }

            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.ChangingNameForText.ToString() + hero.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(HeroBuilderVM.CharacterRenamerText.ToString(), HeroBuilderVM.EnterNewNameText.ToString(),
                true, true, HeroBuilderVM.RenameText.ToString(), HeroBuilderVM.CancelText.ToString(), x => RenameHero(x, hero, action),
                InformationManager.HideInquiry, false));

            return true;
        }

        private static void RenameHero(string heroName, Hero selectedHero, Action action)
        {
            if (selectedHero.CharacterObject == null)
            {
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.InvalidCharacterText.ToString(), ColorManager.Red));
                return;
            }

            if (!string.IsNullOrEmpty(heroName))
            {
                selectedHero.Name = new TextObject(heroName);
                action?.Invoke();
            }
            else
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.InvalidNameText.ToString(), ColorManager.Red));
        }

        public static bool EditHero(Hero hero, Action action)
        {
            if (hero == null || hero.CharacterObject == null)
                return false;

            action?.Invoke();
            FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(new MBFaceGeneratorGauntletScreen(hero.CharacterObject, false, null));

            return true;
        }
    }
}
