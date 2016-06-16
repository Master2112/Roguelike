using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.World;

namespace TimGame.Objects.Characters
{
    class Enemy: Character
    {
        private List<Vector2> path;
        private bool firstSteps = false;
        private float speed = 1;
        private Vector2 attackDelta = Vector2.Zero;
        private Vector2 playerGridPointTarget = Vector2.Zero;

        public Enemy(Vector2 position) : base(position, "Enemy", (Genders)TRandom.Range(0, 2))
        {
            position -= new Vector2(0, 20);
        }

        public override void Update()
        {
            base.Update();

            moveAxis = Vector2.Zero;

            bool canSeePlayer = Vector2.Distance(transform.Position, Player.Instance.transform.Position) < WorldObject.ViewDistance;

            bool playerPointChanged = false;

            if(Vector2.Distance(playerGridPointTarget, Player.Instance.GridPosition) > 1.5f)
            {
                if (path == null || path.Count <= 0 || Vector2.Distance(transform.Position + collisionCheckOffset, PathfindConstants.GridToWorld(path[0])) < speed * 4)
                    playerPointChanged = true;
            }

            if (playerPointChanged)
                playerGridPointTarget = Player.Instance.GridPosition;

            if(path == null || path.Count < 1 || (canSeePlayer && playerPointChanged))
            {
                if (!canSeePlayer)
                {
                    if (TRandom.Value < 0.003f || !firstSteps)
                    {
                        path = Pathfinder.FindPathToGridPoint(PathfindConstants.WorldToGrid(GridPosition), PathfindConstants.WorldToGrid(GroundTile.AllTiles[TRandom.Range(0, GroundTile.AllTiles.Count)].transform.Position));
                        firstSteps = true;
                    }
                }
                else
                {
                    Vector2 offset = Vector2.Zero;
                    bool changeAttackDelta = TRandom.Chance(0.03f) || attackDelta == Vector2.Zero;
                    
                    if (GridPosition != Player.Instance.GridPosition + attackDelta)
                    {
                        path = Pathfinder.FindPathToGridPoint(GridPosition, Player.Instance.GridPosition + attackDelta);

                        if (path == null || path.Count <= 0)
                            changeAttackDelta = true;
                        
                        firstSteps = true;
                    }
                    else
                    {
                        if (attackDelta.X < 0)
                            direction = AnimationRows.Right;

                        if (attackDelta.X > 0)
                            direction = AnimationRows.Left;

                        if (attackDelta.Y < 0)
                            direction = AnimationRows.Down;

                        if (attackDelta.Y > 0)
                            direction = AnimationRows.Up;

                        Attack();
                    }

                    if (changeAttackDelta)
                    {
                        offset = new Vector2(TRandom.Range(-1f, 1f), TRandom.Range(-1f, 1f));//transform.Position - Player.Instance.transform.Position;

                        if (Math.Abs(offset.X) > Math.Abs(offset.Y))
                        {
                            attackDelta.X = offset.X > 0 ? 1 : -1;
                            attackDelta.Y = 0;
                        }
                        else
                        {
                            attackDelta.Y = offset.Y > 0 ? 1 : -1;
                            attackDelta.X = 0;
                        }
                    }
                }
            }
            else
            {
                if (Vector2.Distance(PathfindConstants.GridToWorld(path[0]), (transform.Position + collisionCheckOffset)) > 40)
                    path = null;
                else
                {
                    moveAxis = (PathfindConstants.GridToWorld(path[0]) - (transform.Position + collisionCheckOffset)).Normalized();

                    if (Vector2.Distance(transform.Position + collisionCheckOffset, PathfindConstants.GridToWorld(path[0])) < speed * 2)
                        path.RemoveAt(0);

                    if (path.Count > 0)
                        moveAxis = (PathfindConstants.GridToWorld(path[0]) - (transform.Position + collisionCheckOffset)).Normalized();
                }
            }
        }
    }
}
