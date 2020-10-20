using CharacterCreation.Models;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CharacterCreation
{
    class CompatibilityPatch
    {
        private Action<ModuleInfo, Harmony> action;

        public string ModuleId { get; }

        internal CompatibilityPatch(string moduleId, Action<ModuleInfo, Harmony> action)
        {
            ModuleId = moduleId;
            this.action = action;
        }

        public void Patch(Harmony harmony)
        {
            try
            {
                if (ModDetectionUtil.IsModuleEnabled(ModuleId))
                {
                    action?.Invoke(ModDetectionUtil.GetModule(ModuleId), harmony);
                }
            }
            catch (Exception e)
            {
                Debug.Print($"[CharacterCreation] Failed to create a compatibility patch for {ModuleId}.\n{e}");
            }
        }

        public static void CreateCompatibilityPatches(Harmony harmony)
        {
            new CompatibilityPatch("CharacterReload", CharacterReloadPatches.PatchCharacterReload).Patch(harmony);
        }
    }

    static class CompatibilityPatchUtil
    {
        public static bool DoNotExecuteMethod()
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and stopped.\n{new System.Diagnostics.StackTrace()}");

            return DoNotExecuteMethodSilent();
        }

        public static bool DoNotExecuteMethodSilent()
        {
            return false;
        }

        public static bool DoNotExecutePrefix(ref bool __result)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and skipped.\n{new System.Diagnostics.StackTrace()}");

            return DoNotExecutePrefixSilent(ref __result);
        }

        public static bool DoNotExecutePrefixSilent(ref bool __result)
        {
            __result = true;
            return false;
        }
    }

    static class CharacterReloadPatches
    {
        public static void PatchCharacterReload(ModuleInfo moduleInfo, Harmony harmony)
        {
            Debug.Print("[CharacterCreation] Patching Character Reload for compatibility");
            Type ModuleProcessSkinsXmlPatch = AccessTools.TypeByName("CharacterReload.Pathes.ModuleProcessSkinsXmlPatch");
            Type EncyclopediaPageChangedHandle = AccessTools.TypeByName("CharacterReload.EncyclopediaPageChangedHandle");
            Type CharacterObjectPatch = AccessTools.Inner(AccessTools.TypeByName("CharacterReload.Pathes.CharacterObjectPath"), "CharacterObjectPatch");
            Type HeroBuilderVM = AccessTools.TypeByName("CharacterReload.VM.HeroBuilderVM");
            Type FaceGenPropertyVMNamePath = AccessTools.TypeByName("CharacterReload.Pathes.FaceGenPropertyVMNamePath");
            Type FaceGenPropertyVMValuePath = AccessTools.TypeByName("CharacterReload.Pathes.FaceGenPropertyVMValuePath");

            if (ModuleProcessSkinsXmlPatch == default || EncyclopediaPageChangedHandle == default || CharacterObjectPatch == default || HeroBuilderVM == default)
                return;

            MethodInfo DoNotExecuteMethodInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethod));
            MethodInfo DoNotExecutePrefixInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefix));

            // ModuleProcessSkinsXmlPatch - disable all
            harmony.Patch(AccessTools.Method(ModuleProcessSkinsXmlPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixInfo)); // strange as it is, it actually works.
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.ModuleProcessSkinsXmlPatch.Prefix");

            // EncyclopediaPageChangedHandle
            harmony.Patch(AccessTools.Method(EncyclopediaPageChangedHandle, "OnEncyclopediaPageChanged"), new HarmonyMethod(DoNotExecuteMethodInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.EncyclopediaPageChangedHandle.OnEncyclopediaPageChanged");

            // CharacterObjectPatch
            harmony.Patch(AccessTools.Method(CharacterObjectPatch, "Postfix"), new HarmonyMethod(DoNotExecuteMethodInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.CharacterObjectPath.CharacterObjectPatch.Postfix");

            // HeroBuilderVM - this one's more complicated
            EncyclopediaPageChangedAction.HeroBuilderVMType = HeroBuilderVM;
            MethodInfo ExecuteEditPrefixInfo = AccessTools.Method(typeof(CharacterReloadPatches), nameof(CharacterReloadPatches.ExecuteEditPrefix));
            harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteEdit"), new HarmonyMethod(ExecuteEditPrefixInfo));
            Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteEdit");
            MethodInfo ExecuteNamePrefixInfo = AccessTools.Method(typeof(CharacterReloadPatches), nameof(CharacterReloadPatches.ExecuteNamePrefix));
            harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteName"), new HarmonyMethod(ExecuteNamePrefixInfo));
            Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteName");

            MethodInfo DoNotExecuteMethodSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethodSilent));
            MethodInfo DoNotExecutePrefixSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefixSilent));

            // FaceGenPropertyVMNamePath
            harmony.Patch(AccessTools.Method(FaceGenPropertyVMNamePath, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMNamePath.Prefix");

            // FaceGenPropertyVMValuePath
            harmony.Patch(AccessTools.Method(FaceGenPropertyVMValuePath, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMValuePath.Postfix");
        }

        private static bool ExecuteEditPrefix(object __instance, Hero ___selectedHero, Action<Hero> ___editCallback)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace()}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.EditHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___editCallback);
            return false;
        }

        private static bool ExecuteNamePrefix(object __instance, Hero ___selectedHero, Action<Hero> ___nameCallback)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace()}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.RenameHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___nameCallback);
            return false;
        }
    }
}
