using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Engine
{
    class Checkable<T>
    {
        private T innerValue = default(T);
        private bool changed = false;

        public Checkable(){}
        public Checkable(T initialValue)
        {
            innerValue = initialValue;
        }

        public bool HasChanged
        {
            get
            {
                bool cha = changed;
                changed = false;
                return cha;
            }
        }

        public T Value
        {
            get
            {
                return innerValue;
            }

            set
            {
                if (!value.Equals(innerValue))
                    changed = true;

                innerValue = value;
            }
        }

        public static implicit operator T(Checkable<T> c)
        {
            return c.Value;
        }
    }
}
