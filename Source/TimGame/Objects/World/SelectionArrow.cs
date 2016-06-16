using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.World
{
    class SelectionArrow : GameObject
    {
        public SelectionArrow(Vector2 position) : base("arrow", true, position, "SelectionArrow")
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
