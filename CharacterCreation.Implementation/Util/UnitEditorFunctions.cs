using Helpers;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using CharacterCreation.CampaignSystem;
using CharacterCreation.CampaignSystem.GameState;
using CharacterCreation.Settings;
using SandBox.View.Map;
using TaleWorlds.Core.ImageIdentifiers;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Util
{
    public static class UnitEditorFunctions
    {
        public static void RenameUnit(CharacterObject unit, Action? postAction = null)
        {
            if (DccSettings.Instance!.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(ChangingNameForText + unit.Name));

            InformationManager.ShowTextInquiry(new TextInquiryData(CharacterRenamerText, EnterNewNameText,
                true, true, RenameText, NativeCancel, x => RenameUnit(x, unit, postAction),
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
            else InformationManager.DisplayMessage(new InformationMessage(InvalidNameText, ColorManager.Red));

            action?.Invoke();
        }

        public static void UndoRename(CharacterObject selectedUnit, Action? postAction = null)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(CharacterUnrenamerText, UnrenameWarningText,
                true, true, NativeYes, NativeNo, () =>
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
                MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(TroopEditTitle,
                    TroopEditText, new List<InquiryElement>
                    {
                        new InquiryElement("BodyProperties", BodyPropertiesButton,
                            new EmptyImageIdentifier()),
                        new InquiryElement("Tags", TagsButton,
                            new EmptyImageIdentifier())
                    }, true, 1, 1, NativeContinue, NativeCancel,
                    list =>
                    {
                        if (list[0].Identifier.Equals("BodyProperties"))
                            EditUnitBodyProperties(unit);
                        else
                            EditUnitTags(unit);
                    },
                    _ => InformationManager.HideInquiry()));
            }
            else
                GameStateManager.Current.PushState(
                    Game.Current.GameStateManager.CreateState<CharacterEditorState>(unit));
        }

        private static void EditUnitBodyProperties(CharacterObject unit)
        {
            MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(TroopEditTitle,
                TroopEditPropertiesText, new List<InquiryElement>
                {
                    new InquiryElement("MinBodyProperties", MinPropertiesButton,
                        new EmptyImageIdentifier()),
                    new InquiryElement("MaxBodyProperties", MaxPropertiesButton,
                        new EmptyImageIdentifier())
                }, true, 1, 2, NativeContinue, NativeCancel,
                list =>
                {
                    CharacterEditorStatePropertyType flag = default;
                    foreach (var e in list)
                    {
                        if (e.Identifier.Equals("MinBodyProperties")) flag |= CharacterEditorStatePropertyType.MinProperties;
                        else if (e.Identifier.Equals("MaxBodyProperties")) flag |= CharacterEditorStatePropertyType.MaxProperties;
                    }

                    GameStateManager.Current.PushState(
                        Game.Current.GameStateManager.CreateState<CharacterEditorState>(unit, flag));
                },
                _ => InformationManager.HideInquiry()));
        }

        private static void EditUnitTags(CharacterObject unit)
        {
            MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(TroopEditTitle,
                TroopEditTagsText, new List<InquiryElement>
                {
                    new InquiryElement("HairTags", HairTagsButton, new EmptyImageIdentifier()),
                    new InquiryElement("BeardTags", BeardTagsButton, new EmptyImageIdentifier()),
                    new InquiryElement("TattooTags", TattooTagsButton, new EmptyImageIdentifier())
                }, true, 1, 1, NativeContinue, NativeCancel,
                list =>
                {
                    var type = list[0].Identifier switch
                    {
                        "HairTags" => TroopTagEditType.Hair,
                        "BeardTags" => TroopTagEditType.Beard,
                        "TattooTags" => TroopTagEditType.Tattoo,
                        _ => default
                    };
                    if (type == default)
                    {
                        var msg = "[CharacterCreation] Unknown tag edit type: " + list[0].Identifier;
                        InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                        Debug.Print(msg);
                        return;
                    }
                    
                    OnEditUnitTags(unit, type);
                }, _ => InformationManager.HideInquiry()));
        }

        private static void OnEditUnitTags(CharacterObject unit, TroopTagEditType type)
        {
            InformationManager.ShowTextInquiry(new TextInquiryData(TroopEditTitle, TagEditText,
                true, true, NativeYes, NativeNo, x =>
                {
                    x ??= string.Empty;
                    switch (type)
                    {
                        case TroopTagEditType.Hair:
                            CharacterCreationCampaignBehavior.Instance?.SetTagOverride(unit, hairTags: x);
                            break;
                        case TroopTagEditType.Beard:
                            CharacterCreationCampaignBehavior.Instance?.SetTagOverride(unit, beardTags: x);
                            break;
                        case TroopTagEditType.Tattoo:
                            CharacterCreationCampaignBehavior.Instance?.SetTagOverride(unit, tattooTags: x);
                            break;
                        default:
                            var msg = "[CharacterCreation] Unknown tag edit type: " + type;
                            InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                            Debug.Print(msg);
                            break;
                    }
                    RefreshEncyclopediaPage();
                }, InformationManager.HideInquiry, defaultInputText: GetDefaultInputText(unit, type)), true);
        }

        private static string GetDefaultInputText(CharacterObject unit, TroopTagEditType type)
        {
            return type switch
            {
                TroopTagEditType.Hair => unit.BodyPropertyRange.HairTags,
                TroopTagEditType.Beard => unit.BodyPropertyRange.BeardTags,
                TroopTagEditType.Tattoo => unit.BodyPropertyRange.TattooTags,
                _ => string.Empty
            };
        }

        public static void UndoEdit(CharacterObject selectedUnit, Action? postAction = null)
        {
            if (selectedUnit.IsHero) return; // this should not happen, so here's a sanity check.

            InformationManager.ShowInquiry(new InquiryData(CharacterUneditText, UneditWarningText,
                true, true, NativeYes, NativeNo, () =>
                {
                    CharacterCreationCampaignBehavior.Instance?.UndoBodyPropertiesOverride(selectedUnit);
                    postAction?.Invoke();
                    RefreshEncyclopediaPage();
                }, InformationManager.HideInquiry), true);
        }

        public static void ResetBirthDayForAge(CharacterObject characterObject, float targetAge, bool randomize = false)
        {
            var randomizedBirthday = randomize
                ? HeroHelper.GetRandomBirthDayForAge(targetAge)
                : CampaignTime.YearsFromNow(-targetAge);
            var actualAge = randomizedBirthday.ElapsedYearsUntilNow;

            characterObject.Age = actualAge;
            if (characterObject.IsHero)
            {
                var hero = characterObject.HeroObject;
                if (hero.IsAlive)
                    hero.SetBirthDay(randomizedBirthday);
                else
                    hero.SetDeathDay(hero.BirthDay + CampaignTime.Years(actualAge));
            }
        }
    }

    internal enum TroopTagEditType
    {
        Hair = 1,
        Beard = 2,
        Tattoo = 3
    }
}
