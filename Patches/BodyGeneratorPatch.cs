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
    public class BodyGeneratorPatch
    {
        [HarmonyPatch(typeof(BodyGenerator), "SaveCurrentCharacter")]
        private class SaveCurrentCharacter
        {
            static bool Prefix(BodyGenerator __instance)
            {
                try
                {
                    //TODO: Update to reflect SaveTraitChange() -- Does nothing right now but may in the future
                    //var piSBP = typeof(BodyGenerator).GetProperty("SaveTraitChanges", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                   __instance.Character.UpdatePlayerCharacterBodyProperties(__instance.CurrentBodyProperties, __instance.IsFemale);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
                    return false;
                }
            }
        }
    }
}
