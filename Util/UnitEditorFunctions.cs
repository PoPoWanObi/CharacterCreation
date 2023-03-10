using CharacterCreation.Models;
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

namespace CharacterCreation.Util
{
    public static class UnitEditorFunctions
    {
        private static readonly MethodInfo BasicCharacterObjectSetNameMethod
            = AccessTools.Method(typeof(BasicCharacterObject), "SetName");

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
                    var func = AccessTools.MethodDelegate<Action<TextObject>>(BasicCharacterObjectSetNameMethod, selectedUnit);
                    func(new TextObject(unitName));
                }
            }
            else InformationManager.DisplayMessage(new InformationMessage(UnitBuilderVM.InvalidNameText.ToString(), ColorManager.Red));

            action();
        }

        public static void EditUnit(CharacterObject unit, Action postAction)
        {
            if (unit.IsHero) EditUnitCallback(unit, postAction);
            else
            {
                InformationManager.ShowInquiry(new InquiryData(UnitBuilderVM.EditBodyText.ToString(), UnitBuilderVM.EditMinBodyText.ToString(), true, true,
                    NativeYes.ToString(), NativeNo.ToString(), () => EditUnitMinCallback(unit, postAction, true), () => EditUnitMinCallback(unit, postAction)), true);
            }
        }

        private static void EditUnitMinCallback(CharacterObject unit, Action postAction, bool editMin = false)
        {
            if (editMin)
            {
                CharacterObjectPatch.ModLevel = CharacterObjectPatch.BodyPropertyModification.Minimum;
                CharacterObjectPatch.BaseProperties = unit.GetBodyPropertiesMax();
                EditUnitCallback(unit, postAction);
            }
            else
            {
                InformationManager.ShowInquiry(new InquiryData(UnitBuilderVM.EditBodyText.ToString(), UnitBuilderVM.EditMaxBodyText.ToString(), true, true,
                    NativeYes.ToString(), NativeNo.ToString(), () => EditUnitMaxCallback(unit, postAction), InformationManager.HideInquiry), true);
            }
        }

        private static void EditUnitMaxCallback(CharacterObject unit, Action postAction)
        {
            CharacterObjectPatch.ModLevel = CharacterObjectPatch.BodyPropertyModification.Maximum;
            CharacterObjectPatch.BaseProperties = unit.GetBodyPropertiesMin();
            EditUnitCallback(unit, postAction);
        }

        private static void EditUnitCallback(CharacterObject unit, Action postAction)
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
