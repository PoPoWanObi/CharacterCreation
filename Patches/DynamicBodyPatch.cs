using CharacterCreation.Manager;
using HarmonyLib;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Localization;

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
            if (DCCSettings.Instance == null || !DCCSettings.Instance.IgnoreDailyTick) 
                return true;

            //CampaignTime deltaTime = CampaignTime.Now - SubModule.TimeSinceLastSave;
            CampaignTime deltaTime = SubModule.GetDeltaTime(true);
            double yearsElapsed = deltaTime.ToYears;
            //SubModule.TimeSinceLastSave = CampaignTime.Now;

            if (!DCCSettings.Instance.DisableAutoAging)
            {
                IDictionary dictionary = (IDictionary)AccessTools.Field(typeof(DynamicBodyCampaignBehavior), "_heroBehaviorsDictionary").GetValue(__instance);
                foreach (var hero in dictionary.Keys.Cast<Hero>())
                {
                    if (hero.IsHumanPlayerCharacter && DCCSettings.Instance.DebugMode)
                    {
                        InformationManager.DisplayMessage(new InformationMessage(DebugSetAppearanceMsg.ToString() + hero.Name, ColorManager.Red));
                        var test = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                        InformationManager.DisplayMessage(new InformationMessage(DebugResultMsg.ToString() + test, ColorManager.Red));
                        hero.BodyProperties.DynamicProperties.Equals(test);
                    }

                    // TODO:: Why is this conflicting now???
                    /*double newAge = hero.Age + yearsElapsed;
                            DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties((float)newAge, hero.Weight, hero.Build);*/

                    DynamicBodyProperties dynamicBodyProperties = new DynamicBodyProperties(hero.Age, hero.Weight, hero.Build);
                    BodyProperties heroBodyProperties = new BodyProperties(dynamicBodyProperties, hero.BodyProperties.StaticProperties);
                    //BodyProperties heroBodyProperties = hero.BodyProperties;
                    //CharacterBodyManager.CopyDynamicBodyProperties(dynamicBodyProperties, heroBodyProperties.DynamicProperties);
                    hero.CharacterObject.UpdatePlayerCharacterBodyProperties(heroBodyProperties, hero.IsFemale);

                    if (hero.IsHumanPlayerCharacter && DCCSettings.Instance.DebugMode)
                        InformationManager.DisplayMessage(new InformationMessage(SubModule.GetFormattedAgeDebugMessage(hero, hero.Age), ColorManager.Red));
                }
            }

            return false;
        }
    }
}