using FDK.UI.Base.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FDK.UI
{
    public abstract class SelectableUI : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        public UnityEvent<IGameUIData> OnSelectableChangedEvt = new ();
        public IGameUIData GameData { get; private set; }
        public bool IsSelected { get; protected set; }

        public void Initialize(IGameUIData gameData)
        {
            GameData = gameData;
            _button.onClick.AddListener(OnSelectableChanged);
        }

        public virtual void UpdateUI()
        {

        }

        private void OnSelectableChanged()
        {
            OnSelectableChangedEvt.Invoke(GameData);
        }

        public virtual void Select()
        {

        }

        public virtual void Deselect()
        {

        }

        public virtual void Confirm()
        {

        }

        public void OnSelect(BaseEventData eventData)
        {

        }
    }
}
