using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CharacterCreation.Util
{
    class SettingsEffects
    {
        // localization
        private static readonly TextObject
            WarningTitle = new TextObject("{=CharacterCreation_WarningTitle}WARNING"),
            WarningAck = new TextObject("{=CharacterCreation_WarningAck}Acknowledged");

        private const string WarningConflictText = "{=CharacterCreation_WarningConflictText}Override Age is currently set to {OA}, but Disable Life and Death Cycle {LDC} is turned on. This settings will not be applied.";

        public static SettingsEffects Instance { get; private set; }

        private bool isAgingDisabled;

        internal static SettingsEffects Initialize(bool reinitialize = false)
        {
            if (Instance == default || reinitialize) Instance = new SettingsEffects();
            return Instance;
        }

        private SettingsEffects()
        {
            isAgingDisabled = DCCPerSaveSettings.SaveInstance?.DisableAutoAging ?? false;
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_SetAutoAging", SetAutoAging);
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_UpdateAllHeroes", UpdateAllHeroes);
        }

        public void SetAutoAging() => SetAutoAging(default); // this is necessary because no arguments literally means no arguments

        // sets the auto-aging depending on game settings
        public void SetAutoAging(Game game = default, bool tempIgnoreSettings = false)
        {
            // this is after code refactoring
            if (DCCPerSaveSettings.SaveInstance == default || tempIgnoreSettings) return;
            if (game == default) game = Game.Current;
            if (game == default) return;

            if (DCCPerSaveSettings.SaveInstance.DisableAutoAging)
            {
                if (isAgingDisabled) CampaignOptions.IsLifeDeathCycleDisabled = true;
                else isAgingDisabled = true;
                var heroList = Hero.AllAliveHeroes;
                foreach (var hero in heroList)
                {
                    var age = hero.Age;
                    UnitEditorFunctions.ResetBirthDayForAge(hero.CharacterObject, age);
                }
                CampaignOptions.IsLifeDeathCycleDisabled = false;
            }
            else if (isAgingDisabled) isAgingDisabled = false;

            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"DisableAutoAging is {DCCPerSaveSettings.SaveInstance.DisableAutoAging}"));
        }

        public void UpdateAllHeroes() => UpdateAllHeroes(default);

        // Updates all heroes (as if they all need updating anyway)
        public void UpdateAllHeroes(Game game = default, bool tempIgnoreSettings = false)
        {
            if (DCCPerSaveSettings.SaveInstance == default || tempIgnoreSettings) return;
            if (game == default) game = Game.Current;

            if (DCCPerSaveSettings.SaveInstance.OverrideAge)
            {
                if (CampaignOptions.IsLifeDeathCycleDisabled)
                {
                    var text = new TextObject(WarningConflictText, new Dictionary<string, object>()
                    {
                        ["OA"] = DCCPerSaveSettings.SaveInstance.OverrideAge.ToString(),
                        ["LDC"] = DCCPerSaveSettings.SaveInstance.DisableAutoAging.ToString()
                    });
                    InformationManager.ShowInquiry(new InquiryData(WarningTitle.ToString(), text.ToString(), true, false, WarningAck.ToString(), null,
                        InformationManager.HideInquiry, InformationManager.HideInquiry), true);
                }
                else
                {
                    var player = Hero.MainHero;
                    if (player != default)
                    {
                        CampaignOptions.IsLifeDeathCycleDisabled = true; // disable life cycle to get default age
                        var age = player.Age;
                        UnitEditorFunctions.ResetBirthDayForAge(player.CharacterObject, age);
                        CampaignOptions.IsLifeDeathCycleDisabled = false; // reenable to get 'true' age
                    }
                }
            }

            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"OverrideAge is {DCCPerSaveSettings.SaveInstance.OverrideAge}"));
        }
    }
}
