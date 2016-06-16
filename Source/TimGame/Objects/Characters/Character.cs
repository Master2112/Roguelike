using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Engine;
using TimGame.Objects.Items;
using TimGame.Objects.World;

namespace TimGame.Objects.Characters
{
    public abstract class Character : WorldObject
    {
        public enum Genders
        {
            Male,
            Female
        }

        public enum AnimationRows
        {
            Down,
            Left,
            Right,
            Up
        }

        public enum HairStyles
        {
            Bald
        }

        private bool attacking = false;
        private float attackTimer = 0;

        public float Health = 100;
        public float MaxHealth
        {
            get
            {
                return 10 + (CharStats.Vitality * 3);
            }
        }

        protected float walkSpeed = 2;
        private bool setSprite = false;
        protected AnimationRows direction = AnimationRows.Down;
        private int animationIndex = 0;
        private int animTimer = 0;
        private int animDelay = 10;
        private Genders gender = Genders.Male;
        protected Vector2 collisionCheckOffset = new Vector2(0, 15);
        public Inventory Inventory;

        public bool canAttack { get { return attackTimer < -0.5f; } }

        public Vector2 GridPosition
        {
            get
            {
                return PathfindConstants.WorldToGrid(transform.Position + collisionCheckOffset);
            }
        }

        protected HairStyles hairStyle = HairStyles.Bald;
        
        public Weapon weapon;

        private Renderer armorRenderer;
        private Renderer hairRenderer;

        protected Vector2 moveAxis = Vector2.Zero;
        private float damageFlashAmount = 0;
        public Stats CharStats;

        public Character(Vector2 position, string name, Genders gender = Genders.Male, Stats stats = null) : base(name, true, position, "ball")
        {
            this.gender = gender;

            CharStats = stats;

            if(CharStats == null)
            {
                CharStats = new Stats(1, 1, 1, 1);
            }

            Health = MaxHealth;

            armorRenderer = new Renderer(this);
            hairRenderer = new Renderer(this);

            Inventory = new Inventory(this);
        }

