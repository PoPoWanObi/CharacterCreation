using TaleWorlds.Core;

namespace CharacterCreation.Manager
{
    class CharacterBodymanager
    {
        public static void copyDynamicBodyProperties(DynamicBodyProperties src, DynamicBodyProperties target)
        {
            target.Age = src.Age;
            target.Build = src.Build;
            target.Weight = src.Weight;
           
        }
    }
}
