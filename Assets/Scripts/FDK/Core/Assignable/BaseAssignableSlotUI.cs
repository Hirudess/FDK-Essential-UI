using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.Core.Assignable
{
    public abstract class BaseAssignableSlotUI<T, U> : MonoBehaviour,
    IBaseSelectableUiItem where T : IAssignableSlotData<U> where U : IAssignableData
    {
        [SerializeField] private Button _button;

        public T AssignableSlot { get; protected set; }
        public System.Action<T> OnButtonClicked;

        public UnityEvent OnAssign { get; protected set; }
        public bool IsBlocked { get; protected set; }
        public bool IsSelected { get; protected set; }


        public virtual void Initialize(System.Action<T> onClick)
        {
            OnButtonClicked = onClick;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => OnClick());
            UpdateUI();
        }

        public void UpdateGameData(U assignableItem)
        {
            if (AssignableSlot == null) return;

            AssignableSlot.Assign(assignableItem);
            UpdateUI();
        }

        public virtual void UpdateUI()
        {
        }

        private void OnClick()
        {
            if (AssignableSlot == null) return;
            OnButtonClicked?.Invoke(AssignableSlot);
            Select();
        }

        public virtual void Deselect()
        {

        }

        public void Hide()
        {

        }

        public virtual void Select()
        {

        }

        public void Show()
        {

        }
    }
}
