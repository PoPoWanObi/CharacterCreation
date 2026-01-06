using HarmonyLib;
using System;
using System.Collections.Generic;
using CharacterCreation.Util;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Compatibility
{
    public abstract class CompatibilityPatch
    {
        private static readonly Dictionary<string, CompatibilityPatch> PatchList = new Dictionary<string, CompatibilityPatch>();

        public static IReadOnlyDictionary<string, CompatibilityPatch> Patches => PatchList;

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

        public static void PatchAll(Harmony harmony)
        {
            foreach (var patch in PatchList.Values)
                patch.Patch(harmony);
        }

        public static T AddPatch<T>() where T : CompatibilityPatch, new()
            => AddPatch(new T());

        public static T AddPatch<T>(T instance) where T : CompatibilityPatch
        {
            PatchList.Add(instance.ModuleId, instance);
            return instance;
        }
    }
}
