using FDK.Dialogue;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class DialogueSystemLifetimeScope : LifetimeScope
{
    [SerializeField]
    private DialogueManagerReference _dialogueManagerRef;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_dialogueManagerRef);
        builder.RegisterEntryPoint<DialogueManager>(Lifetime.Singleton).As<IDialogueManager>();
    }
}
