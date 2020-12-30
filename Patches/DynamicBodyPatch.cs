using CharacterCreation.Manager;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace CharacterCreation.Patches
{
    public static class DynamicBodyPatch
    {
        // localization
        private static readonly TextObject DebugSetAppearanceMsg = new TextObject("{=CharacterCreation_DebugSetAppearanceMsg}[Debug] Set appearance for: "),
            DebugResultMsg = new TextObject("{=CharacterCreation_DebugResultMsg}[Debug] Result: ");

        public static bool Prefix(DynamicBodyCampaignBehavior __instance)
        {
            if (!DCCSettingsUtil.Instance.IgnoreDailyTick) 
                return true;
            
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

            return false;
        }
    }
}