using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using System.Reflection;
using TaleWorlds.Library;
using CharacterCreation.Util;
using System;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
    static class CharacterObjectPatch
    {
        private static readonly TextObject HeroUpdatedMsg = new TextObject("{=CharacterCreation_HeroUpdatedMsg}Hero updated: ");

        // ignored for hero modification; both will be applied
        internal static BodyPropertyModification ModLevel { get; set; } = BodyPropertyModification.None;
        // this is the original value of whichever extreme is NOT being edited (see above)
        internal static BodyProperties BaseProperties { get; set; } = BodyProperties.Default;

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
                if (ModLevel == BodyPropertyModification.None) return;

                var minProperties = BaseProperties;
                var maxProperties = BaseProperties;

                if (ModLevel == BodyPropertyModification.Minimum)
                    minProperties = properties;
                else
                    maxProperties = properties;

                __instance.BodyPropertyRange.Init(minProperties, maxProperties);
                __instance.Race = race;
                __instance.IsFemale = isFemale;
            }
            BaseProperties = BodyProperties.Default;
            ModLevel = BodyPropertyModification.None;

            if (__instance.IsHero && __instance.HeroObject.IsHumanPlayerCharacter && DCCSettingsUtil.Instance.DebugMode)
                InformationManager.DisplayMessage(new InformationMessage(HeroUpdatedMsg.ToString() + __instance.HeroObject.Name, ColorManager.Purple));
        }

        internal enum BodyPropertyModification
        {
            None,
            Minimum,
            Maximum,
        }
    }
}