using HarmonyLib;
using Helpers;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.GauntletUI;

namespace CharacterCreation.Models
{
    public static class HeroEditorFunctions
    {
        public static void RenameHero(Hero hero, Action postAction, Action<Hero> nameCallback)
        {
            if (hero?.CharacterObject == null)
                return;

            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.ChangingNameForText.ToString() + hero.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(HeroBuilderVM.CharacterRenamerText.ToString(), HeroBuilderVM.EnterNewNameText.ToString(),
                true, true, HeroBuilderVM.RenameText.ToString(), HeroBuilderVM.CancelText.ToString(), x => RenameHero(x, hero, postAction),
                InformationManager.HideInquiry, false, CampaignUIHelper.IsStringApplicableForHeroName));

            nameCallback?.Invoke(hero);
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
                var newName = new TextObject(heroName);
                selectedHero.Name = newName;
                selectedHero.FirstName = newName;
                if (selectedHero.IsPartyLeader)
                    selectedHero.PartyBelongedTo.Name = MobilePartyHelper.GeneratePartyName(selectedHero.CharacterObject);
                action?.Invoke();
            }
            else
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.InvalidNameText.ToString(), ColorManager.Red));
        }

        public static void EditHero(Hero hero, Action postAction, Action<Hero> editCallback)
        {
            if (hero?.CharacterObject == null)
                return;

            postAction?.Invoke();
            FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(new MBFaceGeneratorGauntletScreen(hero.CharacterObject, false, null));

            editCallback?.Invoke(hero);
        }
    }
}
