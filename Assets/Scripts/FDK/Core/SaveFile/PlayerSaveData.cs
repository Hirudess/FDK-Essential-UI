using FDK.Inventory;
using System;
using System.Collections.Generic;

namespace FDK.Core.SaveFile
{
    [Serializable]
    public class PlayerSaveData
    {
        public string SaveVersion;
        public PlayerInfo PlayerInfo;
        public Inventory Inventory;
        public PlayerEquip PlayerEquip;

        public PlayerSaveData()
        {
            SaveVersion = "0.0.0.0.1";
            PlayerInfo = new PlayerInfo();
            Inventory = new Inventory();
            PlayerEquip = new PlayerEquip();
        }
    }

    [Serializable]
    public class PlayerInfo
    {
        public string PlayerName;
        public float PlaytimeHours;
        public string LastSave;


        public PlayerInfo()
        {
            PlayerName = "Save File";
            PlaytimeHours = 0;
            LastSave = null;
        }
    }

    [Serializable]
    public class PlayerEquip
    {
        public string WeaponId;
        public string ArmorId;

        public void EquipWeapon(string weaponId)
        {
            WeaponId = weaponId;
        }

        public void EquipArmor(string armorId)
        {
            ArmorId = armorId;
        }
    }

    [Serializable]
    public class Character
    {
        public string Id;
        public string Name;
        public string ClassType; // Renamed from "@class" (avoid reserved keywords)
        public int Level;
        public int Experience;
        public CharacterStats Stats;
    }

    [Serializable]
    public class CharacterStats
    {
        public int Health;
        public int Attack;
        public int Defense;
    }

    [Serializable]
    public class Inventory
    {
        public int MaxSlots;
        public int UsedSlots;
        public InventoryItem[] Items;

        public Inventory()
        {
            MaxSlots = 20;
            Items = new InventoryItem[MaxSlots];
        }

        public Inventory(Dictionary<string, ItemSlotPlayerData> inventory)
        {
            MaxSlots = 20;
            Items = new InventoryItem[inventory.Count];

            var count = 0;
            foreach (var item in inventory)
            {
                Items[count].Id = item.Key;
                Items[count].Stack = item.Value.Amount;
                count++;
            }
        }

    }

    [Serializable]
    public class InventoryItem
    {
        public string Id;
        public int Stack;
    }
}