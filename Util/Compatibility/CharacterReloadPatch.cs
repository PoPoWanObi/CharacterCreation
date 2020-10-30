using CharacterCreation.Models;
using CharacterCreation.Patches;
using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CharacterCreation.Util
{
    class CharacterReloadPatch : CompatibilityPatch
    {
        public CharacterReloadPatch() : base("CharacterReload")
        {

        }

        protected override void OnPatch(ModuleInfo moduleInfo, Harmony harmony)
        {
            Debug.Print("[CharacterCreation] Patching Character Reload for compatibility");
            Type ModuleProcessSkinsXmlPatch = AccessTools.TypeByName("CharacterReload.Pathes.ModuleProcessSkinsXmlPatch");
            Type EncyclopediaPageChangedHandle = AccessTools.TypeByName("CharacterReload.EncyclopediaPageChangedHandle");
            Type CharacterObjectPatch = AccessTools.Inner(AccessTools.TypeByName("CharacterReload.Pathes.CharacterObjectPath"), "CharacterObjectPatch");
            Type HeroBuilderVM = AccessTools.TypeByName("CharacterReload.VM.HeroBuilderVM");
            Type FaceGenPropertyVMNamePath = AccessTools.TypeByName("CharacterReload.Pathes.FaceGenPropertyVMNamePath");
            Type FaceGenPropertyVMValuePath = AccessTools.TypeByName("CharacterReload.Pathes.FaceGenPropertyVMValuePath");
            Type MyClanLordItemVM = AccessTools.TypeByName("CharacterReload.VM.MyClanLordItemVM");

            if (ModuleProcessSkinsXmlPatch == default || EncyclopediaPageChangedHandle == default || CharacterObjectPatch == default || HeroBuilderVM == default
                || FaceGenPropertyVMNamePath == default || FaceGenPropertyVMValuePath == default || MyClanLordItemVM == default)
                return;

            MethodInfo DoNotExecuteMethodInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethod));
            MethodInfo DoNotExecutePrefixInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefix));

            // ModuleProcessSkinsXmlPatch - disable all
            harmony.Patch(AccessTools.Method(ModuleProcessSkinsXmlPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixInfo)); // strange as it is, it actually works.
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.ModuleProcessSkinsXmlPatch.Prefix");

            // EncyclopediaPageChangedHandle
            harmony.Patch(AccessTools.Method(EncyclopediaPageChangedHandle, "OnEncyclopediaPageChanged"), new HarmonyMethod(DoNotExecuteMethodInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.EncyclopediaPageChangedHandle.OnEncyclopediaPageChanged");

            // HeroBuilderVM - this one's more complicated
            EncyclopediaPageChangedAction.HeroBuilderVMType = HeroBuilderVM;
            MethodInfo ExecuteEditPrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteEditPrefix));
            harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteEdit"), new HarmonyMethod(ExecuteEditPrefixInfo));
            Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteEdit");
            MethodInfo ExecuteNamePrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteNamePrefix));
            harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteName"), new HarmonyMethod(ExecuteNamePrefixInfo));
            Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteName");

            MethodInfo DoNotExecuteMethodSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethodSilent));
            MethodInfo DoNotExecutePrefixSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefixSilent));

            // CharacterObjectPatch
            harmony.Patch(AccessTools.Method(CharacterObjectPatch, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.CharacterObjectPath.CharacterObjectPatch.Postfix");

            // FaceGenPropertyVMNamePath
            harmony.Patch(AccessTools.Method(FaceGenPropertyVMNamePath, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMNamePath.Prefix");

            // FaceGenPropertyVMValuePath
            harmony.Patch(AccessTools.Method(FaceGenPropertyVMValuePath, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
            Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMValuePath.Postfix");

            // MyClanLordItemVM
            harmony.Patch(AccessTools.Method(MyClanLordItemVM, "OnNamingHeroOver"), null,
                new HarmonyMethod(AccessTools.Method(typeof(ClanLordItemVMPatch), nameof(ClanLordItemVMPatch.OnNamingHeroOverPostfix))));
            Debug.Print("[CharacterCreation] Patched CharacterReload.VM.MyClanLordItemVM.OnNamingHeroOver");
        }

        private static bool ExecuteEditPrefix(object __instance, Hero ___selectedHero, Action<Hero> ___editCallback)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.EditHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___editCallback);
            return false;
        }

        private static bool ExecuteNamePrefix(object __instance, Hero ___selectedHero, Action<Hero> ___nameCallback)
        {
            if (DCCSettings.Instance != null && DCCSettings.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.RenameHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___nameCallback);
            return false;
        }
    }
}