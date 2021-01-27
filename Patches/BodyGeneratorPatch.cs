using CharacterCreation.Manager;
using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(BodyGenerator), nameof(BodyGenerator.SaveCurrentCharacter))]
    public static class BodyGeneratorPatch
    {
        static void Postfix(BodyGenerator __instance)
        {
            if (__instance.Character is CharacterObject characterObject)
            {
                float bodyAge = __instance.CurrentBodyProperties.DynamicProperties.Age;
                CharacterBodyManager.ResetBirthDayForAge(characterObject, bodyAge);
                if (DCCSettingsUtil.Instance.DebugMode)
                    Debug.Print($"[CharacterCreation] Character {characterObject.Name} expected age: {bodyAge}, actual: {characterObject.Age}");
            }
        }
    }
}