using HarmonyLib;
using SandBox.GauntletUI;
using SandBox.View.Map;
using System;
using System.Reflection;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    public static class BodyGeneratorPatch
    {
        [HarmonyPatch(typeof(BodyGenerator), nameof(BodyGenerator.SaveCurrentCharacter))]
        private static class SaveCurrentCharacter
        {
            static bool Prefix(BodyGenerator __instance)
            {
                try
                {
                   __instance.Character.UpdatePlayerCharacterBodyProperties(__instance.CurrentBodyProperties, __instance.IsFemale);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
                    return true;
                }
            }
        }
    }
}
