using FDK.Core;
using FDK.Dialogue;
using FDK.Notification;
using VContainer;
using VContainer.Unity;

public class ProjectRootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CurrencySystem>(Lifetime.Singleton).As<ICurrencySystem>();

        builder.RegisterEntryPoint<DialogueService>(Lifetime.Singleton).As<IDialogueService>();
        builder.RegisterEntryPoint<GlobalNotificationService>(Lifetime.Singleton).As<IGlobalNotificationService>();
    }
}
