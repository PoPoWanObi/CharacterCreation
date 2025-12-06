using System.Collections.Generic;
using HarmonyLib;
using SandBox.GauntletUI;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.MountAndBlade.View.Tableaus;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(GauntletBarberScreen), MethodType.Constructor, typeof(BarberState))]
    static class GauntletBarberScreenConstructorPatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions);
            codeMatcher.MatchStartForward(
                CodeMatch.Calls(AccessTools.PropertyGetter(typeof(Hero), nameof(Hero.MainHero))),
                CodeMatch.Calls(AccessTools.PropertyGetter(typeof(Hero), nameof(Hero.CharacterObject))));
            if (codeMatcher.IsValid)
                codeMatcher.RemoveInstructions(2).InsertAndAdvance(CodeInstruction.LoadArgument(1),
                    CodeInstruction.LoadField(typeof(BarberState), nameof(BarberState.Character)));
            return codeMatcher.InstructionEnumeration();
        }
    }
}