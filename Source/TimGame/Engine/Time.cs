using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public static class Time
    {
        public static float DeltaTime
        {
            get
            {
                return 1f / 60f;
            }
        }
    }
}
