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
            Type DynamicBodyPatch = AccessTools.TypeByName("CharacterReload.Patches.DynamicBodyPatch");
            Type SaveCurrentCharacter = AccessTools.Inner(AccessTools.TypeByName("CharacterReload.Pathes.BodyGeneratorPatch"), "SaveCurrentCharacter");

            MethodInfo DoNotExecuteMethodInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethod));
            MethodInfo DoNotExecutePrefixInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefix));

            // ModuleProcessSkinsXmlPatch - disable all
            if (ModuleProcessSkinsXmlPatch == default) ModuleProcessSkinsXmlPatch = AccessTools.TypeByName("CharacterReload.Patch.ModuleProcessSkinsXmlPatch");
            if (ModuleProcessSkinsXmlPatch != default)
            {
                harmony.Patch(AccessTools.Method(ModuleProcessSkinsXmlPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixInfo)); // strange as it is, it actually works.
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.ModuleProcessSkinsXmlPatch.Prefix");
            }

            // EncyclopediaPageChangedHandle
            if (EncyclopediaPageChangedHandle != default)
            {
                harmony.Patch(AccessTools.Method(EncyclopediaPageChangedHandle, "OnEncyclopediaPageChanged"), new HarmonyMethod(DoNotExecuteMethodInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.EncyclopediaPageChangedHandle.OnEncyclopediaPageChanged");
            }

            // HeroBuilderVM - this one's more complicated
            if (HeroBuilderVM != default)
            {
                EncyclopediaPageChangedAction.HeroBuilderVMType = HeroBuilderVM;
                MethodInfo ExecuteEditPrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteEditPrefix));
                harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteEdit"), new HarmonyMethod(ExecuteEditPrefixInfo));
                Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteEdit");
                MethodInfo ExecuteNamePrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteNamePrefix));
                harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteName"), new HarmonyMethod(ExecuteNamePrefixInfo));
                Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteName");
            }

            MethodInfo DoNotExecuteMethodSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethodSilent));
            MethodInfo DoNotExecutePrefixSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefixSilent));

            // CharacterObjectPatch
            if (CharacterObjectPatch == default) CharacterObjectPatch = AccessTools.TypeByName("CharacterReload.Patch.CharacterObjectPath");
            if (CharacterObjectPatch != default)
            {
                harmony.Patch(AccessTools.Method(CharacterObjectPatch, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.CharacterObjectPath.CharacterObjectPatch.Postfix");
            }

            // FaceGenPropertyVMNamePath
            if (FaceGenPropertyVMNamePath == default) FaceGenPropertyVMNamePath = AccessTools.TypeByName("CharacterReload.Patch.FaceGenPropertyVMNamePath");
            if (FaceGenPropertyVMNamePath != default)
            {
                harmony.Patch(AccessTools.Method(FaceGenPropertyVMNamePath, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMNamePath.Prefix");
            }

            // FaceGenPropertyVMValuePath
            if (FaceGenPropertyVMValuePath == default) FaceGenPropertyVMValuePath = AccessTools.TypeByName("CharacterReload.Patch.FaceGenPropertyVMValuePath");
            if (FaceGenPropertyVMValuePath != default)
            {
                harmony.Patch(AccessTools.Method(FaceGenPropertyVMValuePath, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.FaceGenPropertyVMValuePath.Postfix");
            }

            // MyClanLordItemVM
            if (MyClanLordItemVM != default)
            {
                harmony.Patch(AccessTools.Method(MyClanLordItemVM, "OnNamingHeroOver"), null,
                    new HarmonyMethod(AccessTools.Method(typeof(ClanLordItemVMPatch), nameof(ClanLordItemVMPatch.OnNamingHeroOverPostfix))));
                Debug.Print("[CharacterCreation] Patched CharacterReload.VM.MyClanLordItemVM.OnNamingHeroOver");
            }

            // DynamicBodyPatch
            if (DynamicBodyPatch == default) CharacterObjectPatch = AccessTools.TypeByName("CharacterReload.Patch.DynamicBodyPatch");
            if (DynamicBodyPatch != default)
            {
                harmony.Patch(AccessTools.Method(DynamicBodyPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.DynamicBodyPatch");
            }
            
            // SaveCurrentCharacter
            if (SaveCurrentCharacter == default) SaveCurrentCharacter = AccessTools.Inner(AccessTools.TypeByName("CharacterReload.Patch.BodyGeneratorPatch"), "SaveCurrentCharacter");
            if (SaveCurrentCharacter != default)
            {
                harmony.Patch(AccessTools.Method(SaveCurrentCharacter, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Pathes.BodyGeneratorPatch.SaveCurrentCharacter");

            }
        }

        private static bool ExecuteEditPrefix(object __instance, Hero ___selectedHero, Action<Hero> ___editCallback)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.EditHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___editCallback);
            return false;
        }

        private static bool ExecuteNamePrefix(object __instance, Hero ___selectedHero, Action<Hero> ___nameCallback)
        {
            if (DCCSettingsUtil.Instance.DebugMode)
                Debug.Print($"[CharacterCreation] Call intercepted and redirected.\n{new System.Diagnostics.StackTrace().GetFrame(1)}");

            MethodInfo ClosePageInfo = AccessTools.Method(__instance.GetType(), "ClosePage");
            HeroEditorFunctions.RenameHero(___selectedHero, AccessTools.MethodDelegate<Action>(ClosePageInfo, __instance), ___nameCallback);
            return false;
        }
    }
}