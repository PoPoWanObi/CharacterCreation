using CharacterCreation.Common.Settings;
using HarmonyLib;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;

namespace CharacterCreation.Implementation.Patches;

[HarmonyPatch(typeof(FaceGenPropertyVM))]
internal static class FaceGenPropertyVmPatch
{
    [HarmonyPatch(nameof(FaceGenPropertyVM.RefreshValues))]
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    public static void RefreshValuesPostfix(FaceGenPropertyVM __instance)
    {
        if (!DccSettings.Instance!.AddFaceGenValues) return;
        __instance.Name = $"{__instance.Name} ({__instance.Value:F2})";
    }

    [HarmonyPatch(nameof(FaceGenPropertyVM.Value), MethodType.Setter)]
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    public static void SetValuePostfix(FaceGenPropertyVM __instance)
    {
        if (!DccSettings.Instance!.AddFaceGenValues) return;
        __instance.RefreshValues();
    }
}