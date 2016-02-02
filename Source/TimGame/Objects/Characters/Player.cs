using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.Characters
{
    class Player: GameObject
    {
        private static float walkSpeed = 2;
        public static float Health = 100;

        public Player(Vector2 position) : base("Player", true, position, "ball")
        {
            DrawDepth = -1;
        }

        public override void Update()
        {
            base.Update();
            GetInput();
        }

        public void GetInput()
        {
            Vector2 moveAxis = Vector2.Zero;

            if(Input.LeftHeld)
            {
                moveAxis += new Vector2(-1, 0);
            }

            if (Input.RightHeld)
            {
                moveAxis += new Vector2(1, 0);
            }

            if (Input.DownHeld)
            {
                moveAxis += new Vector2(0, 1);
            }

            if (Input.UpHeld)
            {
                moveAxis += new Vector2(0, -1);
            }

            moveAxis = moveAxis.Normalized(); //Normalize() doesn't check for zero-length first.

            RoomData roomX = CellSpawner.GetRoomAtWorldPosition(transform.Position + new Vector2((float)Math.Round(moveAxis.X) * 16, -10));

            RoomData roomX2 = CellSpawner.GetRoomAtWorldPosition(transform.Position + new Vector2((float)Math.Round(moveAxis.X) * 16, 10));

            if (roomX != null && roomX.type == RoomData.Type.Passable && roomX2 != null && roomX2.type == RoomData.Type.Passable)
            {
                transform.Position += new Vector2(moveAxis.X * walkSpeed, 0);
            }

            RoomData roomY = CellSpawner.GetRoomAtWorldPosition(transform.Position + new Vector2(10, (float)Math.Round(moveAxis.Y) * 16));

            RoomData roomY2 = CellSpawner.GetRoomAtWorldPosition(transform.Position + new Vector2(-10, (float)Math.Round(moveAxis.Y) * 16));

            if (roomY != null && roomY.type == RoomData.Type.Passable && roomY2 != null && roomY2.type == RoomData.Type.Passable)
            {
                transform.Position += new Vector2(0, moveAxis.Y * walkSpeed);
            }
        }
    }
}
