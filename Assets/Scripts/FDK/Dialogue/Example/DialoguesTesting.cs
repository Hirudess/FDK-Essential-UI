using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace FDK.Dialogue
{
    public class DialoguesTesting : MonoBehaviour
    {
        [SerializeField]
        private DialogueGameData _dialogueGameData;
        [SerializeField]
        private Button _testDialogue;

        private IDialogueService _dialogueService;
        [Inject]
        public void Inject(IDialogueService dialogueService)
        {
            _dialogueService = dialogueService;
            _testDialogue.onClick.AddListener(TestDialogue);
        }

        private void TestDialogue()
        {
            _dialogueService.RegisterDialogue(_dialogueGameData);
            _dialogueService.PlayDialogue();
        }
    }
}
