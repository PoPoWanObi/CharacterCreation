using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Util
{
    public static class ModDetectionUtil
    {
        private static readonly List<ModuleInfo> ModuleList = new List<ModuleInfo>();

        public static ReadOnlyCollection<ModuleInfo> Modules
        {
            get
            {
                if (ModuleList.Count == 0) // this should only happen when this property is first called.
                {
                    string[] moduleNames = Utilities.GetModulesNames();
                    foreach (string moduleName in moduleNames)
                    {
                        ModuleInfo m = new ModuleInfo();
                        m.LoadWithFullPath(ModuleHelper.GetModuleFullPath(moduleName));
                        ModuleList.Add(m);
                    }
                }

                return ModuleList.AsReadOnly();
            }
        }

        public static bool IsModuleEnabled(string moduleId)
        {
            return Modules.Count(x => x.Id == moduleId) > 0;
        }

        public static ModuleInfo? GetModule(string moduleId)
        {
            return Modules.FirstOrDefault(x => x.Id == moduleId);
        }
    }
}
