using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Characters;

namespace TimGame.Objects.World
{
    public class WorldObject : GameObject
    {
        public static List<WorldObject> AllObjects 
        { 
            get
            {
                if (allObjects == null)
                    return null;

                allObjects.RemoveAll(o => o == null || o.destroyed);
                return allObjects;
            }

            private set
            {
                allObjects = value;
            }
        }

        private static List<WorldObject> allObjects;

        private float maxSight { get { return ViewDistance / 2; } }
        private float fallofSight { get { return ViewDistance / 3; } }

        public static float ViewDistance = 400;

        public WorldObject(string name, bool ignoreCollisions, Vector2 position, string spriteName, RendererOptions renderOptions = null) : base(name, ignoreCollisions, position, spriteName, renderOptions)
        {
            if (AllObjects == null)
                AllObjects = new List<WorldObject>();

            AllObjects.Add(this);
        }

        public virtual void Damage(float damage, Character dealer)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            float visibility = 1 - (float)Math.Max(0, (Vector2.Distance(transform.Position, Renderer.CameraOffset + new Vector2(TGame.ScreenWidth / 2, TGame.ScreenHeight / 2)) - maxSight) / (fallofSight));

            if (visibility < 0)
                visibility = 0;
            else if (visibility > 1)
                visibility = 1;

            //renderer.BlendColor.A = Convert.ToByte(visibility);

            renderer.BlendColor = new Color(renderer.BlendColor, visibility);

            base.Draw(batch);
        }
    }
}
