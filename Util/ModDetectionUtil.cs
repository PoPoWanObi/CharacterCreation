using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace CharacterCreation
{
    class ModDetectionUtil
    {
        private static readonly List<ModuleInfo> modules = new List<ModuleInfo>();

        public static ReadOnlyCollection<ModuleInfo> Modules
        {
            get
            {
                if (modules.Count == 0) // this should only happen when this property is first called.
                {
                    string[] moduleNames = Utilities.GetModulesNames();
                    foreach (string moduleName in moduleNames)
                    {
                        ModuleInfo m = new ModuleInfo();
                        m.Load(moduleName);
                        modules.Add(m);
                    }
                }

                return modules.AsReadOnly();
            }
        }

        public static bool IsModuleEnabled(string moduleId)
        {
            return Modules.Count(x => x.Id == moduleId) > 0;
        }

        public static ModuleInfo GetModule(string moduleId)
        {
            return Modules.FirstOrDefault(x => x.Id == moduleId);
        }
    }
}
