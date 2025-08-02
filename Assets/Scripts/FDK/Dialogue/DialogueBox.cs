using Cysharp.Threading.Tasks;
using FDK.UI.Base;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.Dialogue
{
    public interface IDialogueBox
    {
        void PlayDialogue(IDialogueLine dialogueLine);
    }

    public class DialogueBox : BaseUIItemGroup, IDialogueBox
    {
        [SerializeField]
        private float _charactersPerSecond = 30f;
        [SerializeField]
        private float _punctuationDelayMultiplier = 3f;
        [SerializeField]
        private Image _portrait;
        [SerializeField]
        private TMP_Text _speakerName;
        [SerializeField]
        private TMP_Text _speakerDialogue;
        [SerializeField]
        private Button _nextButton;

        public UnityEvent OnNextButtonPressed;

        private CancellationTokenSource _cancellationTokenSource;
        private string _currentText;
        private bool _isTyping;

        private void Awake()
        {
            _nextButton.onClick.AddListener(Next);
        }

        public void PlayDialogue(IDialogueLine dialogueLine)
        {
            ShowCanvasGroup();

            _speakerName.text = dialogueLine.Name;

            if (dialogueLine.Portrait != null)
            {
                _portrait.gameObject.SetActive(true);
                _portrait.sprite = dialogueLine.Portrait;
            }
            else
            {
                _portrait.gameObject.SetActive(false);
            }
            TypeTextAsync(dialogueLine.Dialogue).Forget();

        }

        private void Next()
        {
            if (_isTyping)
            {
                SkipTyping();
            }
            else
            {
                OnNextButtonPressed?.Invoke();
            }
        }

        public async UniTask TypeTextAsync(string text)
        {
            CancelCurrentTyping();

            _currentText = text;
            _isTyping = true;
            _speakerDialogue.text = string.Empty;
            _cancellationTokenSource = new CancellationTokenSource();

            for (int i = 0; i < text.Length; i++)
            {
                if (_cancellationTokenSource.IsCancellationRequested) break;

                _speakerDialogue.text += text[i];
                await UniTask.Delay(GetDelayDuration(text[i]), cancellationToken: _cancellationTokenSource.Token);
            }

            _isTyping = false;
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

        }

        public void SkipTyping()
        {
            if (_isTyping)
            {
                _speakerDialogue.text = _currentText;
                CancelCurrentTyping();
            }
        }

        private void CancelCurrentTyping()
        {
            _isTyping = false;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private int GetDelayDuration(char character)
        {
            float baseDelay = 1000 / _charactersPerSecond;

            if (char.IsPunctuation(character) && character != '\'' && character != '"')
            {
                return Mathf.RoundToInt(baseDelay * _punctuationDelayMultiplier);
            }

            return Mathf.RoundToInt(baseDelay);
        }
    }
}
