using UnityEngine;

namespace FDK.UI
{
    public abstract class BaseUi : MonoBehaviour, IBaseUI
    {
        public bool IsBlocked { get; protected set; }

        public virtual void Hide()
        {
        }

        public virtual void Show()
        {
        }

        public virtual void UpdateUI()
        {

        }
    }
}
