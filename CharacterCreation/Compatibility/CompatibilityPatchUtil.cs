using CharacterCreation.Settings;
using TaleWorlds.Library;

namespace CharacterCreation.Compatibility
{
    static class CompatibilityPatchUtil
    {
        public static bool DoNotExecuteMethod()
        {
            if (DccSettings.Instance!.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and stopped.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            return DoNotExecuteMethodSilent();
        }

        public static bool DoNotExecuteMethodSilent()
        {
            return false;
        }

        // ReSharper disable once InconsistentNaming
        public static bool DoNotExecutePrefix(ref bool __result)
        {
            if (DccSettings.Instance!.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and skipped.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            return DoNotExecutePrefixSilent(ref __result);
        }

        // ReSharper disable once InconsistentNaming
        public static bool DoNotExecutePrefixSilent(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

}