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
            DebugResultMsg = new TextObject("{=CharacterCreation_DebugResultMsg}[Debug] Result: ");

        private static SettingsEffects instance;

        public static SettingsEffects Instance => Initialize();

        internal static SettingsEffects Initialize()
        {
            if (instance == default) instance = new SettingsEffects();
            return instance;
        }

        private SettingsEffects()
        {
            CampaignEvents.HourlyTickEvent?.AddNonSerializedListener(this, SetAutoAging);
            CampaignEvents.DailyTickEvent?.AddNonSerializedListener(this, UpdateAllHeroes);
        }

        public static void SetAutoAging() => SetAutoAging(default); // this is necessary because no arguments literally means no arguments

        // sets the auto-aging depending on game settings
        public static void SetAutoAging(Game game = default)
        {
            // this is after code refactoring
            if (game == default) game = Game.Current;

            // pseudocode time
            // if mod setting enables aging (and aging is previously disabled), set everyone's birthday by using default age
            // if mod setting disables aging (and aging is previously enabled), set everyone's birthday by using current age
            bool disableAutoAging = DCCSettingsUtil.Instance.DisableAutoAging;
            if (disableAutoAging != CampaignOptions.IsLifeDeathCycleDisabled)
            {
                // only print message update when switching auto-aging on and off, and only when Debug is on
                if (DCCSettingsUtil.Instance.DebugMode)
                    InformationManager.DisplayMessage(
                        new InformationMessage($"DisableAutoAging: {disableAutoAging}, IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));

                var heroList = game.ObjectManager.GetObjectTypeList<Hero>();
                foreach (var hero in heroList.Where(x => x.IsAlive)) // ignore dead heroes
                {
                    var age = hero.Age;
                    CharacterBodyManager.ResetBirthDayForAge(hero.CharacterObject, age);
                }
                CampaignOptions.IsLifeDeathCycleDisabled = disableAutoAging;

                if (DCCSettingsUtil.Instance.DebugMode)
                    InformationManager.DisplayMessage(
                        new InformationMessage($"DisableAutoAging: {disableAutoAging}, IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));
            }
        }

        public static void UpdateAllHeroes() => UpdateAllHeroes(default);

        // Updates all heroes (as if they all need updating anyway)
        public static void UpdateAllHeroes(Game game = default)
        {
            if (game == default) game = Game.Current;

            // only override player hero aging if life cycle is not disabled
            if (DCCSettingsUtil.Instance.OverrideAge && (!DCCSettingsUtil.Instance.DisableAutoAging || !CampaignOptions.IsLifeDeathCycleDisabled))
            {
                var player = game.ObjectManager.GetObjectTypeList<Hero>().FirstOrDefault(x => x.IsHumanPlayerCharacter);
                if (player != default)
                {
                    CampaignOptions.IsLifeDeathCycleDisabled = true; // disable life cycle to get default age
                    var age = player.Age;
                    CharacterBodyManager.ResetBirthDayForAge(player.CharacterObject, age);
                    CampaignOptions.IsLifeDeathCycleDisabled = false; // reenable to get 'true' age
                }
            }

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
