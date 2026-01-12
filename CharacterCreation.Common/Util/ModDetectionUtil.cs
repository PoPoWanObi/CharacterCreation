using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Common.Util;

public static class ModDetectionUtil
{
    private static readonly List<ModuleInfo> ModuleList = new List<ModuleInfo>();

    private static readonly ReadOnlyCollection<ModuleInfo> ReadOnlyList = ModuleList.AsReadOnly();

    public static ReadOnlyCollection<ModuleInfo> Modules
    {
        get
        {
            if (ModuleList.Count == 0) // this should only happen when this property is first called.
            {
                var moduleNames = Utilities.GetModulesNames();
                foreach (var moduleName in moduleNames)
                {
                    var m = new ModuleInfo();
                    m.LoadWithFullPath(ModuleHelper.GetModuleFullPath(moduleName));
                    ModuleList.Add(m);
                }
            }

            return ReadOnlyList;
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