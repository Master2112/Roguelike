using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    static class TRandom
    {
        private static System.Random rand = new System.Random();

        public static float Value
        {
            get
            {
                return (float)rand.NextDouble();
            }
        }

        public static float Range(float min, float max)
        {
            return min + (Value * (max - min));
        }

        public static int Range(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
