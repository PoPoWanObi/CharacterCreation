using CharacterCreation.Util;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Tableaus;

namespace CharacterCreation.Patches
{

    [HarmonyPatch(typeof(BasicCharacterTableau), "RefreshCharacterTableau")]
    static class InitializeAgentVisualsTranspiler
    {
        //public static bool Prepare(MethodBase original)
        //{
        //    if (!DCCSettingsUtil.Instance.PatchSavePreviewGenderBug) return false;
        //    if (original == null) return true;
        //    var info = Harmony.GetPatchInfo(original);
        //    if (info == default || info.Transpilers.Count == 0)
        //    {
        //        Debug.Print("[CharacterCreation] Preparing to patch incorrect save preview stuff");
        //        return true;
        //    }
        //    Debug.Print("[CharacterCreation] Patch to fix misgendered character rendering already exists, skipping");
        //    return false;
        //}

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var list = instructions.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
                if (list[i].Matches(OpCodes.Ldfld, AccessTools.Field(typeof(BasicCharacterTableau), "_faceDirtAmount"))
                    && list[i + 1].Matches(OpCodes.Ldloc_S) && list[i + 1].operand is LocalBuilder lb && lb.LocalIndex == 4)
                {
                    list.RemoveAt(i + 1);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeof(BasicCharacterTableau), "_isFemale"));
                }
            }
        }
    }
}
