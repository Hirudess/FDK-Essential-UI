using VContainer;
using VContainer.Unity;

namespace FDK.Notification
{
    public interface INotificationUIHandler
    {
        void ShowNotification(string message);
    }
    [System.Serializable]
    public struct NotificationUIReference
    {
        public NotificationListUIItemGroup NotificationUIHandler;
    }

    public class NotificationUIHandler : IStartable, INotificationUIHandler 
    {
        private readonly NotificationListUIItemGroup _notificationListUIItem;

        [Preserve]
        public NotificationUIHandler(NotificationUIReference reference, IGlobalNotificationService globalNotificationService)
        {
            _notificationListUIItem = reference.NotificationUIHandler;
            globalNotificationService.RegisterNotification(this);
        }

        public void ShowNotification(string message)
        {
            _notificationListUIItem?.ShowNotification(message);
        }

        public void Start()
        {
        }
    }
}
