using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Characters;

namespace TimGame.Objects.Items
{
    public abstract class Item
    {
        public enum Enchantments
        {
            None,
            Fire,
            Occult,
            Electric,
            Blessed
        }

        public string SpriteName = "";
        public string Name;
        public bool Destroy = false;

        public virtual string DisplayName { get { return Name; } }

        public Item(string name, string spriteName)
        {
            Name = name;
            SpriteName = spriteName;
        }

        public virtual void Use(Character user)
        {

        }

        public virtual void Draw(SpriteBatch batch, Rectangle rect)
        {
            batch.Draw(SpriteLoader.Instance.GetSprite(SpriteName), rect, Color.White);
        }
    }
}
