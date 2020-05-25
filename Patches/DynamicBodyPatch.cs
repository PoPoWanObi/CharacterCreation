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
            if (Settings.Instance != null && Settings.Instance.IgnoreDailyTick)
            {
                IDictionary dictionary = (IDictionary)AccessTools.Field(typeof(DynamicBodyCampaignBehavior), "_heroBehaviorsDictionary").GetValue(__instance);

                CampaignTime deltaTime = CampaignTime.Now - SubModule.TimeSinceLastSave;
                double yearsElapsed = deltaTime.ToYears;
                SubModule.TimeSinceLastSave = CampaignTime.Now;

                foreach (DictionaryEntry heroBehaviors in dictionary)
                {
                    Hero hero = (Hero)heroBehaviors.Key;

                    if (!Settings.Instance.DisableAutoAging)
                    {
                        if (hero.IsHumanPlayerCharacter && Settings.Instance.DebugMode)
                        {
                            InformationManager.DisplayMessage(new InformationMessage(DebugSetAppearanceMsg.ToString() + hero.Name, ColorManager.Red));
                            var test = new DynamicBodyProperties(hero.DynamicBodyProperties.Age + 12f, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                            InformationManager.DisplayMessage(new InformationMessage(DebugResultMsg.ToString() + test, ColorManager.Red));
                            hero.DynamicBodyProperties.Equals(test);

                            // TODO: Get access to keyValuePair w/ Reflection

                            /*float weight = hero.DynamicBodyProperties.Weight;
                            float build = hero.DynamicBodyProperties.Build;
                            ____heroBehaviorsDictionary.Key.DynamicBodyProperties = new DynamicBodyProperties(____heroBehaviorsDictionary.Key.Age, weight, build;*/
                        }

                        double newAge = hero.DynamicBodyProperties.Age + yearsElapsed;
                        DynamicBodyProperties bodyProperties = new DynamicBodyProperties((float)newAge, hero.DynamicBodyProperties.Weight, hero.DynamicBodyProperties.Build);
                        hero.DynamicBodyProperties = bodyProperties;

                        if (hero.IsHumanPlayerCharacter && Settings.Instance.DebugMode)
                            InformationManager.DisplayMessage(new InformationMessage(SubModule.GetFormattedAgeDebugMessage(hero, hero.DynamicBodyProperties.Age), ColorManager.Red));
                    }
                }
                return false;
            }
            else
                return true;
        }
    }
}
