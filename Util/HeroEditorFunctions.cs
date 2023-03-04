using CharacterCreation.Models;
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
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace CharacterCreation.Util
{
    public static class HeroEditorFunctions
    {
        public static void RenameHero(Hero hero, Action postAction, Action<Hero> nameCallback)
        {
            if (hero?.CharacterObject == null)
                return;

            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.ChangingNameForText.ToString() + hero.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(HeroBuilderVM.CharacterRenamerText.ToString(), HeroBuilderVM.EnterNewNameText.ToString(),
                true, true, HeroBuilderVM.RenameText.ToString(), HeroBuilderVM.CancelText.ToString(), x => RenameHero(x, hero, postAction),
                InformationManager.HideInquiry, false, CampaignUIHelper.IsStringApplicableForHeroName), true);

            nameCallback?.Invoke(hero);
        }

        private static void RenameHero(string heroName, Hero selectedHero, Action action)
        {
            if (selectedHero.CharacterObject == null)
            {
                InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.InvalidCharacterText.ToString(), ColorManager.Red));
                return;
            }

            if (!string.IsNullOrEmpty(heroName)) selectedHero.SetName(new TextObject(heroName), new TextObject(heroName));
            else InformationManager.DisplayMessage(new InformationMessage(HeroBuilderVM.InvalidNameText.ToString(), ColorManager.Red));
        }

        public static void EditHero(Hero hero, Action postAction, Action<Hero> editCallback)
        {
            if (hero?.CharacterObject == null)
                return;

            postAction?.Invoke();
            FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(new GauntletBodyGeneratorScreen(hero.CharacterObject, false, null));

            editCallback?.Invoke(hero);
        }

        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            if (!characterObject.IsHero) return;
            characterObject.HeroObject.SetBirthDay(randomize ? HeroHelper.GetRandomBirthDayForAge(targetAge) : CampaignTime.YearsFromNow(-targetAge));
        }
    }
}
