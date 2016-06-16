using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;

namespace TimGame.Objects.World
{
    class Builder
    {
        private List<Vector2> tiles;
        private List<Vector2> chests;
        private List<Vector2> enemies;
        private int steps = 0;

        public Builder()
        {
            tiles = new List<Vector2>();
            chests = new List<Vector2>();
            enemies = new List<Vector2>();
            tiles.Add(new Vector2(0, 0));
            steps = 0;
        }

        public RoomTemplate Build(int pathLength, float turnChance, int minRoomSize, int maxRoomSize, float roomChance, int maxRooms, float chestChancePerRoom, float enemyChancePerRoom, int maxEnemiesPerRoom)
        {
            Directions direction = (Directions)TRandom.Range(0, 3);
            Vector2 lastTile = Vector2.Zero;
            int noRoomSteps = maxRoomSize + 3;

            while(steps <= pathLength)
            {
                if (noRoomSteps <= 0 && steps < pathLength - (maxRoomSize + 3))
                {
                    if(TRandom.Value < roomChance)
                    {
                        int size = TRandom.Range(minRoomSize, maxRoomSize);

                        noRoomSteps = size * 2;

                        Console.WriteLine("new room, size " + size);

                        Vector2 currTile = tiles[tiles.Count - 1];

                        for(int x = 0; x < size; x++)
                        {
                            for(int y = 0; y < size; y++)
                            {
                                Vector2 newPos = currTile + (new Vector2(x, y) - new Vector2(size / 2, size / 2));

                                if (!tiles.Contains(newPos))
                                    tiles.Add(newPos);
                            }
                        }

                        lastTile = currTile;

                        if (TRandom.Value < chestChancePerRoom)
                        {
                            if (!chests.Contains(currTile))
                                chests.Add(currTile);
                        }
                        else
                        {
                            for (int i = 0; i < TRandom.Range(0, maxEnemiesPerRoom); i++)
                            {
                                if (TRandom.Value < enemyChancePerRoom)
                                {
                                    enemies.Add(currTile);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Vector2 newPos = lastTile + DirectionToVector(direction);

                    if (!tiles.Contains(newPos))
                        tiles.Add(newPos);

                    lastTile = newPos;
                    noRoomSteps--;
                }

                if(TRandom.Value < turnChance)
                {
                    int newDir = ((int)direction + (TRandom.Value > 0.5f ? 1 : -1)) % 3;
                    if(newDir < 0)
                        newDir = 3;

                    direction = (Directions)newDir;
                }

                steps++;
            }

            return new RoomTemplate(tiles, chests, enemies, lastTile);
        }

        public Vector2 DirectionToVector(Directions direction)
        {
            switch(direction)
            {
                case Directions.North: return new Vector2(0, 1);
                case Directions.East: return new Vector2(1, 0);
                case Directions.South: return new Vector2(0, -1);
                case Directions.West: return new Vector2(-1, 0);
            }

            return Vector2.Zero;
        }

        public class RoomTemplate
        {
            public List<Vector2> tiles, chests, enemies;
            public Vector2 endPoint;

            public RoomTemplate(List<Vector2> tiles, List<Vector2> chests, List<Vector2> enemies, Vector2 endPoint)
            {
                this.tiles = tiles;
                this.chests = chests;
                this.enemies = enemies;
                this.endPoint = endPoint;
            }
        }

        public enum Directions
        {
            North,
            East,
            South,
            West
        }
    }
}
