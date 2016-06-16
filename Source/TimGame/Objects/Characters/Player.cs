using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Items.Offhands;
using TimGame.Objects.Items.Potions;
using TimGame.Objects.World;
using TimGame.Scenes;

namespace TimGame.Objects.Characters
{
    class Player: Character
    {
        public static Player Instance { get; private set; }

        private static InputStates inputState = InputStates.Character;

        private const float torchSight = 400;
        private const float normalSight = 200;
        public static Character.Genders gender;
        private int experience = 0;

        private float newItemTimer = 0;
        private float newItemDisplayTime = 3;
        private string itemDisplay;

        public int menuIndex = 0;

        public Player(Vector2 position) : base(position, "Player", gender)
        {
            Instance = this;

            Inventory.Add(new HealthPotion());
            Inventory.Add(new HealthPotion());
            Inventory.Add(new HealthPotion());
            Inventory.Add(new HealthPotion());
            Inventory.Add(new Poison());
            Inventory.Add(new Poison());
            Inventory.Add(new Poison());
            Inventory.Add(new Lightstone());
        }

        public override void Update()
        {
            base.Update();

            if (Inventory.EquippedOffhand is Lightstone || (baseScene is MainScene && !((MainScene)baseScene).Dark))
                WorldObject.ViewDistance = torchSight;
            else
                WorldObject.ViewDistance = normalSight;

            newItemTimer -= Time.DeltaTime;

            if(newItemTimer < 0 && Inventory.newItems.Count > 0)
            {
                itemDisplay = Inventory.newItems.Dequeue();
                newItemTimer = newItemDisplayTime;
            }

            GetInput();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            batch.DrawHorizontalBar(new Rectangle(10, TGame.ScreenHeight - 20, TGame.ScreenWidth - 20, 10), Health / MaxHealth, Color.White, Color.Red, 1);

            base.Draw(batch);

            if(inputState == InputStates.Character)
            {
                if (newItemTimer > 0)
                {
                    Vector2 center = (TGame.Instance.MainFont.MeasureString(itemDisplay) * 0.5f);
                    batch.DrawString(TGame.Instance.MainFont, itemDisplay, new Vector2(TGame.ScreenWidth * 0.5f, TGame.ScreenHeight - 32), Color.White, 0, center, 0.3f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);
                }
            }
            else if(inputState == InputStates.Inventory)
            {
                batch.DrawRectangle(new Rectangle(10, 10, TGame.ScreenWidth - 20, TGame.ScreenHeight - 40), Color.Gray);

                string name = "";

                if (menuIndex >= 3)
                {
                    name = Inventory.GetSelectedName(menuIndex - 3);
                }

                if (menuIndex == 0 && Inventory.EquippedWeapon != null)
                    name = Inventory.EquippedWeapon.DisplayName;

                if (menuIndex == 1 && Inventory.EquippedArmor != null)
                    name = Inventory.EquippedArmor.DisplayName;

                if (menuIndex == 2 && Inventory.EquippedOffhand != null)
                    name = Inventory.EquippedOffhand.DisplayName;

                Vector2 center = (TGame.Instance.MainFont.MeasureString(name) * 0.5f);
                batch.DrawString(TGame.Instance.MainFont, name, new Vector2(TGame.ScreenWidth * 0.75f, TGame.ScreenHeight * 0.25f), Color.White, 0, center, 0.3f, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0);

                batch.DrawRectangle(new Rectangle(20 + 10, 10 + 10, 32, 32), menuIndex == 0? Color.Red : Color.LightGray);

                if(Inventory.EquippedWeapon != null)
                    Inventory.EquippedWeapon.Draw(batch, new Rectangle(20 + 10, 10 + 10, 32, 32));

                batch.DrawRectangle(new Rectangle(20 + 10 + 42, 10 + 10, 32, 32), menuIndex == 1 ? Color.Red : Color.LightGray);

                if (Inventory.EquippedArmor != null)
                    Inventory.EquippedArmor.Draw(batch, new Rectangle(20 + 10 + 42, 10 + 10, 32, 32));

                batch.DrawRectangle(new Rectangle(20 + 10 + 84, 10 + 10, 32, 32), menuIndex == 2 ? Color.Red : Color.LightGray); //inv items equipped

                if (Inventory.EquippedOffhand != null)
                    Inventory.EquippedOffhand.Draw(batch, new Rectangle(20 + 10 + 84, 10 + 10, 32, 32));

                for (int x = 0; x < Inventory.gridWidth; x++)
                {
                    for (int y = 0; y < Inventory.gridHeight; y++)
                    {
                        batch.DrawRectangle(new Rectangle(20 + (x * 42), 64 + (y * 42), 32, 32), Color.LightGray);
                    }
                }

                for (int i = 0; i < Inventory.maxItems; i++)
                {
                    int xP = i;
                    int yP = 0;

                    while (xP >= Inventory.gridWidth)
                    {
                        xP -= Inventory.gridWidth;
                        yP++;
                    }

                    if (menuIndex - 3 == i)
                        batch.DrawRectangle(new Rectangle(20 + (xP * 42), 64 + (yP * 42), 32, 32), Color.Red);
                }

                for(int i = 0; i < Inventory.Items.Count; i++)
                {
                    if (Inventory.Items[i] != null)
                    {
                        int xP = i;
                        int yP = 0;

                        while (xP >= Inventory.gridWidth)
                        {
                            xP -= Inventory.gridWidth;
                            yP++;
                        }

                        Inventory.Items[i].Draw(batch, new Rectangle(20 + (xP * 42), 64 + (yP * 42), 32, 32));
                    }
                }
            }
        }

