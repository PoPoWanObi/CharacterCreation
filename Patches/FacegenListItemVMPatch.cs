using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection;

namespace CharacterCreation.Patches
{
    public class FacegenListVMPatch
    {
        [HarmonyPatch(typeof(FacegenListItemVM), MethodType.Constructor, new Type[] { typeof(string), typeof(int), typeof(Action<FacegenListItemVM, bool>) })]
        internal class FacegenListItemVMPatch
        {
            private static void Postfix(FacegenListItemVM __instance)
            {
                try
                {
                    TaleWorlds.Core.FaceGen.ShowDebugValues = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error :\n{ex.Message} \n\n{ex.InnerException?.Message}");
                }
            }
        }
    }
}
