using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.World
{
    class Wall : GameObject
    {
        float collToggleTime = 0.5f;

        public Wall(Vector2 position) : base("wall", true, position, "wall")
        {
            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Impassable);
        }

        public override void Update()
        {
            base.Update();

            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Impassable);

            if (collToggleTime <= 0)
                IgnoreCollisions = true;
            else
                collToggleTime -= Time.DeltaTime;
        }
    }
}
