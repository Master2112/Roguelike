using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    class Input
    {
        private class PressedKeys
        {
            public bool thisFrame;
            public bool isActive;
            public Keys key;
        }

        private static List<PressedKeys> pressedKeys = new List<PressedKeys>();

        private static KeyboardState keyboardState = Keyboard.GetState();
        private static MouseState mouseState = Mouse.GetState();

        public static bool ConfirmPressed
        {
            get
            {
                return KeyPressed(Keys.Enter) || KeyPressed(Keys.Space);
            }
        }

        public static bool ConfirmHeld
        {
            get
            {
                return KeyHeld(Keys.Enter) || KeyHeld(Keys.Space);
            }
        }
        //
        public static bool LeftPressed
        {
            get
            {
                return KeyPressed(Keys.Left) || KeyPressed(Keys.A);
            }
        }

        public static bool LeftHeld
        {
            get
            {
                return KeyHeld(Keys.Left) || KeyHeld(Keys.A);
            }
        }
        //
        public static bool RightPressed
        {
            get
            {
                return KeyPressed(Keys.Right) || KeyPressed(Keys.D);
            }
        }

        public static bool RightHeld
        {
            get
            {
                return KeyHeld(Keys.Right) || KeyHeld(Keys.D);
            }
        }
        //
        public static bool UpPressed
        {
            get
            {
                return KeyPressed(Keys.Up) || KeyPressed(Keys.W);
            }
        }

        public static bool UpHeld
        {
            get
            {
                return KeyHeld(Keys.Up) || KeyHeld(Keys.W);
            }
        }
        //
        public static bool DownPressed
        {
            get
            {
                return KeyPressed(Keys.Down) || KeyPressed(Keys.S);
            }
        }

        public static bool DownHeld
        {
            get
            {
                return KeyHeld(Keys.Down) || KeyHeld(Keys.S);
            }
        }

        public static void UpdateState()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            Keys[] keys = keyboardState.GetPressedKeys();

            for (int k = 0; k < pressedKeys.Count; k++)
            {
                pressedKeys[k].isActive = false;   
            }

            for(int i = 0; i < keys.Length; i++)
            {
                bool keyFound = false;

                for(int k = 0; k < pressedKeys.Count; k++)
                {
                    if(pressedKeys[k].key == keys[i])
                    {
                        pressedKeys[k].thisFrame = false;
                        pressedKeys[k].isActive = true;
                        keyFound = true;
                    }
                }

                if(!keyFound)
                {
                    PressedKeys newKey = new PressedKeys();
                    newKey.thisFrame = true;
                    newKey.key = keys[i];
                    newKey.isActive = true;

                    pressedKeys.Add(newKey);
                }
            }

            pressedKeys.RemoveAll(o => !o.isActive);
        }

        public static bool KeyHeld(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            for (int k = 0; k < pressedKeys.Count; k++)
            {
                if(pressedKeys[k].key == key)
                {
                    return pressedKeys[k].thisFrame;
                }
            }

            return false;
        }

        public static bool MouseButtonPressed(int btn = 0)
        {
            if(btn == 0)
                return mouseState.LeftButton == ButtonState.Pressed;
            else
                return mouseState.RightButton == ButtonState.Pressed;
        }

        public static Vector2 MousePosition()
        {
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }
    }
}

/* Input Textfield
oldKeyState = keyState;
keyState = Keyboard.GetState();
foreach (Keys key in keyState.GetPressedKeys()){
    if(oldKeyState.IsKeyUp(key)){
        if (key == Keys.Back){
            name = name.Remove(name.Length - 1, 1);
            }
        else if (keys == Keys.Enter){
            System.Diagnostics.Trace.WriteLine(name); // (just do whatever you need with the name
            name = "";
            }
        else{
            name += key.ToString();
            }
        }
}
*/