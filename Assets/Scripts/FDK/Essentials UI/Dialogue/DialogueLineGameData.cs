using UnityEngine;

namespace FDK.Dialogue
{
    public interface IDialogueLine
    {
        string Name { get; }
        string Dialogue { get; }
        Sprite Portrait { get; }
    }

    [System.Serializable]
    public class DialogueLineGameData : IDialogueLine
    {
        [SerializeField] private string _name;
        [SerializeField] private string _dialogue;
        [SerializeField] private Sprite _portrait;

        public string Name => _name;
        public string Dialogue => _dialogue;
        public Sprite Portrait => _portrait;
    }
}
