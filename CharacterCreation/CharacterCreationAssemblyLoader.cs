using System.IO;
using CharacterCreation.Util;
using ConditionalAssemblyLoader;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation
{
    public sealed class CharacterCreationAssemblyLoader : AssemblyLoader<CharacterCreationEntryPoint>
    {
        public CharacterCreationAssemblyLoader()
        {
            var nativeModule = ModDetectionUtil.GetModule("Native");
            var nativeVersion = nativeModule!.Version;
            
            var binaryPath = Path.Combine(Path.GetFullPath(ModuleHelper.GetModuleFullPath("zzCharacterCreation")),
                "bin", "Win64_Shipping_Client");
            References.AddRange(new []
            {
                new ConditionalAssemblyReference(
                    () => nativeVersion is { Major: 1, Minor: 3 } && nativeVersion.Revision >= 13,
                    "CharacterCreation.1.7.1", Path.Combine(binaryPath, "CharacterCreation.1.7.1.dll")),
                new ConditionalAssemblyReference(() => true, "CharacterCreation.1.7.0",
                    Path.Combine(binaryPath, "CharacterCreation.1.7.0.dll"))
            });
            Out = str => Debug.Print(str);
            Error = str => Debug.Print(str);
        }
        
        protected override void OnAssemblyLoaded(CharacterCreationEntryPoint value)
        {
            
        }
    }
}