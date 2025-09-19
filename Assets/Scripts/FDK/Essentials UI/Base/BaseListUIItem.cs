using FDK.UI.Base;
using FDK.UI.Base.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace FDK.Core
{
    public abstract class BaseListUIItem<T, U> : BaseUIPanel where T : IGameUIData where U : BaseUiItem
    {
        [SerializeField] private U _uiPrefab;
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected RectTransform _content;

        public Dictionary<T, U> ItemContainerDict = new();

        public List<T> GameData = new();
        public List<U> ItemContainer = new();

        public bool IsSelected { get; private set; }

        public void Initialize(List<T> gameData)
        {
            GameData = gameData;
            UpdateUI();
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
                    container.UpdateUI();
                    ItemContainerDict.Add(GameData[i], container);
                }
                else
                {
                    var data = GameData[i];
                    var ui = Instantiate(_uiPrefab, _content);
                    ItemContainer.Add(ui);

                    ItemContainerDict.Add(data, ui);
                }
            }
        }
    }
}
