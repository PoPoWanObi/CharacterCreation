using System;
using System.Collections.Generic;

namespace CharacterCreation.Util
{
    public struct CommandImplementation
    {
        public Func<List<string>, string> AgeHeroCallback;
        public Func<List<string>, string> AgeCallback;
        public Func<List<string>, string> AspectCallback;
    }
}