        public void GetInput()
        {
            if (inputState == InputStates.Character)
            {
                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.I))
                    inputState = InputStates.Inventory;

                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
                    inputState = InputStates.Menu;

                moveAxis = Vector2.Zero;

                if (Input.DownHeld)
                {
                    moveAxis += new Vector2(0, 1);
                }

                if (Input.UpHeld)
                {
                    moveAxis += new Vector2(0, -1);
                }

                if (Input.LeftHeld)
                {
                    moveAxis += new Vector2(-1, 0);
                }

                if (Input.RightHeld)
                {
                    moveAxis += new Vector2(1, 0);
                }

                if (Input.KeyHeld(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    Attack();
                }
            }
            else if(inputState == InputStates.Inventory)
            {
                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.I))
                    inputState = InputStates.Character;

                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
                    inputState = InputStates.Menu;

                if (menuIndex == 0 && Input.ConfirmPressed)
                    Inventory.UnequipWeapon();

                if (menuIndex == 1 && Input.ConfirmPressed)
                    Inventory.UnequipArmor();

                if (menuIndex == 2 && Input.ConfirmPressed)
                    Inventory.UnequipOffhand();

                if(menuIndex < 3)
                {
                    if (Input.RightPressed)
                        menuIndex++;

                    if(Input.DownPressed)
                    {
                        if (menuIndex == 0)
                            menuIndex = 3;

                        if (menuIndex == 1)
                            menuIndex = 4;

                        if (menuIndex == 2)
                            menuIndex = 5;
                    }

                    if (Input.LeftPressed || Input.UpPressed)
                        menuIndex--;
                }
                else
                {
                    if (Input.RightPressed)
                        menuIndex++;

                    if (Input.LeftPressed)
                        menuIndex--;

                    if (Input.DownPressed)
                        menuIndex += Inventory.gridWidth;

                    if (Input.UpPressed)
                    {
                        if (menuIndex == 3)
                            menuIndex = 0;
                        else if (menuIndex == 4)
                            menuIndex = 1;
                        else if (menuIndex == 5)
                            menuIndex = 2;
                        else
                            menuIndex -= Inventory.gridWidth;
                    }

                    if(Input.ConfirmPressed)
                    {
                        Inventory.UseItem(menuIndex - 3);
                    }

                    if (menuIndex > Inventory.maxItems + 2)
                        menuIndex = Inventory.maxItems + 2;
                }

                DrawDepth = -int.MaxValue;
            }
            else if (inputState == InputStates.Menu)
            {
                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.I))
                    inputState = InputStates.Inventory;

                if (Input.KeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
                    inputState = InputStates.Character;

                DrawDepth = -int.MaxValue;
            }

            if (menuIndex < 0)
                menuIndex = 0;
        }

        public enum InputStates
        {
            Character,
            Menu,
            Inventory
        }
    }
}
