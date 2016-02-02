using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TimGame.Engine
{
    public static class PathfindConstants
    {
        public static float GridSize = 32; //Change this to the width and height of the grid
        public static RoomData.Type defaultType = RoomData.Type.Impassable;

        public enum Directions
        {
            North,
            East,
            South,
            West,
            NorthEast,
            NorthWest,
            SouthEast,
            SouthWest,
            None
        }

        public static Vector2 GridToWorld(int x, int y)
        {
            return RoundedVector(x * GridSize, y * GridSize);
        }

        public static Vector2 WorldToGrid(int x, int y)
        {
            return RoundedVector(x / GridSize, y / GridSize);
        }

        public static Vector2 GridToWorld(float x, float y)
        {
            return GridToWorld((int)Math.Round(x), (int)Math.Round(y));
        }

        public static Vector2 GridToWorld(Vector2 gridPosition)
        {
            return GridToWorld(gridPosition.X, gridPosition.Y);
        }

        public static Vector2 WorldToGrid(float x, float y)
        {
            return WorldToGrid((int)Math.Round(x), (int)Math.Round(y));
        }

        public static Vector2 WorldToGrid(Vector2 worldPos)
        {
            return WorldToGrid((int)Math.Round(worldPos.X), (int)Math.Round(worldPos.Y));
        }

        public static Vector2 RoundedVector(float x, float y)
        {
            return new Vector2((int)Math.Round(x), (int)Math.Round(y));
        }
    }
}