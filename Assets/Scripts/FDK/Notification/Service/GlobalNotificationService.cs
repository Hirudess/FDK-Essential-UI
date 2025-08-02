using FDK.Core.Services;
using VContainer.Unity;

namespace FDK.Notification
{
    public interface IGlobalNotificationService
    {
        bool IsReady { get; }
        NotificationUIHandler Notification { get; }

        void RegisterNotification(NotificationUIHandler notificationUIHandler);
        void ShowNotification(string text);
    }

    public class GlobalNotificationService : BaseService, IGlobalNotificationService, IStartable
    {
        public NotificationUIHandler Notification { get; private set; }

        public void RegisterNotification(NotificationUIHandler notificationUIHandler)
        {
            Notification = notificationUIHandler;
            SetReady(true);
        }

        public void ShowNotification(string text)
        {
            if (!IsReady) return;
            Notification.ShowNotification(text);
        }

        public void Start()
        {
        }
    }
}
