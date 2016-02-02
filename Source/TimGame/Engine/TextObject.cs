using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects
{
    class TextObject : GameObject
    {
        public string Text;
        public float Scale = 1f;

        public TextObject() : base("TextObject", true, Vector2.Zero, "none", new RendererOptions(Color.Black)) { }
        public TextObject(Vector2 position, string text, float scale = 1, RendererOptions options = null) : base("TextObject", true, position, "none", options == null? new RendererOptions(Color.Black) : options)
        {
            Text = text;
            Scale = scale;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            if (Text == null)
                Text = "";

            Vector2 center = (TGame.Instance.MainFont.MeasureString(Text) * 0.5f);

            batch.DrawString(TGame.Instance.MainFont, Text, transform.Position, renderer.BlendColor, transform.Rotation, center, Scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
        }
    }
}
