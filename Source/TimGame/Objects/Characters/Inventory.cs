using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimGame.Objects.Items;

namespace TimGame.Objects.Characters
{
    public class Inventory
    {
        public Armor EquippedArmor;
        public Weapon EquippedWeapon;
        public Offhand EquippedOffhand;

        public int gridWidth = 10;
        public int gridHeight = 5;
        public int maxItems = 0;

        public List<Item> Items;
        public Queue<string> newItems;

        private Character attachedCharacter;

        public Inventory(Character attachedChar)
        {
            attachedCharacter = attachedChar;
            Items = new List<Item>();
            maxItems = gridWidth * gridHeight;

            newItems = new Queue<string>();
        }

        public void Add(Item item)
        {
            Items.Add(item);
            newItems.Enqueue(item.DisplayName);
        }

        public void UnequipWeapon()
        {
            if (EquippedWeapon != null)
                Items.Add(EquippedWeapon);

            EquippedWeapon = null;
        }

        public void UnequipArmor()
        {
            if (EquippedArmor != null)
                Items.Add(EquippedArmor);
            
            EquippedArmor = null;
        }

        public void UnequipOffhand()
        {
            if (EquippedOffhand != null) 
                Items.Add(EquippedOffhand);
            
            EquippedOffhand = null;
        }

        public string GetSelectedName(int index)
        {
            if (Items.Count > index)
            {
                return Items[index].DisplayName;
            }

            return "";
        }

        public void UseItem(int index)
        {
            if (Items.Count > index)
            {
                if (Items[index] is Armor)
                {
                    UnequipArmor();

                    EquippedArmor = (Armor)Items[index];
                    Items.RemoveAt(index);
                    return;
                }

                if (Items[index] is Weapon)
                {
                    UnequipWeapon();

                    EquippedWeapon = (Weapon)Items[index];
                    Items.RemoveAt(index);
                    return;
                }

                if (Items[index] is Offhand)
                {
                    UnequipOffhand();

                    EquippedOffhand = (Offhand)Items[index];
                    Items.RemoveAt(index);
                    return;
                }

                Items[index].Use(attachedCharacter);
            }

            Items.RemoveAll(o => o == null);
            Items.RemoveAll(o => o.Destroy);
        }
    }
}
