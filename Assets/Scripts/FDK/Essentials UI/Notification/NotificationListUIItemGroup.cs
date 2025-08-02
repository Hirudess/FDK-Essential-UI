using FDK.UI.Base;

namespace FDK.Notification
{
    public class NotificationListUIItemGroup : BaseListUIItemGroup<GameNotificationUIItem>
    {
        public void ShowNotification(string notificationText)
        {
            var lifetime = 3 * 1000;

            var notificationUI = GetSpareNotificationUI();
            var needSpawnNewUI = notificationUI == null;

            if (needSpawnNewUI)
            {
                notificationUI = Instantiate(ItemPrefab, Content);
                Items.Add(notificationUI);
            }

            notificationUI.ShowNotification(notificationText, lifetime);
            notificationUI.RectTransform.SetAsFirstSibling();
        }

        private GameNotificationUIItem GetSpareNotificationUI()
        {
            foreach (var notification in Items)
            {
                if (notification.IsShowing) continue;

                return notification;
            }
            return null;
        }
    }
}
