using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;

namespace CharacterCreation.Patches
{

    [HarmonyPatch(typeof(BasicCharacterTableau), "RefreshCharacterTableau")]
    static class InitializeAgentVisualsTranspiler
    {
        private static readonly List<string> incompatibleInstances = new List<string>
        {
            "d225.fixedbanditspawning"
        };

        public static bool Prepare(MethodBase original)
        {
            if (original == null) return true;
            var info = Harmony.GetPatchInfo(original);
            if (info == default || info.Transpilers.Select(x => x.owner).Intersect(incompatibleInstances).IsEmpty())
            {
                Debug.Print("[CharacterCreation] Preparing to patch incorrect save preview stuff");
                return true;
            }
            Debug.Print("[CharacterCreation] Patch to fix misgendered character rendering already exists, skipping");
            return false;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            Debug.Print("[CharacterCreation] Attempting to fix save preview misgendering");
            var code = instructions.ToList();

            // locate call to MBAgentVisuals function and work backwards
            int i;
            for (i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Call && code[i].operand is MethodInfo method
                    && method == AccessTools.Method(typeof(MBAgentVisuals), nameof(MBAgentVisuals.FillEntityWithBodyMeshesWithoutAgentVisuals)))
                    break;
            }

            if (i < code.Count)
            {
                for (; i >= 0; i--)
                {
                    if (code[i].opcode == OpCodes.Ldloc_S && code[i].operand is LocalBuilder lb && lb.LocalIndex == 5)
                    {
                        // replace current instruction
                        code[i] = new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BasicCharacterTableau), "_isFemale"));
                        // insert ahead this
                        code.Insert(i, new CodeInstruction(OpCodes.Ldarg_0)); // instance methods use ldarg_0 as this
                        Debug.Print("[CharacterCreation] Save preview misgendering fixed");
                        break;
                    }
                }
            }

            return code.AsEnumerable();
        }
    }
}
