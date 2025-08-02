using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FDK.Notification
{
    public class NotificationLifetimeScope : LifetimeScope
    {
        [SerializeField] private NotificationUIReference _notificationUIReference;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_notificationUIReference);
            builder.RegisterEntryPoint<NotificationUIHandler>(Lifetime.Singleton).As<INotificationUIHandler>();
        }
    }
}
