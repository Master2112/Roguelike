using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Characters;
using TimGame.Objects.Items.Offhands;
using TimGame.Objects.Items.Potions;

namespace TimGame.Objects.World
{
    class Chest : WorldObject
    {
        public Chest(Vector2 position) : base("chest", true, position, "chest")
        {
            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Impassable);
        }

        public override void Damage(float damage, Character dealer)
        {
            Destroy();

            if (TRandom.Chance(0.1f))
                dealer.Inventory.Items.Add(new HealthPotion());
            else if (TRandom.Chance(0.1f))
                dealer.Inventory.Add(new Lightstone());
            else
                dealer.Inventory.Add(new Poison());

            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Passable);
            base.Damage(damage, dealer);
        }
    }
}
