using System.Collections.Generic;
using UnityEngine;

namespace FDK.Dialogue
{
    public interface IDialogueGameData
    {
        string Id { get; }
        List<DialogueLineGameData> Lines { get; }
    }

    [CreateAssetMenu(fileName = "Dialogue", menuName = "FDK/Dialogue/Dialogues")]
    public class DialogueGameData : BasePreset, IDialogueGameData
    {
        [SerializeField] private string _id;
        [SerializeField] private List<DialogueLineGameData> _lines;

        public List<DialogueLineGameData> Lines => _lines;
        public string Id => _id;
    }
}
