using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using System.Reflection;
using TaleWorlds.Library;
using CharacterCreation.Util;
using System;
using CharacterCreation.CampaignSystem;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
    static class CharacterObjectPatch
    {
        private static readonly TextObject HeroUpdatedMsg = new TextObject("{=CharacterCreation_HeroUpdatedMsg}Hero updated: ");

        private static void Postfix(CharacterObject __instance, BodyProperties properties, int race, bool isFemale)
        {
            if (!DCCSettingsUtil.Instance.FixCharEditEffectOnNPC) return;

            if (DCCSettingsUtil.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Preparing edit to {__instance.GetName()}. Properties: ({__instance.GetBodyProperties(__instance.Equipment).ToString()}), Race: {__instance.Race.ToString()}, Female: {__instance.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
                msg =
                    $"[CharacterCreation] Applying changes to {__instance.GetName()}. Properties: ({properties.ToString()}), Race: {race.ToString()}, Female: {isFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
            
            if (__instance.IsHero)
            {
                //PropertyInfo staticBodyPropertyInfoOnHero = AccessTools.Property(typeof(Hero), "StaticBodyProperties");
                //staticBodyPropertyInfoOnHero.SetValue(__instance.HeroObject, properties.StaticProperties);
                __instance.HeroObject.StaticBodyProperties = properties.StaticProperties;
                __instance.HeroObject.Weight = properties.Weight;
                __instance.HeroObject.Build = properties.Build;
                __instance.Race = race;
                __instance.HeroObject.IsFemale = isFemale;
                CampaignEventDispatcher.Instance.OnPlayerBodyPropertiesChanged();
                
                if (DCCSettingsUtil.Instance.DebugMode)
                {
                    var msg = HeroUpdatedMsg.ToString() + __instance.HeroObject.Name;
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Purple));
                    Debug.Print(msg);
                }
            }
            else
            {
                CharacterCreationCampaignBehavior.Instance?.SetBodyPropertiesOverride(__instance, properties, race, isFemale);
            }
            
            if (DCCSettingsUtil.Instance.DebugMode)
            {
                var msg =
                    $"[CharacterCreation] Changes applied to {__instance.GetName()}. Properties: ({__instance.GetBodyProperties(__instance.Equipment).ToString()}), Race: {__instance.Race.ToString()}, Female: {__instance.IsFemale.ToString()}";
                Debug.Print(msg);
                InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.White));
            }
        }
    }
}