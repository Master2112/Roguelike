using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.Items.Potions
{
    public abstract class Potion : Item
    {
        public Potion(string name) : base(name, "bottle_empty")
        {

        }

        public override string DisplayName
        {
            get
            {
                if (IsKnown())
                    return base.DisplayName;
                else
                    return "Unknown Potion";
            }
        }

        public abstract Color GetColor();
        public abstract bool IsKnown();

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch, Microsoft.Xna.Framework.Rectangle rect)
        {
            batch.Draw(SpriteLoader.Instance.GetSprite("bottle_filling"), rect, GetColor());
            base.Draw(batch, rect);
        }
    }
}
