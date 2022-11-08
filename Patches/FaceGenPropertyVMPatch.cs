using HarmonyLib;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(FaceGenPropertyVM))]
    static class FaceGenPropertyVMPatch
    {
        [HarmonyPatch(nameof(FaceGenPropertyVM.RefreshValues))]
        [HarmonyPostfix]
        public static void RefreshValuesPostfix(FaceGenPropertyVM __instance)
        {
            if (!DCCSettingsUtil.Instance.AddFaceGenValues) return;
            __instance.Name = $"{__instance.Name}\n{__instance.Value}";
        }

        [HarmonyPatch(nameof(FaceGenPropertyVM.Value), MethodType.Setter)]
        [HarmonyPostfix]
        public static void SetValuePostfix(FaceGenPropertyVM __instance)
        {
            if (!DCCSettingsUtil.Instance.AddFaceGenValues) return;
            __instance.RefreshValues();
        }
    }
}
