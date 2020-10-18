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
    [HarmonyPatch(typeof(FaceGenPropertyVM))]
    static class FaceGenPropertyVMPatch
    {
        [HarmonyPatch(nameof(FaceGenPropertyVM.RefreshValues))]
        [HarmonyPostfix]
        public static void RefreshValuesPostfix(FaceGenPropertyVM __instance)
        {
            __instance.Name = $"{__instance.Name}\n{__instance.Value}";
        }

        [HarmonyPatch(nameof(FaceGenPropertyVM.Value), MethodType.Setter)]
        [HarmonyPostfix]
        public static void SetValuePostfix(FaceGenPropertyVM __instance)
        {
            __instance.RefreshValues();
        }
    }
}
