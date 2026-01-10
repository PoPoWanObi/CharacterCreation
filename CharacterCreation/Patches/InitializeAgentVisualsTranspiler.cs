using HarmonyLib;
using System.Collections.Generic;
using CharacterCreation.Settings;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Tableaus;

namespace CharacterCreation.Patches
{

    [HarmonyPatch(typeof(BasicCharacterTableau), "RefreshCharacterTableau")]
    static class InitializeAgentVisualsTranspiler
    {
        static bool Prepare()
        {
            if (DccSettings.Instance!.PatchSavePreviewGenderBug)
            {
                Debug.Print("[CharacterCreation] Will target character save preview and fix it.");
                return true;
            }

            return false;
        }
        
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            // prepare local variable
            // the one to target is flag1 (which is set by an obtuse age check)
            var codeMatcher = new CodeMatcher(instructions);
            codeMatcher.MatchStartForward(
                CodeMatch.LoadsField(AccessTools.Field(typeof(BasicCharacterTableau), "_faceDirtAmount")),
                CodeMatch.LoadsLocal());
            if (codeMatcher.IsValid)
                codeMatcher.Advance().RemoveInstruction().InsertAndAdvance(CodeInstruction.LoadArgument(0),
                    CodeInstruction.LoadField(typeof(BasicCharacterTableau), "_isFemale"));
            return codeMatcher.InstructionEnumeration();

        }
    }
}
