using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.Items.Potions
{
    class Poison : Potion
    {
        private static Color color;
        private static bool known = false;

        public Poison() : base("Poison")
        {
            color = new Color(new Vector3(TRandom.Value, TRandom.Value, TRandom.Value));
        }

        public override Color GetColor()
        {
            return color;
        }

        public override bool IsKnown()
        {
            return known;
        }

        public override void Use(Characters.Character user)
        {
            known = true;
            user.Health -= 10;
            Destroy = true;
        }
    }
}
