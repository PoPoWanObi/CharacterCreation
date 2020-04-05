using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using System.Windows.Forms;

using CharacterCreation.Lib;
using HarmonyLib;

namespace CharacterCreation
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "CharacterCreation";

        // Debug console ~~
        [DllImport("Rgl.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "?toggle_imgui_console_visibility@rglCommand_line_manager@@QEAAXXZ")]
        public static extern void toggle_imgui_console_visibility(UIntPtr x);

        // Main
        protected override void OnSubModuleLoad()
        {

            base.OnSubModuleLoad();
            TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Enable developer facegen

            try
            {
                Loader.Initialise(ModuleName);

                var harmony = new Harmony("mod.bannerlord.characterc");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error patching:\n{ex.Message} \n\n{ex.InnerException?.Message}");
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            if (!this._isLoaded)
            {
                InformationManager.DisplayMessage(new InformationMessage("Loaded Detailed Character Creation.", Color.FromUint(4282569842U)));
                this._isLoaded = true;
            }
        }

        private bool _isLoaded;
    }
}