        public override Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)transform.Position.X - 20, (int)transform.Position.Y + (int)collisionCheckOffset.Y - 20, 40, 40);
            }
        }

        public void Attack()
        {
            if (canAttack)
            {
                attacking = true;
                attackTimer = weapon != null ? weapon.AttackTime : 0.3f;

                List<WorldObject> toDamage = new List<WorldObject>();

                if (direction == AnimationRows.Down)
                {
                    toDamage.AddRange(WorldObject.AllObjects.FindAll(o => o != this && o.Bounds.Contains(transform.Position + collisionCheckOffset + new Vector2(0, 32))));
                }
                if (direction == AnimationRows.Left)
                {
                    toDamage.AddRange(WorldObject.AllObjects.FindAll(o => o != this && o.Bounds.Contains(transform.Position + collisionCheckOffset + new Vector2(-32, 0))));
                }
                if (direction == AnimationRows.Right)
                {
                    toDamage.AddRange(WorldObject.AllObjects.FindAll(o => o != this && o.Bounds.Contains(transform.Position + collisionCheckOffset + new Vector2(32, 0))));
                }
                if (direction == AnimationRows.Up)
                {
                    toDamage.AddRange(WorldObject.AllObjects.FindAll(o => o != this && o.Bounds.Contains(transform.Position + collisionCheckOffset + new Vector2(0, -32))));
                }

                foreach(WorldObject obj in toDamage)
                {
                    obj.Damage(weapon != null ? weapon.Damage + weapon.EffectDamage : 1 + CharStats.Strength, this);
                }
            }
        }

        public override void Update()
        {
            if (Health <= 0)
                Destroy();

            Inventory.Items.RemoveAll(o => o == null);

            DrawDepth = -((int.MaxValue / 2) + (int)transform.Position.Y);

            attackTimer -= Time.DeltaTime;

            if (attackTimer < 0)
                attacking = false;

            if (!setSprite)
            {
                setSprite = true;
                renderer.SetTexture(gender == Genders.Female ? "female_base" : "male_base", 4, 5);
            }

            Move();
            Animate();

            renderer.BlendColor = Color.Lerp(Color.White, Color.Red, damageFlashAmount);
            damageFlashAmount -= Time.DeltaTime;

            base.Update();
        }

        public override void Damage(float damage, Character dealer)
        {
            damageFlashAmount = 1;
            Health -= damage;
            base.Damage(damage, dealer);
        }

        private void Move()
        {
            if (!attacking)
            {
                if (moveAxis.Y > 0)
                    direction = AnimationRows.Down;

                if (moveAxis.Y < 0)
                    direction = AnimationRows.Up;

                if (moveAxis.X > 0.01f)
                    direction = AnimationRows.Right;

                if (moveAxis.X < -0.01f)
                    direction = AnimationRows.Left;

                moveAxis = moveAxis.Normalized(); //Normalize() doesn't check for zero-length first.

                RoomData roomX = CellSpawner.GetRoomAtWorldPosition(transform.Position + collisionCheckOffset + new Vector2((float)Math.Round(moveAxis.X) * 16, -10));

                RoomData roomX2 = CellSpawner.GetRoomAtWorldPosition(transform.Position + collisionCheckOffset + new Vector2((float)Math.Round(moveAxis.X) * 16, 10));

                if (roomX != null && roomX.type == RoomData.Type.Passable && roomX2 != null && roomX2.type == RoomData.Type.Passable)
                {
                    transform.Position += new Vector2(moveAxis.X * walkSpeed, 0);
                }

                RoomData roomY = CellSpawner.GetRoomAtWorldPosition(transform.Position + collisionCheckOffset + new Vector2(10, (float)Math.Round(moveAxis.Y) * 16));

                RoomData roomY2 = CellSpawner.GetRoomAtWorldPosition(transform.Position + collisionCheckOffset + new Vector2(-10, (float)Math.Round(moveAxis.Y) * 16));

                if (roomY != null && roomY.type == RoomData.Type.Passable && roomY2 != null && roomY2.type == RoomData.Type.Passable)
                {
                    transform.Position += new Vector2(0, moveAxis.Y * walkSpeed);
                }
            }
        }

        private void Animate()
        {
            if (moveAxis.Length() > 0)
            {
                if (animTimer == animDelay)
                {
                    animationIndex = (animationIndex + 1) % 4;
                    animTimer = 0;
                }
                else
                {
                    animTimer++;
                }
            }
            else
            {
                animationIndex = 0;
            }

            int offset = 0;

            if (!attacking)
            {
                if (direction == AnimationRows.Down)
                    offset = 0;
                if (direction == AnimationRows.Left)
                    offset = 4;
                if (direction == AnimationRows.Right)
                    offset = 8;
                if (direction == AnimationRows.Up)
                    offset = 12;
            }
            else
            {
                offset = 16;

                if (direction == AnimationRows.Down)
                    animationIndex = 0;
                if (direction == AnimationRows.Left)
                    animationIndex = 1;
                if (direction == AnimationRows.Right)
                    animationIndex = 2;
                if (direction == AnimationRows.Up)
                    animationIndex = 3;
            }

            renderer.ImageIndex = offset + animationIndex;
            hairRenderer.ImageIndex = offset + animationIndex;
            armorRenderer.ImageIndex = offset + animationIndex;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            if(Active)
            {
                base.Draw(batch);

                //batch.Draw(SpriteLoader.Instance.GetSprite("wall"), new Rectangle((int)-Renderer.CameraOffset.X + Bounds.X, (int)-Renderer.CameraOffset.Y + Bounds.Y, Bounds.Width, Bounds.Height), Color.White);

                if(hairStyle != HairStyles.Bald)
                    hairRenderer.Draw(batch);

                if(Inventory.EquippedArmor != null)
                    armorRenderer.Draw(batch);
            }
        }

        public class Stats
        {
            public int Vitality, Strength, Dexterity, Magic;

            public Stats(int vit, int str, int dex, int mag)
            {
                Vitality = vit;
                Strength = str;
                Dexterity = dex;
                Magic = mag;
            }
        }
    }
}
