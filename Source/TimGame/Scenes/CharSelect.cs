using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TimGame;
using TimGame.Engine;
using TimGame.Objects;
using NLua;
using TimGame.Objects.World;
using TimGame.Objects.Characters;
using Microsoft.Xna.Framework.Input;

namespace TimGame.Scenes
{
    class CharSelect : Scene
    {
        private SelectionArrow arrow;
        private TextObject nameBox;
        private TextObject genderSelect;
        private TextObject startText;

        private string name = "Player";
        private int selectionIndex = 0;

        public CharSelect() : base("Main")
        {

        }

        public override void InitScene()
        {
            arrow = (SelectionArrow)MakeSceneObject(new SelectionArrow(Vector2.Zero));

            nameBox = (TextObject)MakeSceneObject(new TextObject(new Vector2(TGame.ScreenWidth / 2, 100), name));
            genderSelect = (TextObject)MakeSceneObject(new TextObject(new Vector2(TGame.ScreenWidth / 2, 200), Player.gender.ToString()));
            startText = (TextObject)MakeSceneObject(new TextObject(new Vector2(TGame.ScreenWidth / 2, 300), "Start Game"));

            nameBox.renderer.BlendColor = Color.White;
            genderSelect.renderer.BlendColor = Color.White;
            startText.renderer.BlendColor = Color.White;
        }

        public override void Update()
        {
            if (Input.KeyPressed(Keys.Down))
                selectionIndex++;

            if (Input.KeyPressed(Keys.Up))
                selectionIndex--;

            if (selectionIndex < 0)
                selectionIndex = 0;

            if (selectionIndex > 2)
                selectionIndex = 2;

            if (selectionIndex == 0)
            {
                DoNameInput();
                arrow.transform.Position = new Vector2(50, 100);
            }

            if (selectionIndex == 1)
            {
                if (Input.RightPressed || Input.LeftPressed)
                    Player.gender = (Player.Genders)(((int)Player.gender + 1) % 2);

                arrow.transform.Position = new Vector2(50, 200);
            }

            if (selectionIndex == 2)
            {
                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    TGame.Instance.LoadScene(new MainScene());

                arrow.transform.Position = new Vector2(50, 300);
            }

            nameBox.Text = name;
            genderSelect.Text = Player.gender.ToString();

            base.Update();
        }

        public void DoNameInput()
        {
            if (Input.KeyPressed(Keys.Back) && name.Length > 0)
                name = name.Remove(name.Length - 1);

            if (Input.KeyPressed(Keys.A))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "A" : "a";

            if (Input.KeyPressed(Keys.B))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "B" : "b";

            if (Input.KeyPressed(Keys.C))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "C" : "c";

            if (Input.KeyPressed(Keys.D))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "D" : "d";

            if (Input.KeyPressed(Keys.E))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "E" : "e";

            if (Input.KeyPressed(Keys.F))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "F" : "f";

            if (Input.KeyPressed(Keys.G))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "G" : "g";

            if (Input.KeyPressed(Keys.H))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "H" : "h";

            if (Input.KeyPressed(Keys.I))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "I" : "i";

            if (Input.KeyPressed(Keys.J))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "J" : "j";

            if (Input.KeyPressed(Keys.K))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "K" : "k";

            if (Input.KeyPressed(Keys.L))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "L" : "l";

            if (Input.KeyPressed(Keys.M))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "M" : "m";

            if (Input.KeyPressed(Keys.N))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "N" : "n";

            if (Input.KeyPressed(Keys.O))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "O" : "o";

            if (Input.KeyPressed(Keys.P))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "P" : "p";

            if (Input.KeyPressed(Keys.Q))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "Q" : "q";

            if (Input.KeyPressed(Keys.R))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "R" : "r";

            if (Input.KeyPressed(Keys.S))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "S" : "s";

            if (Input.KeyPressed(Keys.T))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "T" : "t";

            if (Input.KeyPressed(Keys.U))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "U" : "u";

            if (Input.KeyPressed(Keys.V))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "V" : "v";

            if (Input.KeyPressed(Keys.W))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "W" : "w";

            if (Input.KeyPressed(Keys.X))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "X" : "x";

            if (Input.KeyPressed(Keys.Y))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "Y" : "y";

            if (Input.KeyPressed(Keys.Z))
                name += Input.KeyHeld(Keys.LeftShift) || Input.KeyHeld(Keys.RightShift) ? "Z" : "z";

            if (Input.KeyPressed(Keys.Space))
                name += " ";

            if (Input.KeyPressed(Keys.NumPad0))
                name += "0";

            if (Input.KeyPressed(Keys.NumPad1))
                name += "1";

            if (Input.KeyPressed(Keys.NumPad2))
                name += "2";

            if (Input.KeyPressed(Keys.NumPad3))
                name += "3";

            if (Input.KeyPressed(Keys.NumPad4))
                name += "4";

            if (Input.KeyPressed(Keys.NumPad5))
                name += "5";

            if (Input.KeyPressed(Keys.NumPad6))
                name += "6";

            if (Input.KeyPressed(Keys.NumPad7))
                name += "7";

            if (Input.KeyPressed(Keys.NumPad8))
                name += "8";

            if (Input.KeyPressed(Keys.NumPad9))
                name += "9";
        }
    }
}
