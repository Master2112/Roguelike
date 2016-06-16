using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    public class Transform
    {
        private GameObject owner;

        private Vector2 lastLocalPos;
        private float lastLocalRot;

        public Transform(GameObject owner)
        {
            this.owner = owner;
        }

        public void UpdateLocalPos()
        {
            if (owner.Parent != null)
            {
                LocalPosition = lastLocalPos;
                LocalRotation = lastLocalRot;
            }
        }

        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Sin(rotation), (float)-Math.Cos(rotation));
            }

            set
            {
                if (value.Equals(Vector2.Zero))
                {

                }
                else
                {
                    value.Normalize();
                    rotation = MathHelper.ToRadians(((float)Math.Atan2(value.Y, value.X) * 180 / (float)Math.PI) + 90);
                }
            }
        }

        public Vector2 Right
        {
            get
            {
                return new Vector2((float)Math.Sin(rotation + MathHelper.ToRadians(90)), (float)-Math.Cos(rotation + MathHelper.ToRadians(90)));
            }

            set
            {
                rotation = MathHelper.ToRadians(((float)Math.Atan2(value.Y, value.X) * 180 / (float)Math.PI) + 90) - MathHelper.ToRadians(90);
            }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;

                if (owner.Parent != null)
                    lastLocalPos = LocalPosition;
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                if (owner.Parent == null)
                    return position;
                else
                {

                    Vector2 parentPos = owner.Parent.transform.position;
                    Vector2 deltaP = Position - parentPos;

                    float length = deltaP.Length();

                    float angle = MathHelper.ToRadians(((float)Math.Atan2(-deltaP.Y, deltaP.X) * 180f / (float)Math.PI) + 90f);

                    angle += owner.Parent.transform.Rotation;

                    Vector2 rotated = new Vector2((float)Math.Sin(angle) * length, (float)Math.Cos(angle) * length);

                    return rotated;
                }
            }

            set
            {
                lastLocalPos = value;

                if (owner.Parent == null)
                    position = value;
                else
                {
                    Vector2 parentPos = owner.Parent.transform.Position;

                    Vector2 deltaP = value;

                    float angle = MathHelper.ToRadians(((float)Math.Atan2(-deltaP.Y, deltaP.X) * 180f / (float)Math.PI) + 90f);

                    angle -= owner.Parent.transform.Rotation;

                    float length = lastLocalPos.Length();

                    Vector2 rotatedParentPos = parentPos + new Vector2((float)Math.Sin(angle) * length, (float)Math.Cos(angle) * length);

                    position = rotatedParentPos;
                }
            }
        }

        private float rotation = 0;

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;

                if (owner.Parent != null)
                    lastLocalRot = LocalRotation;
            }
        }

        public float LocalRotation
        {
            get
            {
                if (owner.Parent == null)
                    return rotation;
                else
                    return rotation - owner.Parent.transform.Rotation;
            }

            set
            {
                lastLocalRot = value;

                if (owner.Parent == null)
                    rotation = value;
                else
                    rotation = owner.Parent.transform.Rotation + value;
            }
        }
    }
}
