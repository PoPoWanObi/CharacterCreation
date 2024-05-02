using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace CharacterCreation.Util
{
    public static class MarginUtil
    {
        // I think this is probably a better approach than the margin
        // library, however, I'm sure someone with more experience in
        // this field could improve this. It's accurate enough to 
        // work, though ~ PoPo

        public static int GetTopMarginForAspectRatio()
        {
            // This isn't entirely accurate, but it's very close -- should suffice
            double a = 686.6034216162005;
            double b = -2.6609724694782133;

            return (int)(a * Math.Pow(Screen.AspectRatio, b) + 10);
        }

        public static int GetRightMarginForAspectRatio()
        {
            double a = 80;
            double b = 262.5;

            return (int)(a * Screen.AspectRatio + b ) - 25;
        }
    }
}
