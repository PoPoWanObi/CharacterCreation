using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.PlatformService;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.DotNet;
using TaleWorlds.Diamond.ClientApplication;
using System.Windows.Forms;

using CharacterCreation.Lib;
using HarmonyLib;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace CharacterCreation
{
    public class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleName = "zzCharacterCreation";
        
        // Main
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                Loader.Initialise(ModuleName);

                var harmony = new Harmony("mod.bannerlord.characterc");
                harmony.PatchAll();
                TaleWorlds.Core.FaceGen.ShowDebugValues = true; // Enable developer facegen
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
        
        // Load our XML files
        private void LoadXMLFiles(CampaignGameStarter gameInitializer)
        {
            // If we require loading texts
        }

        // Called when loading save game
        public override void OnGameLoaded(Game game, object initializerObject)
        {
            CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
            this.LoadXMLFiles(gameInitializer); // We should call this
        }

        // Called when starting new campaign
        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            CampaignGameStarter gameInitializer = (CampaignGameStarter)initializerObject;
            this.LoadXMLFiles(gameInitializer); // We should call this
        }
        
        protected override void OnApplicationTick(float dt)
        {
            // Do thing
        }

        private bool _isLoaded;
    }
}