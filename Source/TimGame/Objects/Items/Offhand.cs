using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Objects.Items
{
    public abstract class Offhand : Item
    {
        public Offhand(string spriteName, string name) : base(name, spriteName)
        {

        }
    }
}
