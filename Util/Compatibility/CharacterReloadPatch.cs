using CharacterCreation.Patches;
using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation.Util
{
    // dummy that exists to prevent something from happening
    public sealed class CharacterReloadPatch : CompatibilityPatch
    {
        public CharacterReloadPatch() : base("CharacterReload")
        {
        }

        protected override void OnPatch(ModuleInfo moduleInfo, Harmony harmony)
        {
        }
    }
}