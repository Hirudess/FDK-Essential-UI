using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace FDK.Dialogue
{
    public class DialogueSystemInstaller : IInstaller
    {
        [SerializeField]
        private DialogueManagerReference _dialogueManagerRef;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_dialogueManagerRef);
            builder.RegisterEntryPoint<DialogueManager>(Lifetime.Singleton).As<IDialogueManager>();
        }
    }
}
