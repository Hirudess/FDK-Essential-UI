using FDK.Core.GameData;
using System.Collections.Generic;

namespace FDK.Inventory
{
    public abstract class BaseInventory<T> where T : BaseItemGameData
    {
        protected readonly Dictionary<string, T> items = new Dictionary<string, T>();

        public virtual int Capacity { get; protected set; } = 20;
        public int CurrentCount => items.Count;
        public bool IsFull => CurrentCount >= Capacity;

        public virtual void AddItem(string itemId, T item)
        {
            if (string.IsNullOrEmpty(itemId) || item == null || IsFull || items.ContainsKey(itemId))
            {
                return;
            }

            items.Add(itemId, item);
        }

        public virtual bool RemoveItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId) || !items.ContainsKey(itemId))
            {
                return false;
            }

            items.Remove(itemId);
            return true;
        }

        public virtual bool HasItem(string itemId)
        {
            return !string.IsNullOrEmpty(itemId) && items.ContainsKey(itemId);
        }

        public virtual T GetItem(string itemId)
        {
            if (string.IsNullOrEmpty(itemId) || !items.ContainsKey(itemId))
            {
                return null;
            }

            return items[itemId];
        }

        public virtual void Clear()
        {
            items.Clear();
        }

        public virtual IEnumerable<KeyValuePair<string, T>> GetAllItems()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        protected virtual bool CanAddItem(string itemId, T item)
        {
            return !string.IsNullOrEmpty(itemId) &&
                   item != null &&
                   !IsFull &&
                   !items.ContainsKey(itemId);
        }
    }
}
