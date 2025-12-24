using System.Collections.Generic;
using CharacterCreation.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using static CharacterCreation.Util.DccLocalization;

namespace CharacterCreation.Util
{
    internal class SettingsEffects
    {
        public static SettingsEffects? Instance { get; private set; }

        private bool _isAgingDisabled;

        internal static SettingsEffects Initialize(bool reinitialize = false)
        {
            if (Instance is null || reinitialize) Instance = new SettingsEffects();
            return Instance;
        }

        private SettingsEffects()
        {
            _isAgingDisabled = DccPerSaveSettings.SaveInstance?.DisableAutoAging ?? false;
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_SetAutoAging", SetAutoAging);
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_UpdateAllHeroes", UpdateAllHeroes);
        }

        public void SetAutoAging() => SetAutoAging(null); // this is necessary because no arguments literally means no arguments

        // sets the auto-aging depending on game settings
        public void SetAutoAging(Game? game, bool tempIgnoreSettings = false)
        {
            // this is after code refactoring
            if (DccPerSaveSettings.SaveInstance is null || tempIgnoreSettings) return;
            game ??= Game.Current;
            if (game is null) return;

            if (DccPerSaveSettings.SaveInstance.DisableAutoAging)
            {
                if (_isAgingDisabled) CampaignOptions.IsLifeDeathCycleDisabled = true;
                else _isAgingDisabled = true;
                var heroList = Hero.AllAliveHeroes;
                foreach (var hero in heroList)
                {
                    var age = hero.Age;
                    UnitEditorFunctions.ResetBirthDayForAge(hero.CharacterObject, age);
                }
                CampaignOptions.IsLifeDeathCycleDisabled = false;
            }
            else if (_isAgingDisabled) _isAgingDisabled = false;

            if (DccSettings.Instance!.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"DisableAutoAging is {DccPerSaveSettings.SaveInstance.DisableAutoAging}"));
        }

        public void UpdateAllHeroes() => UpdateAllHeroes(null);

        // Updates all heroes (as if they all need updating anyway)
        public void UpdateAllHeroes(Game? game, bool tempIgnoreSettings = false)
        {
            if (DccPerSaveSettings.SaveInstance is null || tempIgnoreSettings) return;
            game ??= Game.Current;
            if (game is null) return;

            if (DccPerSaveSettings.SaveInstance.OverrideAge)
            {
                if (CampaignOptions.IsLifeDeathCycleDisabled)
                {
                    var text = new TextObject(WarningConflictText, new Dictionary<string, object>()
                    {
                        ["OA"] = DccPerSaveSettings.SaveInstance.OverrideAge.ToString(),
                        ["LDC"] = DccPerSaveSettings.SaveInstance.DisableAutoAging.ToString()
                    });
                    InformationManager.ShowInquiry(new InquiryData(WarningTitleTextObject.ToString(), text.ToString(),
                        true, false, WarningAck, null,
                        InformationManager.HideInquiry, InformationManager.HideInquiry), true);
                }
                else
                {
                    var player = Hero.MainHero;
                    if (player != null)
                    {
                        CampaignOptions.IsLifeDeathCycleDisabled = true; // disable life cycle to get default age
                        var age = player.Age;
                        UnitEditorFunctions.ResetBirthDayForAge(player.CharacterObject, age);
                        CampaignOptions.IsLifeDeathCycleDisabled = false; // reenable to get 'true' age
                    }
                }
            }

            if (DccSettings.Instance!.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"OverrideAge is {DccPerSaveSettings.SaveInstance.OverrideAge}"));
        }
    }
}
