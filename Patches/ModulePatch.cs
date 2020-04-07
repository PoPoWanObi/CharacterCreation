using CharacterCreation.Lib;
using HarmonyLib;
using System;
using System.Windows.Forms;

namespace CharacterCreation.Patches
{
    [HarmonyPatch(typeof(TaleWorlds.MountAndBlade.Module), "OnApplicationTick")]
    public class ModulePatch
    {
        static void Finalizer(Exception __exception)
        {
            if (__exception != null)
                MessageBox.Show($"What happened here: \n\n{__exception.ToStringFull()}", "Crash Report");
        }

        static bool Prepare()
        {
            return Settings.Instance.DebugMode;
        }
    }
}
