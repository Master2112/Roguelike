using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public class Renderer
    {
        public static Color BackgroundColor = Color.Black;

        private GameObject owner;
        public Texture2D Texture {get; private set;}
        private string textureName;
        private int cols, rows;
        public float Scale = 1;
        public Color BlendColor = Color.White;
        public static Vector2 CameraOffset = Vector2.Zero;

        public int Frames
        {
            get
            {
                return cols * rows;
            }
        }

        public float TextureWidth
        {
            get
            {
                if (Texture == null)
                    return 0;

                return (Texture.Width / cols) * Scale;
            }
        }

        public float TextureHeight
        {
            get
            {
                if (Texture == null)
                    return 0;

                return (Texture.Height / rows) * Scale;
            }
        }

        private Vector2 origin;

        public int ImageIndex;

        public Renderer(GameObject owner)
        {
            this.owner = owner;
        }

        public void SetTexture(string name, int cols = 1, int rows = 1)
        {
            if (rows < 1)
                rows = 1;

            if (cols < 1)
                cols = 1;

            textureName = name;
            Texture = SpriteLoader.Instance.GetSprite(name);
            this.cols = cols;
            this.rows = rows;

            if(Texture != null)
                origin = new Vector2(Texture.Width * 0.5f, Texture.Height * 0.5f);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (Texture != null)
            {
                Rectangle sourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
                Rectangle destRect = new Rectangle((int)(owner.transform.Position.X - CameraOffset.X), (int)(owner.transform.Position.Y - CameraOffset.Y), Texture.Width, Texture.Height);

                if(cols != 1 || rows != 1)
                {
                    int width = Texture.Width / cols;
                    int height = Texture.Height / rows;

                    while (ImageIndex > Frames)
                        ImageIndex -= Frames;

                    int xPos = width * ImageIndex;
                    int yPos = 0;

                    while(xPos >= Texture.Width)
                    {
                        xPos -= Texture.Width;
                        yPos += height;
                    }

                    sourceRect.X = xPos;
                    sourceRect.Y = yPos;
                    sourceRect.Width = width;
                    sourceRect.Height = height;

                    destRect.Width = width;
                    destRect.Height = height;

                    origin.X = (float)width / 2;
                    origin.Y = (float)height / 2;
                }

                batch.Draw(Texture, owner.transform.Position - CameraOffset, sourceRect, BlendColor, owner.transform.Rotation, origin, Scale, SpriteEffects.None, 0);
                //batch.Draw(Texture, owner.transform.Position, null, Color.White, owner.transform.Rotation, origin, 1, SpriteEffects.None, 0);
            }
            else
            {
                SetTexture(textureName, cols, rows);
            }
        }
    }
}
