using CharacterCreation.UI;
using CharacterCreation.Patches;
using HarmonyLib;
using Helpers;
using System;
using System.Reflection;
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
using CharacterCreation.CampaignSystem;

namespace CharacterCreation.Util
{
    public static class UnitEditorFunctions
    {
        private static readonly TextObject
            NativeYes = new TextObject("{=aeouhelq}Yes"),
            NativeNo = new TextObject("{=8OkPHu4f}No"),
            NativeCancel = new TextObject("{=3CpNUnVl}Cancel");

        public static void RenameUnit(CharacterObject unit, Action? postAction = default)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(UnitBuilderVM.ChangingNameForText.ToString() + unit.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(UnitBuilderVM.CharacterRenamerText.ToString(), UnitBuilderVM.EnterNewNameText.ToString(),
                true, true, UnitBuilderVM.RenameText.ToString(), NativeCancel.ToString(), x => RenameUnit(x, unit, postAction),
                InformationManager.HideInquiry, false, CampaignUIHelper.IsStringApplicableForHeroName), true);
        }

        private static void RenameUnit(string unitName, CharacterObject selectedUnit, Action? action = default)
        {
            if (!string.IsNullOrEmpty(unitName))
            {
                if (selectedUnit.IsHero) selectedUnit.HeroObject.SetName(new TextObject(unitName), new TextObject(unitName));
                else
                    CharacterCreationCampaignBehavior.Instance?.SetUnitNameOverride(selectedUnit, unitName);
            }
            else InformationManager.DisplayMessage(new InformationMessage(UnitBuilderVM.InvalidNameText.ToString(), ColorManager.Red));

            action?.Invoke();
        }

        public static void UndoRename(CharacterObject selectedUnit, Action? postAction = default)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(UnitBuilderVM.CharacterUnrenamerText.ToString(), UnitBuilderVM.UnrenameWarningText.ToString(),
                true, true, NativeYes.ToString(), NativeNo.ToString(), () =>
                {
                    CharacterCreationCampaignBehavior.Instance?.UndoUnitNameOverride(selectedUnit);
                    postAction?.Invoke();
                }, InformationManager.HideInquiry));
        }

        public static void EditUnit(CharacterObject unit, Action? postAction = default)
        {
            postAction?.Invoke();
            FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(new GauntletBodyGeneratorScreen(unit, false, null));
        }

        public static void UndoEdit(CharacterObject selectedUnit, Action? postAction = default)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(UnitBuilderVM.CharacterUneditText.ToString(), UnitBuilderVM.UneditWarningText.ToString(),
                true, true, NativeYes.ToString(), NativeNo.ToString(), () =>
                {
                    CharacterCreationCampaignBehavior.Instance?.UndoBodyPropertiesOverride(selectedUnit);
                    postAction?.Invoke();
                }, InformationManager.HideInquiry));
        }

        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            if (!characterObject.IsHero) return;
            characterObject.HeroObject.SetBirthDay(randomize ? HeroHelper.GetRandomBirthDayForAge(targetAge) : CampaignTime.YearsFromNow(-targetAge));
        }
    }
}
