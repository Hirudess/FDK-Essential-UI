using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace FDK.Dialogue
{
    [System.Serializable]
    public struct DialogueManagerReference
    {
        public DialogueGameData Dialogues;
        public DialogueBox DialogueBox;
    }

    public interface IDialogueManager
    {
        Queue<DialogueLineGameData> Dialogue { get; }

        void InitializeDialogue(IDialogueGameData dialogueGameData);
        void PlayDialogue();
    }

    public class DialogueManager : IDialogueManager, IStartable
    {
        private IDialogueService _dialogueService;
        private readonly DialogueBox _dialogueBox;

        public Queue<DialogueLineGameData> Dialogue { get; private set; }

        [Preserve]
        public DialogueManager(IDialogueService dialogueService, DialogueManagerReference dialogueManagerReference)
        {
            _dialogueBox = dialogueManagerReference.DialogueBox;
            _dialogueBox.OnNextButtonPressed.AddListener(NextDialogue);

            _dialogueService = dialogueService;
            _dialogueService.RegisterManager(this);
        }

        public void InitializeDialogue(IDialogueGameData dialogueGameData)
        {
            Dialogue = ConvertToQueue(dialogueGameData.Lines);
        }

        private Queue<DialogueLineGameData> ConvertToQueue(List<DialogueLineGameData> dialogueLines)
        {
            if (dialogueLines == null)
            {
                throw new ArgumentNullException(nameof(dialogueLines),
                    "Input dialogue lines list cannot be null");
            }

            var queue = new Queue<DialogueLineGameData>(dialogueLines.Count);
            foreach (var line in dialogueLines)
            {
                if (line != null)
                {
                    queue.Enqueue(line);
                }
            }
            return queue;
        }


        private void NextDialogue()
        {
            if (Dialogue.Count <= 0)
            {
                _dialogueBox.Hide();
                return;
            }

            Dialogue.Dequeue();
            PlayDialogue();
        }

        public void PlayDialogue()
        {
            if (Dialogue.Count <= 0) return;

            var currentDialogue = Dialogue.Peek();
            if (currentDialogue == null) return;

            _dialogueBox.PlayDialogue(currentDialogue);
        }

        public void Start()
        {

        }
    }
}
