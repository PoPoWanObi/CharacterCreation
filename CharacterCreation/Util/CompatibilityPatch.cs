using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Util
{
    public abstract class CompatibilityPatch
    {
        private static readonly Dictionary<string, CompatibilityPatch> _patches = new Dictionary<string, CompatibilityPatch>();

        public static IReadOnlyDictionary<string, CompatibilityPatch> Patches => _patches;

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
                {
                    var instance = new CharacterReloadPatch();
                    _patches.Add(instance.ModuleId, instance);
                }
            }
            foreach (var patch in _patches.Values)
                patch.Patch(harmony);
        }

        public static T AddPatch<T>() where T : CompatibilityPatch, new()
            => AddPatch(new T());

        public static T AddPatch<T>(T instance) where T : CompatibilityPatch
        {
            _patches.Add(instance.ModuleId, instance);
            return instance;
        }
    }
}
