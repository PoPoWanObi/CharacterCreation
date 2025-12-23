using ConditionalAssemblyLoader;

namespace CharacterCreation
{
    public sealed class CharacterCreationAssemblyLoader : AssemblyLoader<CharacterCreationEntryPoint>
    {
        public CharacterCreationAssemblyLoader()
        {
            References.Add(new ConditionalAssemblyReference(() => true, "CharacterCreation.1.7.0.dll"));
        }
        
        protected override void OnAssemblyLoaded(CharacterCreationEntryPoint value)
        {
            
        }
    }
}