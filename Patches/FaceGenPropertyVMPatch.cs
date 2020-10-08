using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(FaceGenPropertyVM), nameof(FaceGenPropertyVM.Name), MethodType.Getter)]
    static class FaceGenPropertyVMPatch
    {
        public static void Postfix(ref string __result, FaceGenPropertyVM __instance)
        {
            __result = $"{__result} {__instance.Value}";
        }
    }
}
