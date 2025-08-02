using FDK.Core.Services;
using VContainer.Unity;

namespace FDK.Dialogue
{
    public interface IDialogueService
    {
        void PlayDialogue();
        void RegisterDialogue(IDialogueGameData dialogueGameData);
        void RegisterManager(IDialogueManager dialogueManager);
        void Start();
        void UnregisterManager();
    }

    public class DialogueService : BaseService, IStartable, IDialogueService
    {
        private IDialogueManager _dialogueManager;

        public void RegisterManager(IDialogueManager dialogueManager)
        {
            _dialogueManager = dialogueManager;
            SetReady(true);
        }

        public void UnregisterManager()
        {
            _dialogueManager = null;
            SetReady(true);
        }

        public void RegisterDialogue(IDialogueGameData dialogueGameData)
        {
            if (!IsReady) { return; }
            _dialogueManager.InitializeDialogue(dialogueGameData);
        }

        public void PlayDialogue()
        {
            if (!IsReady) { return; }
            _dialogueManager.PlayDialogue();
        }


        public void Start()
        {

        }
    }
}
