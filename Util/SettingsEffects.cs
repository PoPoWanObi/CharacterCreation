using CharacterCreation.Manager;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private const string WarningConflictText = "{=CharacterCreation_WarningConflictText}Override Age is currently set to {OA}, but either Disable Life and Death Cycle {LDC} or Disable Auto Aging {DAA} is turned on. This settings will not be applied.";

        public static SettingsEffects Instance { get; private set; }

        internal static SettingsEffects Initialize(bool reinitialize = false)
        {
            if (Instance == default || reinitialize) Instance = new SettingsEffects();
            return Instance;
        }

        private SettingsEffects()
        {
            //CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_SetAutoAging", SetAutoAging);
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener("DCC_UpdateAllHeroes", UpdateAllHeroes);
        }

        public void SetAutoAging() => SetAutoAging(default); // this is necessary because no arguments literally means no arguments

        // sets the auto-aging depending on game settings
        public void SetAutoAging(Game game = default, bool tempIgnoreSettings = false)
        {
            // this is after code refactoring
            if (/*DCCPerSaveSettings.SaveInstance == default || */tempIgnoreSettings) return;
            if (game == default) game = Game.Current;
            if (game == default) return;

            // pseudocode time
            // if mod setting enables aging (and aging is previously disabled), set everyone's birthday by using default age
            // if mod setting disables aging (and aging is previously enabled), set everyone's birthday by using current age
            CampaignOptions.IsLifeDeathCycleDisabled = !CampaignOptions.IsLifeDeathCycleDisabled;
            var heroList = Hero.AllAliveHeroes;
            foreach (var hero in heroList)
            {
                var age = hero.Age;
                CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, age);
            }
            CampaignOptions.IsLifeDeathCycleDisabled = !CampaignOptions.IsLifeDeathCycleDisabled;

            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"IsLifeDeathCycleDisabled now set to {CampaignOptions.IsLifeDeathCycleDisabled}"));
        }

        public void UpdateAllHeroes() => UpdateAllHeroes(default);

        // Updates all heroes (as if they all need updating anyway)
        public void UpdateAllHeroes(Game game = default, bool tempIgnoreSettings = false)
        {
            if (DCCPerSaveSettings.SaveInstance == default || tempIgnoreSettings) return;
            if (game == default) game = Game.Current;

            if (DCCPerSaveSettings.SaveInstance.OverrideAge)
            {
                if (DCCPerSaveSettings.SaveInstance.DisableAutoAging || CampaignOptions.IsLifeDeathCycleDisabled)
                {
                    var text = new TextObject(WarningConflictText, new Dictionary<string, object>()
                    {
                        ["OA"] = DCCPerSaveSettings.SaveInstance.OverrideAge.ToString(),
                        ["LDC"] = CampaignOptions.IsLifeDeathCycleDisabled.ToString(),
                        ["DAA"] = DCCPerSaveSettings.SaveInstance.DisableAutoAging.ToString()
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
                        CharacterBodyManager.ResetBirthDayForAge(player.CharacterObject, age);
                        CampaignOptions.IsLifeDeathCycleDisabled = false; // reenable to get 'true' age
                    }
                }
            }
        }
    }
}
