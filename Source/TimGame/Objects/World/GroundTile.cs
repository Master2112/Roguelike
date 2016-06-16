using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.World
{
    class GroundTile : WorldObject
    {
        public static List<GroundTile> AllTiles;

        public GroundTile(Vector2 position) : base("floor", true, position, "floor")
        {
            CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Passable);
            DrawDepth = 2;

            if(AllTiles == null)
            {
                AllTiles = new List<GroundTile>();
            }

            AllTiles.Add(this);
            AllTiles.RemoveAll(o => o == null);
        }

        public override void Update()
        {
            base.Update();

            //CellSpawner.DefineCellFromWorldPos(transform.Position, RoomData.Type.Passable);
        }

        public void MakeWall()
        {
            Vector2 position;

            position = transform.Position + new Vector2(32, 0);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(-32, 0);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(0, 32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(0, -32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(32, 32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(32, -32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(-32, 32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));

            position = transform.Position + new Vector2(-32, -32);
            if (CellSpawner.GetRoomAtGridPosition(PathfindConstants.WorldToGrid(position)) == null)
                baseScene.MakeSceneObject(new Wall(position));
        }
    }
}
