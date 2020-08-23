﻿using CharacterCreation.Manager;
using HarmonyLib;
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using static TaleWorlds.CampaignSystem.Hero;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(DynamicBodyCampaignBehavior), "OnDailyTick")]
    public static class DynamicBodyPatch
    {
        // Rough exposure of DynamicBodyCampaignBehavior.HeroBehaviors struct. You are welcome. - Designer225
        private static readonly Type HeroBehaviorsStructType = AccessTools.Inner(typeof(DynamicBodyCampaignBehavior), "HeroBehaviors");
        private static readonly FieldInfo LastSettlementVisitTimeField = AccessTools.Field(HeroBehaviorsStructType, "LastSettlementVisitTime"); // type: CampaignTime
        private static readonly FieldInfo InASettlementField = AccessTools.Field(HeroBehaviorsStructType, "InASettlement"); // type: bool
        private static readonly FieldInfo LastEncounterTimeField = AccessTools.Field(HeroBehaviorsStructType, "LastEncounterTime"); // type: CampaignTime
        private static readonly FieldInfo IsBattleEncounteredField = AccessTools.Field(HeroBehaviorsStructType, "IsBattleEncountered"); // type: bool

        // localization
        private static readonly TextObject DebugSetAppearanceMsg = new TextObject("{=CharacterCreation_DebugSetAppearanceMsg}[Debug] Set appearance for: "),
            DebugResultMsg = new TextObject("{=CharacterCreation_DebugResultMsg}[Debug] Result: ");

        //static bool Prefix(DynamicBodyCampaignBehavior __instance, ref Dictionary<Hero, object> ____heroBehaviorsDictionary)
        static bool Prefix(DynamicBodyCampaignBehavior __instance)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.IgnoreDailyTick)
            {
                IDictionary dictionary = (IDictionary)AccessTools.Field(typeof(DynamicBodyCampaignBehavior), "_heroBehaviorsDictionary").GetValue(__instance);

                CampaignTime deltaTime = CampaignTime.Now - SubModule.TimeSinceLastSave;
                double yearsElapsed = deltaTime.ToYears;
                SubModule.TimeSinceLastSave = CampaignTime.Now;

                foreach (DictionaryEntry heroBehaviors in dictionary)
                {
                    Hero hero = (Hero)heroBehaviors.Key;

                    if (!DCCSettings.Instance.DisableAutoAging)
                    {
                        if (hero.IsHumanPlayerCharacter && DCCSettings.Instance.DebugMode)
                        {
                            InformationManager.DisplayMessage(new InformationMessage(DebugSetAppearanceMsg.ToString() + hero.Name, ColorManager.Red));
                            var test = new DynamicBodyProperties(hero.Age + 12f, hero.Weight, hero.Build);
                            InformationManager.DisplayMessage(new InformationMessage(DebugResultMsg.ToString() + test, ColorManager.Red));
                            hero.BodyProperties.DynamicProperties.Equals(test);

                            // TODO: Get access to keyValuePair w/ Reflection

                            /*float weight = hero.DynamicBodyProperties.Weight;
                            float build = hero.DynamicBodyProperties.Build;
                            ____heroBehaviorsDictionary.Key.DynamicBodyProperties = new DynamicBodyProperties(____heroBehaviorsDictionary.Key.Age, weight, build;*/
                        }

                        //double newAge = hero.Age + yearsElapsed;
                        DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                        BodyProperties heroBodyProperties = hero.BodyProperties;
                        CharacterBodymanager.copyDynamicBodyProperties(dynamicBodyProperties, heroBodyProperties.DynamicProperties);
                        hero.CharacterObject.UpdatePlayerCharacterBodyProperties(heroBodyProperties, hero.IsFemale);

                        if (hero.IsHumanPlayerCharacter && DCCSettings.Instance.DebugMode)
                            InformationManager.DisplayMessage(new InformationMessage(SubModule.GetFormattedAgeDebugMessage(hero, hero.Age), ColorManager.Red));
                    }
                }
                return false;
            }
            else
                return true;
        }
    }
}