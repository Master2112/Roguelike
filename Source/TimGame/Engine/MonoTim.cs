using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace TimGame.Engine
{
    class MonoTim
    {
        private class CollisionData
        {
            public GameObject objOne;
            public GameObject objTwo;

            public void SendCollisionMessage()
            {
                objOne.Collide(objTwo);
                objTwo.Collide(objOne);
            }
        }

        private SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphics;
        private TimGame.MainGame baseGame;
        private SpriteLoader spriteLoader;

        public static MonoTim Instance { get; private set; }

        private TGame game = new TGame();

        public int GameStep { get; private set; }

        public void Start(TimGame.MainGame baseGame)
        {
            Instance = this;
            this.baseGame = baseGame;

            spriteLoader = new SpriteLoader(baseGame);

            GameStep = 0;

            game.Start();
        }

        public void Update()
        {
            GameStep++;

            Input.UpdateState();

            List<CollisionData> collided = new List<CollisionData>();

            foreach(GameObject toUpdate in GameObject.AllObjects)
            {
                toUpdate.UpdateObject();
            }

            List<GameObject> collisionCheck = GameObject.AllObjects.FindAll(o => !o.IgnoreCollisions && o.Active);

            foreach(GameObject toUpdate in collisionCheck)
            {
                foreach (GameObject potentialCollision in collisionCheck)
                {
                    if (potentialCollision != toUpdate)
                    {
                        if (collided.Find(o => ((o.objOne == toUpdate && o.objTwo == potentialCollision) || (o.objOne == potentialCollision && o.objTwo == toUpdate))) == null)
                        {
                            if (toUpdate.Bounds.Intersects(potentialCollision.Bounds))
                            {
                                CollisionData newCollision = new CollisionData();

                                newCollision.objOne = toUpdate;
                                newCollision.objTwo = potentialCollision;

                                collided.Add(newCollision);
                            }
                        }
                    }
                }
            }

            foreach (CollisionData collision in collided)
                collision.SendCollisionMessage();

            if (GameObject.NewObjects != null)
            {
                GameObject.AllObjects.AddRange(GameObject.NewObjects);
                GameObject.NewObjects.RemoveAll(o => true);
                GameObject.AllObjects.RemoveAll(o => o.destroyed);
            }

            GameObject.SortObjects();

            game.Update();
        }

        public void Draw()
        {
            for(int i = 0; i < GameObject.AllObjects.Count; i++)
            {
                GameObject toUpdate = GameObject.AllObjects[i];

                if(toUpdate.Active)
                    toUpdate.Draw(spriteBatch);
            }
        }

        public void Load(SpriteBatch batch)
        {
            spriteBatch = batch;

            TGame.Instance.MainFont = baseGame.Content.Load<SpriteFont>("FunSized");

            foreach(string toLoad in SpriteLoader.SpriteNames)
            {
                spriteLoader.Load(spriteBatch, toLoad);
            }
        }

        public void Unload()
        {
            
        }

        internal void SetGraphics(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }
    }
}
