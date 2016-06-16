using TimGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Scenes;

namespace TimGame.Engine
{
    class TGame
    {
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 600;

        public static TGame Instance { get; private set; }
        private Scene activeScene;

        public SpriteFont MainFont;

        public void Start()
        {
            Instance = this;
            LoadScene(new CharSelect());
        }

        public void Update()
        {
            if (activeScene != null)
                activeScene.Update();
        }

        public void LoadScene(Scene scene)
        {
            if (activeScene != null)
                activeScene.Clean();

            activeScene = scene;
            LoadSceneAdditive(activeScene);
        }

        public void LoadSceneAdditive(Scene scene)
        {
            scene.InitScene();
        }
    }
}
