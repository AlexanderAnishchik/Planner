using System;

namespace Load.Helpers
{
    public static class MathHelper
    {
        public static double MidpointRound(double f)
        {
            return Math.Round(f, 0, MidpointRounding.AwayFromZero);
        }
    }
}
