using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Objects.Items
{
    public class Weapon : Item
    {
        public override string DisplayName { get { return Enchantment != Enchantments.None ? Enchantment.ToString() + " " + Name : Name; } }

        public Enchantments Enchantment;

        public float Damage;
        public float AttackTime;
        public float StaminaCost;
        public float EffectDamage;


        public Weapon(string spriteName, string name, float damage, float attackTime, float staminaCost, Enchantments enchantment = Enchantments.None, float effectDamage = 0)
            : base(name, spriteName)
        {
            Name = name;
            Damage = damage;
            AttackTime = attackTime;
            StaminaCost = staminaCost;
            Enchantment = enchantment;
            EffectDamage = effectDamage;
        }
    }
}
