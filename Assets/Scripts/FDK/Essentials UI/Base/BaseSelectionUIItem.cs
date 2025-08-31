using FDK.UI.Base;
using FDK.UI.Base.Interface;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace FDK
{
    public abstract class BaseSelectionUIItem<T, U> : BaseUIPanel, IBaseSelectableUiItem where T : IGameUIData where U : BaseSelectableUIItem<T>
    {
        [SerializeField] private U _selectionUIPrefabs;
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected RectTransform _content;

        public UnityEvent<T> OnButtonClicked;
        public Dictionary<T, U> Items = new();

        public T SelectedGameData { get; private set; }
        public List<T> GameData = new();

        public bool IsSelected { get; private set; }

        public void Initialize(List<T> gameData)
        {
            GameData = gameData;
            UpdateUI();

            SelectFirst();
        }

        protected virtual void Select(T gameData)
        {
            OnButtonClicked?.Invoke(gameData);
            if (!Items.ContainsKey(gameData)) { return; }
            var selectable = Items[gameData];
            Select(selectable);
        }

        public override void UpdateUI()
        {
            if (GameData == null) return;
            for (int i = 0; i < GameData.Count; i++)
            {
                var data = Items.ElementAt(i);
                if (i < Items.Count)
                {
                    data.Value.UpdateUI();
                }
                else
                {
                    var ui = Instantiate(_selectionUIPrefabs, _content);
                    ui.Initialize(data.Key, Select);
                }
            }
        }

        public virtual void Select(U selectable)
        {
            foreach (var item in Items)
            {
                var showUI = item.Value.Equals(selectable);
                if (showUI)
                {
                    item.Value.Select();
                }
                else
                {
                    item.Value.Deselect();
                }
            }
        }

        private void SelectFirst()
        {
            if (Items.Count <= 0) return;
            var firstItem = Items.FirstOrDefault();
            Select(firstItem.Key);
        }

        public void Deselect()
        {

        }

        public void Select()
        {
            throw new System.NotImplementedException();
        }
    }
}
