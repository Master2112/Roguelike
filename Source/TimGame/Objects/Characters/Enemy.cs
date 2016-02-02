using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.World;

namespace TimGame.Objects.Characters
{
    class Enemy: GameObject
    {
        private List<Vector2> path;
        private bool firstSteps = false;
        private float speed = 1;

        public Enemy(Vector2 position) : base("dog", true, position, "dog")
        {
            Console.WriteLine("Woof! " + transform.Position.ToString());
        }

        public override void Update()
        {
            base.Update();

            if(path == null || path.Count < 1)
            {
                if (TRandom.Value < 0.003f || !firstSteps)
                {
                    path = Pathfinder.FindPathToGridPoint(PathfindConstants.WorldToGrid(transform.Position), PathfindConstants.WorldToGrid(GroundTile.AllTiles[TRandom.Range(0, GroundTile.AllTiles.Count)].transform.Position));
                    firstSteps = true;
                }
            }
            else
            {
                transform.Position += (PathfindConstants.GridToWorld(path[0]) - transform.Position).Normalized();
                if (Vector2.Distance(transform.Position, PathfindConstants.GridToWorld(path[0])) < speed * 2)
                    path.RemoveAt(0);
            }
        }
    }
}
