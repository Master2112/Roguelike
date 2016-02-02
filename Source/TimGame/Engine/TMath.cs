using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public static class TMath
    {
        public static float Lerp(float from, float to, float percentage)
        {
            return from + ((to - from) * percentage);
        }
    }
}
