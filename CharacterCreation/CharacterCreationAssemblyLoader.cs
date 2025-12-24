using System.IO;
using ConditionalAssemblyLoader;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;

namespace CharacterCreation
{
    public sealed class CharacterCreationAssemblyLoader : AssemblyLoader<CharacterCreationEntryPoint>
    {
        public CharacterCreationAssemblyLoader()
        {

            var binaryPath = Path.Combine(Path.GetFullPath(ModuleHelper.GetModuleFullPath("zzCharacterCreation")),
                "bin", "Win64_Shipping_Client");
            References.Add(new ConditionalAssemblyReference(() => true, "CharacterCreation.1.7.0",
                Path.Combine(binaryPath, "CharacterCreation.1.7.0.dll")));
            Out = str => Debug.Print(str);
            Error = str => Debug.Print(str);
        }
        
        protected override void OnAssemblyLoaded(CharacterCreationEntryPoint value)
        {
            
        }
    }
}