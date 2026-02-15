using FDK.UI.Base;
using FDK.UI.Base.Interface;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FDK.Core.UI
{
    public interface IStaticSelectableUIData : IGameUIData
    {
        string Id { get; }
    }

    public abstract class BaseStaticSelectionUIItem<U> : BaseUIPanel where U : IBaseSelectableUiItem
    {
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected RectTransform _content;

        public UnityEvent<U> OnButtonClicked;

        public U SelectedContainer { get; private set; }
        public U[] ItemContainers;

        public bool IsSelected { get; private set; }

        public void Initialize(U[] containers)
        {
            ItemContainers = containers;
            UpdateUI();
            SelectFirst();
        }

        public override void UpdateUI()
        {
            for (int i = 0; i < ItemContainers.Length; i++)
            {
                if (i < ItemContainers.Length)
                {
                    var container = ItemContainers[i];
                    container.UpdateUI();
                }
            }
        }

        public virtual void Select(U selectable)
        {
            SelectedContainer = selectable;
            foreach (var item in ItemContainers)
            {
                var showUI = item.Equals(SelectedContainer);
                if (showUI)
                {
                    item.Select();
                }
                else
                {
                    item.Deselect();
                }
            }
        }

        private void SelectFirst()
        {
            if (ItemContainers.Length <= 0) return;
            var firstItem = ItemContainers.FirstOrDefault();
            Select(firstItem);
            Debug.LogError(firstItem);
        }
    }
}
