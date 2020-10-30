using TaleWorlds.Library;

namespace CharacterCreation.Util
{
    static class CompatibilityPatchUtil
    {
        public static bool DoNotExecuteMethod()
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and stopped.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            return DoNotExecuteMethodSilent();
        }

        public static bool DoNotExecuteMethodSilent()
        {
            return false;
        }

        public static bool DoNotExecutePrefix(ref bool __result)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and skipped.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            return DoNotExecutePrefixSilent(ref __result);
        }

        public static bool DoNotExecutePrefixSilent(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

}