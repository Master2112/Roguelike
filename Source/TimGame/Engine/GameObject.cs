using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public abstract class GameObject
    {
        public class RendererOptions
        {
            public int cols, rows, index;
            public Color color;

            public RendererOptions(Color color, int cols = 1, int rows = 1, int startIndex = 0)
            {
                this.color = color;
                this.cols = cols;
                this.rows = rows;
                index = startIndex;
            }
        }

        private static List<GameObject> allObjects = new List<GameObject>();
        public Scene baseScene;

        public static List<GameObject> AllObjects
        {
            get
            {
                if (allObjects == null)
                    allObjects = new List<GameObject>();

                return allObjects;
            }

            set
            {
                allObjects = value;
            }
        }

        public static List<GameObject> NewObjects { get; private set; }

        public bool Active = true;

        public Transform transform;
        public Renderer renderer;

        private int drawDepth = 0;

        public int DrawDepth
        {
            get
            {
                return drawDepth;
            }

            set
            {
                drawDepth = value;
            }
        }

        public static GameObject Find(string name)
        {
            return AllObjects.Find(o => o.Name == name && !o.destroyed);
        }

        public static List<GameObject> FindAll(string name)
        {
            return AllObjects.FindAll(o => o.Name == name && !o.destroyed);
        }

        public static GameObject FindNearest(string name, Vector2 pos)
        {
            List<GameObject> ranged = AllObjects.OrderBy(o => Vector2.Distance(o.transform.Position, pos)).ToList();
            return ranged.Find(o => o.Name == name && !o.destroyed);
        }

        public static List<GameObject> FindAllOrderedByDistance(string name, Vector2 pos)
        {
            List<GameObject> ranged = AllObjects.OrderBy(o => Vector2.Distance(o.transform.Position, pos)).ToList();
            return ranged.FindAll(o => o.Name == name && !o.destroyed);
        }

        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)transform.Position.X - (int)(renderer.TextureWidth / 2), (int)transform.Position.Y - (int)(renderer.TextureHeight / 2), (int)renderer.TextureWidth, (int)renderer.TextureHeight);
            }
        }

        public string Name;
        public GameObject Parent = null;

        private string spriteName;
        public bool IgnoreCollisions;

        public bool destroyed = true;

        public GameObject(string name, bool ignoreCollisions, Vector2 position, string spriteName, RendererOptions rendererOptions = null)
        {
            transform = new Transform(this);
            renderer = new Renderer(this);

            int cols = 1;
            int rows = 1;
            int index = 0;

            if (rendererOptions != null)
            {
                renderer.BlendColor = rendererOptions.color;
                cols = rendererOptions.cols;
                rows = rendererOptions.rows;
                index = rendererOptions.index;
            }

            renderer.SetTexture(spriteName, cols, rows);
            renderer.ImageIndex = index;

            this.IgnoreCollisions = ignoreCollisions;

            transform.Position = position;

            if (AllObjects == null)
                AllObjects = new List<GameObject>();

            if (NewObjects == null)
                NewObjects = new List<GameObject>();

            NewObjects.Add(this);

            this.spriteName = spriteName;
            this.Name = name;
        }

        public void Collide(GameObject other)
        {
            OnCollision(other);
        }

        public void Destroy()
        {
            destroyed = true;
            OnDestroy();

            List<GameObject> children = GameObject.AllObjects.FindAll(o => o.Parent == this);

            foreach (GameObject child in children)
                child.Destroy();
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (Active)
            {
                renderer.Draw(batch);
                
                /*
                if(debugBoundsTestTexture != null)
                {
                    Vector2[] positions = new Vector2[4]
                    {
                        new Vector2(Bounds.X, Bounds.Y),
                        new Vector2(Bounds.X + Bounds.Width, Bounds.Y),
                        new Vector2(Bounds.X, Bounds.Y + Bounds.Height),
                        new Vector2(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height)
                    };

                    for (int i = 0; i < 4; i++)
                    {
                        batch.Draw(SpriteLoader.Instance.GetSprite("tinyBall.png"), positions[i] + new Vector2(-debugBoundsTestTexture.Width / 2, -debugBoundsTestTexture.Height / 2), Color.White);
                    }
                }*/
            }
        }

        public void UpdateObject()
        {
            if (!destroyed && Active)
            {
                transform.UpdateLocalPos();

                Update();
            }
        }

        public static void SortObjects()
        {
            AllObjects = AllObjects.OrderByDescending(o => o.DrawDepth).ToList();
        }

        public virtual void Update() { }
        public virtual void OnCollision(GameObject other) { }
        protected virtual void OnDestroy() { }
    }
}
