using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Characters;
using TimGame.Scenes;

namespace TimGame.Objects.World
{
    class Portal : WorldObject
    {
        public Portal(Vector2 position) : base("Portal", true, position, "portal", new RendererOptions(Color.White, 3, 4))
        {
            renderer.Scale = 0.5f;
        }

        public override void Update()
        {
            renderer.ImageIndex++;
            base.Update();

            if (Vector2.Distance(Player.Instance.transform.Position, transform.Position) < 16)
            {
                MainScene.Level++;
                TGame.Instance.LoadScene(new MainScene());
            }
        }

        public override void Damage(float damage, Character dealer)
        {

        }
    }
}
