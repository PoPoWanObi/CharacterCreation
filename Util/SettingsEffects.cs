using CharacterCreation.Manager;
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
            // if mod setting disables aging (and aging is previously enabled), set everyone's birthday to update default age
            bool disableAutoAging = DCCSettingsUtil.Instance.DisableAutoAging;
            if (disableAutoAging != CampaignOptions.IsLifeDeathCycleDisabled)
            {
                // only print message update when switching auto-aging on and off, and only when Debug is on
                if (DCCSettingsUtil.Instance.DebugMode)
                    InformationManager.DisplayMessage(
                        new InformationMessage($"DisableAutoAging: {disableAutoAging}, IsLifeDeathCycleDisabled: {CampaignOptions.IsLifeDeathCycleDisabled}"));

                var heroList = game.ObjectManager.GetObjectTypeList<Hero>();
                foreach (var hero in heroList)
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

        // Updates all heroes (as if they all need updating anyway)
        public static void UpdateAllHeroes()
        {
            var heroList = new List<Hero>(Game.Current.ObjectManager.GetObjectTypeList<Hero>());
            foreach (var hero in heroList)
            {
                if (hero.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                {
                    InformationManager.DisplayMessage(new InformationMessage(DebugSetAppearanceMsg.ToString() + hero.Name, ColorManager.Red));
                    var test = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                    InformationManager.DisplayMessage(new InformationMessage(DebugResultMsg.ToString() + test, ColorManager.Red));
                }

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
