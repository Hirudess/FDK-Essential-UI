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
        [SerializeField] protected U _selectionUIPrefabs;
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected RectTransform _content;

        public UnityEvent<T> OnButtonClicked;
        public UnityEvent<T> OnSelectionChanged = new();
        public Dictionary<T, U> ItemContainerDict = new();

        public T SelectedGameData { get; private set; }
        public List<T> GameData = new();
        public List<U> ItemContainer = new();

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
            if (!ItemContainerDict.ContainsKey(gameData)) { return; }
            var selectable = ItemContainerDict[gameData];
            SelectedGameData = gameData;
            OnSelectionChanged.Invoke(gameData);

            Select(selectable);
        }

        public override void UpdateUI()
        {
            ItemContainerDict.Clear();

            if (GameData == null) return;
            for (int i = 0; i < GameData.Count; i++)
            {
                if (i < ItemContainer.Count)
                {
                    var container = ItemContainer[i];
                    container.UpdateGameData(GameData[i]);
                    container.UpdateUI();

                    ItemContainerDict.Add(GameData[i], container);
                }
                else
                {
                    var data = GameData[i];
                    var ui = Instantiate(_selectionUIPrefabs, _content);
                    ui.Initialize(data, Select);
                    ItemContainer.Add(ui);

                    ItemContainerDict.Add(data, ui);
                }
            }
        }

        public virtual void Select(U selectable)
        {
            foreach (var item in ItemContainerDict)
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
            if (ItemContainerDict.Count <= 0) return;
            var firstItem = ItemContainerDict.FirstOrDefault();
            Select(firstItem.Key);
        }

        public void Deselect()
        {

        }

        public void Select()
        {

        }
    }
}
