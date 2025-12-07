using CharacterCreation.UI;
using Helpers;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using CharacterCreation.CampaignSystem;
using CharacterCreation.CampaignSystem.GameState;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.GameState;

using static CharacterCreation.DccLocalization;

namespace CharacterCreation.Util
{
    public static class UnitEditorFunctions
    {
        private static readonly TextObject
            NativeYes = new TextObject("{=aeouhelq}Yes"),
            NativeNo = new TextObject("{=8OkPHu4f}No"),
            NativeCancel = new TextObject("{=3CpNUnVl}Cancel");

        public static void RenameUnit(CharacterObject unit, Action? postAction = null)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(ChangingNameForText.ToString() + unit.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(CharacterRenamerText.ToString(), EnterNewNameText.ToString(),
                true, true, RenameText.ToString(), NativeCancel.ToString(), x => RenameUnit(x, unit, postAction),
                InformationManager.HideInquiry, false, CampaignUIHelper.IsStringApplicableForHeroName), true);
        }

        private static void RefreshEncyclopediaPage()
        {
            var encyclopediaManager = MapScreen.Instance?.EncyclopediaScreenManager;
            if (encyclopediaManager is null) return;
            encyclopediaManager.CloseEncyclopedia();
            MapScreen.Instance?.OpenEncyclopedia();
        }

        private static void RenameUnit(string unitName, CharacterObject selectedUnit, Action? action = null)
        {
            if (!string.IsNullOrEmpty(unitName))
            {
                if (selectedUnit.IsHero) selectedUnit.HeroObject.SetName(new TextObject(unitName), new TextObject(unitName));
                else
                    CharacterCreationCampaignBehavior.Instance?.SetUnitNameOverride(selectedUnit, unitName);
                RefreshEncyclopediaPage();
            }
            else InformationManager.DisplayMessage(new InformationMessage(InvalidNameText.ToString(), ColorManager.Red));

            action?.Invoke();
        }

        public static void UndoRename(CharacterObject selectedUnit, Action? postAction = null)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(CharacterUnrenamerText.ToString(), UnrenameWarningText.ToString(),
                true, true, NativeYes.ToString(), NativeNo.ToString(), () =>
                {
                    CharacterCreationCampaignBehavior.Instance?.UndoUnitNameOverride(selectedUnit);
                    postAction?.Invoke();
                    RefreshEncyclopediaPage();
                }, InformationManager.HideInquiry), true);
        }

        public static void EditUnit(CharacterObject unit, Action? postAction = null)
        {
            postAction?.Invoke();
            FaceGen.ShowDebugValues = true;

            if (!unit.IsHero)
            {
                InformationManager.DisplayMessage(new InformationMessage());
            }
            
            GameStateManager.Current.PushState(Game.Current.GameStateManager.CreateState<CharacterEditorState>(unit));
        }

        public static void UndoEdit(CharacterObject selectedUnit, Action? postAction = null)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(CharacterUneditText.ToString(), UneditWarningText.ToString(),
                true, true, NativeYes.ToString(), NativeNo.ToString(), () =>
                {
                    CharacterCreationCampaignBehavior.Instance?.UndoBodyPropertiesOverride(selectedUnit);
                    postAction?.Invoke();
                    RefreshEncyclopediaPage();
                }, InformationManager.HideInquiry), true);
        }

        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            if (!characterObject.IsHero) return;
            characterObject.HeroObject.SetBirthDay(randomize ? HeroHelper.GetRandomBirthDayForAge(targetAge) : CampaignTime.YearsFromNow(-targetAge));
        }
    }
}
