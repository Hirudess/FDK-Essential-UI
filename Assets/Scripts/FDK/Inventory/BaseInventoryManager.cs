using System.Collections.Generic;
using UnityEngine;

namespace FDK.Inventory
{
    public abstract class InventoryBase<T> : MonoBehaviour where T : class
    {
        protected readonly Dictionary<string, T> items = new Dictionary<string, T>();

        public virtual int Capacity { get; protected set; } = 20;
        public int CurrentCount => items.Count;
        public bool IsFull => CurrentCount >= Capacity;

        public virtual bool AddItem(string itemId, T item)
        {
            if (string.IsNullOrEmpty(itemId) || item == null || IsFull || items.ContainsKey(itemId))
            {
                return false;
            }

            items.Add(itemId, item);
            return true;
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
