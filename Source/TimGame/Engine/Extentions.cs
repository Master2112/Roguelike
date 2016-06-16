using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public static class Extentions
    {
        private static Texture2D pixelTex;

        public static Texture2D WhitePixel
        {
            get
            {
                if (pixelTex == null)
                {
                    pixelTex = new Texture2D(MonoTim.Instance.graphics.GraphicsDevice, 1, 1);
                    pixelTex.SetData(new[] { Color.White });
                }

                return pixelTex;
            }
        }

        public static float ToAngle(this Vector2 vector)
        {
            return MathHelper.ToRadians(((float)Math.Atan2(vector.Y, vector.X) * 180 / (float)Math.PI) + 90);
        }

        public static Vector2 Normalized(this Vector2 vector)
        {
            if (vector != Vector2.Zero)
                vector.Normalize();

            return vector;
        }

        public static void DrawRectangle(this SpriteBatch batch, Rectangle coords, Color color)
        {
            batch.Draw(WhitePixel, coords, color);
        }

        public static void DrawHorizontalBar(this SpriteBatch batch, Rectangle rect, float fillPercentage, Color outterColor, Color innerColor, int borderWidth = 1)
        {
            fillPercentage = Math.Min(Math.Max(0, fillPercentage), 1);

            batch.DrawRectangle(rect, outterColor);
            batch.DrawRectangle(new Rectangle(rect.X + borderWidth, rect.Y + borderWidth, (int)((rect.Width - (borderWidth * 2)) * (fillPercentage)), rect.Height - (borderWidth * 2)), innerColor);
        }
    }
}
