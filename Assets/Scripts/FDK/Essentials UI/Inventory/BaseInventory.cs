using FDK.Core.GameData;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FDK.Inventory
{
    public interface IBaseInventory<T, U> where T : BaseSlotPlayerData<U> where U : BaseItemGameData
    {
        UnityEvent OnInventoryUpdated { get; }
        int Capacity { get; }
        int CurrentCount { get; }
        bool IsFull { get; }

        void AddItem(U itemData, int amount);
        void RemoveItem(string itemId, int removedAmount);
        void Clear();
        Dictionary<string, T> GetAllItems();
        T GetItem(string itemId);
        bool HasItem(string itemId);
    }

    public abstract class BaseInventory<T, U> : IBaseInventory<T, U> where T : BaseSlotPlayerData<U> where U : BaseItemGameData
    {
        protected readonly Dictionary<string, T> Items = new Dictionary<string, T>();

        public virtual int Capacity { get; protected set; } = 20;
        public int CurrentCount => Items.Count;
        public bool IsFull => CurrentCount >= Capacity;

        public UnityEvent OnInventoryUpdated { get; } = new();

        public virtual void AddItem(U itemData, int amount)
        {
            var itemId = itemData.Id;
            if (string.IsNullOrEmpty(itemId) || itemData == null || IsFull)
            {
                return;
            }

            if (Items.ContainsKey(itemId))
            {
                var slot = Items[itemId];
                var exceedMaxStack = slot.Amount + amount > slot.MaxStack;
                if (exceedMaxStack)
                {
                    return;
                }
                else
                {
                    Items[itemId].Amount += amount;
                }
            }
            else
            {
                CreateAndRegisterSlot(itemData, amount);
            }
        }

        protected virtual void CreateAndRegisterSlot(U item, int amount)
        {

        }

        public virtual void RemoveItem(string itemId, int removedAmount)
        {
            if (string.IsNullOrEmpty(itemId)) return;
            if (!Items.ContainsKey(itemId)) return;

            var slot = Items[itemId];
            var hasEnoughtAmount = slot.Amount >= removedAmount;
            if (hasEnoughtAmount)
            {
                slot.Amount -= removedAmount;
                if (slot.Amount <= 0)
                {
                    Items.Remove(itemId);
                }
            }
        }

        public virtual bool HasItem(string itemId)
        {
            return !string.IsNullOrEmpty(itemId) && Items.ContainsKey(itemId);
        }

        public virtual T GetItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId) || !Items.ContainsKey(itemId))
            {
                return null;
            }

            return Items[itemId];
        }

        public virtual void Clear()
        {
            Items.Clear();
        }

        public virtual Dictionary<string, T> GetAllItems()
        {
            return Items;
        }

        protected virtual bool CanAddItem(string itemId, T item)
        {
            return !string.IsNullOrEmpty(itemId) &&
                   item != null &&
                   !IsFull &&
                   !Items.ContainsKey(itemId);
        }
    }
}
