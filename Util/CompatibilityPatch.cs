using HarmonyLib;
using System;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Util
{
    abstract class CompatibilityPatch
    {
        public string ModuleId { get; }

        protected CompatibilityPatch(string moduleId)
        {
            ModuleId = moduleId;
        }

        public void Patch(Harmony harmony)
        {
            try
            {
                if (ModDetectionUtil.IsModuleEnabled(ModuleId))
                {
                    OnPatch(ModDetectionUtil.GetModule(ModuleId), harmony);
                }
            }
            catch (Exception e)
            {
                Debug.Print($"[CharacterCreation] Failed to create a compatibility patch for {ModuleId}.\n{e}");
            }
        }

        protected abstract void OnPatch(ModuleInfo moduleInfo, Harmony harmony);

        public static void CreateCompatibilityPatches(Harmony harmony)
        {
            if (DCCSettingsUtil.Instance.EnableCompatibility)
            {
                if (DCCSettingsUtil.Instance.EnableCharacterReloadCompatibility)
                    new CharacterReloadPatch().Patch(harmony);
            }
        }
    }
}
