using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TimGame;
using TimGame.Engine;
using TimGame.Objects;
using NLua;
using TimGame.Objects.World;
using TimGame.Objects.Characters;

namespace TimGame.Scenes
{
    class MainScene : Scene
    {
        public static Player player;
        public bool Dark = false;
        public static int Level = 0;

        public MainScene() : base("Main")
        {

        }

        public override void InitScene()
        {
            Dark = TRandom.Chance(0.2f);

            Builder builder = new Builder();
            CellSpawner.ObliterateWorld();

            TimGame.Objects.World.Builder.RoomTemplate template = builder.Build(100, 0.2f, 4, 7, 0.6f, 5, 0.3f, 0.6f, 2);

            List<GroundTile> tiles = new List<GroundTile>();

            foreach(Vector2 tilePos in template.tiles)
            {
                GroundTile tile = (GroundTile)MakeSceneObject(new GroundTile(PathfindConstants.GridToWorld(tilePos)));
                tiles.Add(tile);
            }

            foreach(GroundTile tile in tiles)
            {
                tile.MakeWall();
            }

            foreach (Vector2 chestPos in template.chests)
            {
                MakeSceneObject(new Chest(PathfindConstants.GridToWorld(chestPos)));
            }

            foreach (Vector2 enemy in template.enemies)
            {
                MakeSceneObject(new Enemy(PathfindConstants.GridToWorld(enemy)));
            }

            Console.WriteLine("Tiles: " + template.tiles.Count);

            if (player == null)
                player = (Player)MakeSceneObject(new Player(new Vector2(0, -20)));
            else
            {
                player.destroyed = false;
                WorldObject.AllObjects.Add(player);
                player.transform.Position = new Vector2(0, -20);
                player = (Player)MakeSceneObject(player);
            }

            MakeSceneObject(new Portal(tiles[tiles.Count - 1].transform.Position));
        }

        public override void Update()
        {
            Renderer.CameraOffset = player.transform.Position - new Vector2(TGame.ScreenWidth / 2, TGame.ScreenHeight / 2);

            base.Update();
        }
    }
}
