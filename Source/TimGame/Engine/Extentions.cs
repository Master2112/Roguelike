using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public static class Extentions
    {
        public static Vector2 Normalized(this Vector2 vector)
        {
            if(vector != Vector2.Zero)
                vector.Normalize();

            return vector;
        }
    }
}
