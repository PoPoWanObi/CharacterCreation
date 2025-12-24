using System.Collections.Generic;
using TaleWorlds.Library;

namespace CharacterCreation.Util
{
    public static class Commands
    {
        [CommandLineFunctionality.CommandLineArgumentFunction("age_hero", "dcc")]
        public static string AgeHero(List<string> strings) =>
            SubModule.Instance!.CommandImplementation.AgeHeroCallback(strings);

        [CommandLineFunctionality.CommandLineArgumentFunction("age", "dcc")]
        public static string Age(List<string> strings) =>
            SubModule.Instance!.CommandImplementation.AgeCallback(strings);

        [CommandLineFunctionality.CommandLineArgumentFunction("aspect", "dcc")]
        public static string Aspect(List<string> strings) =>
            SubModule.Instance!.CommandImplementation.AspectCallback(strings);
    }
}
