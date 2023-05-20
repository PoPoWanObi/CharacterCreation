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
            if (__instance.IsHero)
            {
                //PropertyInfo staticBodyPropertyInfoOnHero = AccessTools.Property(typeof(Hero), "StaticBodyProperties");
                //staticBodyPropertyInfoOnHero.SetValue(__instance.HeroObject, properties.StaticProperties);
                __instance.HeroObject.ModifyPlayersFamilyAppearance(properties.StaticProperties);
                __instance.HeroObject.Weight = properties.Weight;
                __instance.HeroObject.Build = properties.Build;
                __instance.Race = race;
                __instance.HeroObject.UpdatePlayerGender(isFemale);
                CampaignEventDispatcher.Instance.OnPlayerBodyPropertiesChanged();
            }
            else
            {
                CharacterCreationCampaignBehavior.Instance?.SetBodyPropertiesOverride(__instance, properties, race, isFemale);
            }

            if (__instance.IsHero && __instance.HeroObject.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(HeroUpdatedMsg.ToString() + __instance.HeroObject.Name, ColorManager.Purple));
        }
    }
}