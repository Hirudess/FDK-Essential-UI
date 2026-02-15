using FDK.UI.Base;
using FDK.UI.Base.Interface;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FDK.UI
{

    [System.Serializable]
    public abstract class BaseSelectableGroupUI<T> : BaseUIPanel where T : SelectableUI
    {
        [SerializeField] private T _selectableUIPrefab;
        [SerializeField] private RectTransform _contentRoot;
        [SerializeField] public List<T> _selectableUIs;
        public T CurrentSelected;

        public UnityEvent<IGameUIData> OnCurrentSelectedChangedEvt { get; protected set; } = new();
        public List<T> SelectableUIs => _selectableUIs;

        public void Initialize(List<IGameUIData> gameUIData)
        {
            foreach (var data in gameUIData)
            {
                var ui = Instantiate(_selectableUIPrefab, _contentRoot);
                ui.OnSelectableChangedEvt.AddListener(OnCurrentSelectedChanged);
            }
        }

        public void Select(T selected)
        {
            CurrentSelected = selected;
            UpdateUI();
        }

        public override void UpdateUI()
        {
            foreach (var item in SelectableUIs)
            {
                if (CurrentSelected == item)
                {
                    item.Select();
                }
                else
                {
                    item.Deselect();
                }
            }
        }

        public void OnCurrentSelectedChanged(IGameUIData gameUIData)
        {
            OnCurrentSelectedChangedEvt?.Invoke(gameUIData);
        }
    }
}
