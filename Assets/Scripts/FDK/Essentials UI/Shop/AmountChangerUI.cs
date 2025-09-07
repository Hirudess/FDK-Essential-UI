using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.Shop
{
    public class AmountChangerUI : MonoBehaviour
    {
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private TMP_Text _amountText;

        public UnityEvent OnAmountChanged = new();

        private int _maxAmount;
        private bool _isInitialized = false;
        public int Amount { get; private set; }


        private void Awake()
        {
            _increaseButton.onClick.AddListener(Increase);
            _decreaseButton.onClick.AddListener(Decrease);
        }

        public void Initialize(int maxAmount)
        {
            _isInitialized = true;
            _maxAmount = maxAmount;
            Amount = 1;

            UpdateAmount();
        }

        public void Increase()
        {
            if (!_isInitialized) return;
            if (Amount + 1 > _maxAmount) return;
            Amount++;

            UpdateAmount();
        }

        public void Decrease()
        {
            if (!_isInitialized) return;
            if (Amount - 1 < 1) return;
            Amount--;

            UpdateAmount();
        }

        private void UpdateAmount()
        {
            _amountText.text = Amount.ToString();
            OnAmountChanged?.Invoke();
        }
    }
}
