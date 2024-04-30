using System.Collections.Generic;
using TaleWorlds.Engine;

namespace CharacterCreation.Util
{
    public static class AspectRatioUtil
    {
        public enum MarginType
        {
            Left = 0,
            Top = 1,
            Bottom = 2,
            Right = 3
        }

        private static readonly Dictionary<(float, float), (int, int, int, int)> aspectRatioMarginMap = new Dictionary<(float, float), (int, int, int, int)>
        {
            // (RangeMin, RangeMax) (MarginTop, MarginBottom, MarginRight)
            {(0.95f, 1.05f), (0, 0, 0, 0)},     // Approximately 1:1 {Unorthodox aspect}
            {(1.1f, 1.27f), (0, 390, 0, 350)},  // Approximately 5:4
            {(1.28f, 1.35f), (0, 340, 0, 360)}, // Approximately 4:3
            {(1.58f, 1.65f), (0, 210, 0, 370)}, // Approximately 8:5 / 16:10 //verify
            {(1.55f, 1.57f), (0, 220, 0, 370)}, // Approximately 25:16
            {(1.66f, 1.9f), (0, 155, 0, 370)},  // Approximately 16:9
            {(1.91f, 2.5f), (0, 0, 0, 0)},      // Approximately 2.35:1 {Unorthodox aspect range}
            {(2.51f, 2.6f), (0, 0, 0, 0)},      // Approximately 21:9 // Don't have a 21:9 setup for testing

            //{(1.59f, 1.61f), (0, 210, 0, 360)},  // Approximately 8:5
        };

        public static int GetMarginForAspectRatio(int type)
        {
            float aspectRatio = GetAspectRatio();
            foreach (var entry in aspectRatioMarginMap)
            {
                var (minRatio, maxRatio) = entry.Key;
                if (aspectRatio >= minRatio && aspectRatio <= maxRatio)
                {
                    // Determine which margin type to return based on the 'type' parameter
                    switch (type)
                    {
                        case 0: // Left margin
                            return entry.Value.Item1;
                        case 1: // Top margin
                            return entry.Value.Item2;
                        case 2: // Bottom margin
                            return entry.Value.Item3;
                        case 3: // Right margin
                            return entry.Value.Item4;
                        default: // Default case
                            return 0;
                    }
                }
            }
            return 0; // Default value for non-defined aspect ratios
        }
        public static float GetAspectRatio()
        {
            return Screen.AspectRatio;
        }
    }
}
