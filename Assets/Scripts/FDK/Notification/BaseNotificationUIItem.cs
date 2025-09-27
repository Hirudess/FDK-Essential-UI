using TMPro;
using UnityEngine;

namespace FDK.Notification
{
    public abstract class BaseNotificationUIItem : BaseUiItem
    {
        [SerializeField] protected CanvasGroup CanvasGroup;
        [SerializeField] private TMP_Text _notificationText;

        public virtual void ShowNotification(string text, int delay = 0)
        {
            _notificationText.SetText(text);
            Show();
        }

        public virtual void HideNotification()
        {
            Hide();
        }
    }
}
