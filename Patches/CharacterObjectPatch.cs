using System;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;
using System.Reflection;
using Helpers;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(CharacterObject), nameof(CharacterObject.UpdatePlayerCharacterBodyProperties))]
    static class CharacterObjectPatch
    {
        static void Postfix(CharacterObject __instance, BodyProperties properties, bool isFemale)
        {
            if (__instance.IsHero)
            {
                if (Settings.Instance != null && Settings.Instance.DebugMode)
                    InformationManager.DisplayMessage(new InformationMessage("Hero updated: " + __instance.HeroObject.Name, ColorManager.Purple));

                if (Settings.Instance != null && !Settings.Instance.OverrideAge)
                    __instance.HeroObject.BirthDay = HeroHelper.GetRandomBirthDayForAge(properties.DynamicProperties.Age);
            }
        }
    }
}
