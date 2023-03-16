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

        public static void RenameUnit(CharacterObject unit, Action postAction)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(UnitBuilderVM.ChangingNameForText.ToString() + unit.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(UnitBuilderVM.CharacterRenamerText.ToString(), UnitBuilderVM.EnterNewNameText.ToString(),
                true, true, UnitBuilderVM.RenameText.ToString(), NativeCancel.ToString(), x => RenameUnit(x, unit, postAction),
                InformationManager.HideInquiry, false, CampaignUIHelper.IsStringApplicableForHeroName), true);
        }

        private static void RenameUnit(string unitName, CharacterObject selectedUnit, Action action)
        {
            if (!string.IsNullOrEmpty(unitName))
            {
                if (selectedUnit.IsHero) selectedUnit.HeroObject.SetName(new TextObject(unitName), new TextObject(unitName));
                else
                {
                    selectedUnit.UpdateName(new TextObject(unitName));
                    CharacterCreationCampaignBehavior.Instance?.SetUnitNameOverride(selectedUnit, unitName);
                }
            }
            else InformationManager.DisplayMessage(new InformationMessage(UnitBuilderVM.InvalidNameText.ToString(), ColorManager.Red));

            action();
        }

        public static void EditUnit(CharacterObject unit, Action postAction)
        {
            postAction?.Invoke();
            FaceGen.ShowDebugValues = true;
            ScreenManager.PushScreen(new GauntletBodyGeneratorScreen(unit, false, null));
        }

        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            if (!characterObject.IsHero) return;
            characterObject.HeroObject.SetBirthDay(randomize ? HeroHelper.GetRandomBirthDayForAge(targetAge) : CampaignTime.YearsFromNow(-targetAge));
        }
    }
}
