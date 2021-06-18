using CharacterCreation.Models;
using CharacterCreation.Patches;
using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

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

            // ModuleProcessSkinsXmlPatch
            // I'm currently working on a mod similar to Rimworld patch ops, which would deprecate this patch since it would run before the patches anyway
            // until then both do the same thing so should this patch be kept at all?
            if (ModuleProcessSkinsXmlPatch == default) ModuleProcessSkinsXmlPatch = AccessTools.TypeByName("CharacterReload.Patch.ModuleProcessSkinsXmlPatch");
            if (ModuleProcessSkinsXmlPatch != default)
            {
                harmony.Patch(AccessTools.Method(ModuleProcessSkinsXmlPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixInfo)); // strange as it is, it actually works.
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.ModuleProcessSkinsXmlPatch.Prefix");
            }

            //// EncyclopediaPageChangedHandle
            //// Currently being phased out in favor of keeping both sets of buttons (but DCC buttons will be indicated)
            //if (EncyclopediaPageChangedHandle != default)
            //{
            //    harmony.Patch(AccessTools.Method(EncyclopediaPageChangedHandle, "OnEncyclopediaPageChanged"), new HarmonyMethod(DoNotExecuteMethodInfo));
            //    Debug.Print("[CharacterCreation] Disabled CharacterReload.EncyclopediaPageChangedHandle.OnEncyclopediaPageChanged");
            //}

            //// HeroBuilderVM - this one's more complicated
            //// Also being phased out - see above
            //if (HeroBuilderVM != default)
            //{
            //    EncyclopediaPageChangedAction.HeroBuilderVMType = HeroBuilderVM;
            //    MethodInfo ExecuteEditPrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteEditPrefix));
            //    harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteEdit"), new HarmonyMethod(ExecuteEditPrefixInfo));
            //    Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteEdit");
            //    MethodInfo ExecuteNamePrefixInfo = AccessTools.Method(typeof(CharacterReloadPatch), nameof(CharacterReloadPatch.ExecuteNamePrefix));
            //    harmony.Patch(AccessTools.Method(HeroBuilderVM, "ExecuteName"), new HarmonyMethod(ExecuteNamePrefixInfo));
            //    Debug.Print("[CharacterCreation] Redirected CharacterReload.VM.HeroBuilderVM.ExecuteName");
            //}

            MethodInfo DoNotExecuteMethodSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecuteMethodSilent));
            MethodInfo DoNotExecutePrefixSilentInfo = AccessTools.Method(typeof(CompatibilityPatchUtil), nameof(CompatibilityPatchUtil.DoNotExecutePrefixSilent));

            // CharacterObjectPatch
            // Both patches do the same thing so this is strictly speaking not necessary - patch it anyway?
            if (CharacterObjectPatch == default)
                CharacterObjectPatch = AccessTools.Inner(AccessTools.TypeByName("CharacterReload.Patch.CharacterObjectPath"), "CharacterObjectPatch");
            if (CharacterObjectPatch != default)
            {
                harmony.Patch(AccessTools.Method(CharacterObjectPatch, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.CharacterObjectPath.CharacterObjectPatch.Postfix");
            }

            // FaceGenPropertyVMNamePath
            // Not sure if it's necessary, but CR does not quite handle this elegantly in every panel other than the body sliders
            if (FaceGenPropertyVMNamePath == default) FaceGenPropertyVMNamePath = AccessTools.TypeByName("CharacterReload.Patch.FaceGenPropertyVMNamePath");
            if (FaceGenPropertyVMNamePath != default)
            {
                harmony.Patch(AccessTools.Method(FaceGenPropertyVMNamePath, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.FaceGenPropertyVMNamePath.Prefix");
            }

            // FaceGenPropertyVMValuePath
            // See above
            if (FaceGenPropertyVMValuePath == default) FaceGenPropertyVMValuePath = AccessTools.TypeByName("CharacterReload.Patch.FaceGenPropertyVMValuePath");
            if (FaceGenPropertyVMValuePath != default)
            {
                harmony.Patch(AccessTools.Method(FaceGenPropertyVMValuePath, "Postfix"), new HarmonyMethod(DoNotExecuteMethodSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.FaceGenPropertyVMValuePath.Postfix");
            }

            // MyClanLordItemVM
            // CR does not (yet as of writing) handle renaming properly - should patch anyway?
            if (MyClanLordItemVM != default)
            {
                //harmony.Patch(AccessTools.Method(MyClanLordItemVM, "OnNamingHeroOver"), null,
                //    new HarmonyMethod(AccessTools.Method(typeof(ClanLordItemVMPatch), nameof(ClanLordItemVMPatch.OnNamingHeroOverPostfix))));
                harmony.Patch(AccessTools.Method(MyClanLordItemVM, "OnNamingHeroOver"), null, new HarmonyMethod(DoNotExecuteMethodSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.VM.MyClanLordItemVM.OnNamingHeroOver");
            }

            // DynamicBodyPatch
            // Do they quite do the samething?
            // CR for e1.5.6 removed this code entirely so an update for e1.5.6 and a hypothetical future version will probably remove this
            if (DynamicBodyPatch == default) DynamicBodyPatch = AccessTools.TypeByName("CharacterReload.Patch.DynamicBodyPatch");
            if (DynamicBodyPatch != default)
            {
                harmony.Patch(AccessTools.Method(DynamicBodyPatch, "Prefix"), new HarmonyMethod(DoNotExecutePrefixSilentInfo));
                Debug.Print("[CharacterCreation] Disabled CharacterReload.Patch.DynamicBodyPatch");
            }
            
            // SaveCurrentCharacter
            // Is this patch necessary at all?
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