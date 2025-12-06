using System.Collections.Generic;
using System.Reflection.Emit;
using CharacterCreation.Util;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(BodyGenerator), nameof(BodyGenerator.SaveCurrentCharacter))]
    public static class BodyGeneratorPatch
    {
        static void Postfix(BodyGenerator __instance)
        {
            if (DCCSettingsUtil.Instance.PatchAgeNotUpdatingOnCharEdit && __instance.Character is CharacterObject characterObject)
            {
                float bodyAge = __instance.CurrentBodyProperties.DynamicProperties.Age;
                UnitEditorFunctions.ResetBirthDayForAge(characterObject, bodyAge);
                if (DCCSettingsUtil.Instance.DebugMode)
                {
                    var msg =
                        $"[CharacterCreation] Character {characterObject.Name} expected age: {bodyAge}, actual: {characterObject.Age}";
                    Debug.Print(msg);
                    InformationManager.DisplayMessage(new InformationMessage(msg, ColorManager.Red));
                }
            }
        }
    }
}