using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimGame.Objects.Items
{
    public class Armor : Item
    {
        public override string DisplayName { get { return Enchantment != Enchantments.None ? Enchantment.ToString() + " " + Name : Name; } }

        public Enchantments Enchantment;

        public float Defense;
        public float EffectDefense;
        public string CharacterArmorSprite;

        public Armor(string spriteName, string charSpriteName, string name, float defense, Enchantments enchantment = Enchantments.None, float effectDefense = 0) : base(name, spriteName)
        {
            Name = name;
            Defense = defense;
            Enchantment = enchantment;
            EffectDefense = effectDefense;

            CharacterArmorSprite = charSpriteName;
        }

        public float TransformDamage(float damage)
        {
            return damage * Defense;
        }
    }
}
