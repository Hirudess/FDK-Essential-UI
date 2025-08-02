using UnityEngine;
using VContainer;

namespace FDK.Dialogue
{
    public interface IDialogueEventHandler
    {

    }

    public class TestDialogueEventHandler : MonoBehaviour
    {
        private DialogueGameData _testDialogue;
        private IDialogueManager _dialogueManager;

        [Inject]
        public void Inject(IDialogueManager dialogueManager, DialogueManagerReference dialogueManagerReference)
        {
            _dialogueManager = dialogueManager;
            _testDialogue = dialogueManagerReference.Dialogues;
            _dialogueManager.InitializeDialogue(_testDialogue);
            _dialogueManager.PlayDialogue();
        }
    }
}
