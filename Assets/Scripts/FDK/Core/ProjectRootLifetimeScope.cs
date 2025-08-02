using FDK.Dialogue;
using VContainer;
using VContainer.Unity;

public class ProjectRootLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<DialogueService>(Lifetime.Singleton).As<IDialogueService>();
    }
}
