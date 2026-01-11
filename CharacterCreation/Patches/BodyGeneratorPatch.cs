using CharacterCreation.Editor;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(BodyGenerator), nameof(BodyGenerator.SaveCurrentCharacter))]
    public static class BodyGeneratorPatch
    {
        static void Postfix(BodyGenerator __instance)
        {
            if (__instance.Character.IsHero && __instance.Character is CharacterObject character)
                CharacterEditorUtil.ApplyCharacterChanges(character, __instance.CurrentBodyProperties, __instance.Race,
                    __instance.IsFemale);
        }
    }
}