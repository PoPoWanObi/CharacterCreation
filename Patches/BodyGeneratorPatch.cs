using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    public class BodyGeneratorPatch
    {
        // Inherits from CampaignBehaviorBase
        [HarmonyPatch(typeof(BodyGenerator), "SaveCurrentCharacter")]
        private class SaveCurrentCharacter
        {
            static bool Prefix(BodyGenerator __instance)
            {
                try
                {
                    InformationManager.DisplayMessage(new InformationMessage("[Debug] Character saved: "+ __instance.Character.Name, Color.FromUint(4282569842U)));
                    __instance.Character.UpdatePlayerCharacterBodyProperties(__instance.CurrentBodyProperties, __instance.IsFemale);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
