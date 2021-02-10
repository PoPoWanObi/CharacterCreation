using CharacterCreation.Manager;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CharacterCreation.Util
{
    class SettingsEffects
    {
        // localization
        private static readonly TextObject DebugSetAppearanceMsg = new TextObject("{=CharacterCreation_DebugSetAppearanceMsg}[Debug] Set appearance for: "),
            DebugResultMsg = new TextObject("{=CharacterCreation_DebugResultMsg}[Debug] Result: "),

            WarningTitle = new TextObject("{=CharacterCreation_WarningTitle}WARNING"),
            WarningYes = new TextObject("{=CharacterCreation_WarningYes}Yes"),
            WarningNo = new TextObject("{=CharacterCreation_WarningNo}No"),
            WarningAck = new TextObject("{=CharacterCreation_WarningAck}Acknowledged");

        private const string WarningText = "{=CharacterCreation_WarningText}Disable Life and Death Cycle is currently set to {LDC}, but Disable Auto Aging is set to {DAA}. Are you sure you want to change the campaign settings? If selecting 'no', changes to the life and death cycle will not take effect until you load or start a campaign.",
            WarningOverrideText = "{=CharacterCreation_WarningOverrideText}Override Age is currently set to {OA}. Are you sure you want your hero to be unaffected by aging? Your choice here will affect your current game, until you load or start a campaign.",
            WarningConflictText = "{=CharacterCreation_WarningConflictText}Override Age is currently set to {OA}, but either Disable Life and Death Cycle {LDC} or Disable Auto Aging {DAA} is turned on. This settings will not be applied.";

        public static SettingsEffects Instance { get; private set; }

        internal static SettingsEffects Initialize(bool reinitialize = false)
        {
            if (Instance == default || reinitialize) Instance = new SettingsEffects();
            return Instance;
        }

        private bool ignoreAutoAgingSettings;
        private bool ignoreAgeOverrideSettings;
        private bool isAgeOverrideOn;

        private SettingsEffects()
        {
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener(this, SetAutoAging);
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener(this, UpdateAllHeroes);
            ignoreAutoAgingSettings = false;
            ignoreAgeOverrideSettings = false;
            isAgeOverrideOn = false;
        }

        public void SetAutoAging() => SetAutoAging(default); // this is necessary because no arguments literally means no arguments

        // sets the auto-aging depending on game settings
        public void SetAutoAging(Game game = default, bool tempIgnoreSettings = false)
        {
            // this is after code refactoring
            if (game == default) game = Game.Current;

            if (ignoreAutoAgingSettings || tempIgnoreSettings) return;

            // pseudocode time
            // if mod setting enables aging (and aging is previously disabled), set everyone's birthday by using default age
            // if mod setting disables aging (and aging is previously enabled), set everyone's birthday by using current age
            if (DCCSettingsUtil.Instance.DisableAutoAging != CampaignOptions.IsLifeDeathCycleDisabled)
            {
                var text = new TextObject(WarningText, new Dictionary<string, object>
                {
                    ["LDC"] = CampaignOptions.IsLifeDeathCycleDisabled.ToString(),
                    ["DAA"] = DCCSettingsUtil.Instance.DisableAutoAging.ToString()
                });
                InformationManager.ShowInquiry(new InquiryData(WarningTitle.ToString(), text.ToString(), true, true,
                    WarningYes.ToString(), WarningNo.ToString(), () => ChangeAutoAgingSettings(game), () => ignoreAutoAgingSettings = true), true);
            }
        }

        private void ChangeAutoAgingSettings(Game game)
        {
            var heroList = game.ObjectManager.GetObjectTypeList<Hero>();
            foreach (var hero in heroList.Where(x => x.IsAlive)) // ignore dead heroes
            {
                var age = hero.Age;
                CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, age);
            }
            CampaignOptions.IsLifeDeathCycleDisabled = DCCSettingsUtil.Instance.DisableAutoAging;

            if (DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(
                    new InformationMessage($"IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));
        }

        public void UpdateAllHeroes() => UpdateAllHeroes(default);

        // Updates all heroes (as if they all need updating anyway)
        public void UpdateAllHeroes(Game game = default, bool tempIgnoreSettings = false)
        {
            if (game == default) game = Game.Current;

            if (!ignoreAgeOverrideSettings && !tempIgnoreSettings && DCCSettingsUtil.Instance.OverrideAge)
            {
                if (DCCSettingsUtil.Instance.DisableAutoAging || CampaignOptions.IsLifeDeathCycleDisabled)
                {
                    var text = new TextObject(WarningConflictText, new Dictionary<string, object>()
                    {
                        ["OA"] = DCCSettingsUtil.Instance.OverrideAge.ToString(),
                        ["LDC"] = CampaignOptions.IsLifeDeathCycleDisabled.ToString(),
                        ["DAA"] = DCCSettingsUtil.Instance.DisableAutoAging.ToString()
                    });
                    InformationManager.ShowInquiry(new InquiryData(WarningTitle.ToString(), text.ToString(), true, false, WarningAck.ToString(), null,
                        InformationManager.HideInquiry, InformationManager.HideInquiry), true);
                }
                else
                {
                    var text = new TextObject(WarningOverrideText, new Dictionary<string, object>()
                    {
                        ["OA"] = DCCSettingsUtil.Instance.OverrideAge.ToString()
                    });
                    InformationManager.ShowInquiry(new InquiryData(WarningTitle.ToString(), text.ToString(), true, true, WarningYes.ToString(), WarningNo.ToString(),
                        () => ResetPlayerAge(game), InformationManager.HideInquiry), true);
                }
                ignoreAgeOverrideSettings = true;
            }

            if (isAgeOverrideOn) ResetPlayerAge(game);
            UpdateHeroBodyProperties(game);
        }

        private void ResetPlayerAge(Game game)
        {
            isAgeOverrideOn = true;
            var player = game.ObjectManager.GetObjectTypeList<Hero>().FirstOrDefault(x => x.IsHumanPlayerCharacter);
            if (player != default)
            {
                CampaignOptions.IsLifeDeathCycleDisabled = true; // disable life cycle to get default age
                var age = player.Age;
                CharacterBodyManager.ResetBirthDayForAge(player.CharacterObject, age);
                CampaignOptions.IsLifeDeathCycleDisabled = false; // reenable to get 'true' age
            }
        }

        private void UpdateHeroBodyProperties(Game game)
        {
            foreach (var hero in game.ObjectManager.GetObjectTypeList<Hero>().Where(x => x.IsAlive)) // again, ignore dead heroes
            {
                if (hero.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                {
                    InformationManager.DisplayMessage(new InformationMessage(DebugSetAppearanceMsg.ToString() + hero.Name, ColorManager.Red));
                    var test = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                    InformationManager.DisplayMessage(new InformationMessage(DebugResultMsg.ToString() + test, ColorManager.Red));
                }

                // update so party name and character name on save is correct (skipped - party name now dynamically handled)
                hero.FirstName = hero.Name;

                // the below code might be unnecessary in light of the way TaleWorlds implements aging now. Dynamic campaign body 'enhancement', on the other hand...
                float age = hero.Age;
                DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties(age, hero.Weight, hero.Build);
                BodyProperties heroBodyProperties = new BodyProperties(dynamicBodyProperties, hero.BodyProperties.StaticProperties);
                hero.CharacterObject.UpdatePlayerCharacterBodyProperties(heroBodyProperties, hero.IsFemale);

                if (hero.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                    InformationManager.DisplayMessage(new InformationMessage(SubModule.GetFormattedAgeDebugMessage(hero, age), ColorManager.Red));
            }
        }
    }
}
