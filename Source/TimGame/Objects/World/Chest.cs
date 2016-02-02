using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.World
{
    class Chest : GameObject
    {
        public Chest(Vector2 position) : base("chest", true, position, "chest")
        {
            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Impassable);
            
        }

        public override void Update()
        {
            base.Update();

            //CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Impassable);
        }
    }
}
