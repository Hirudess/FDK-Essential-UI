using FDK.UI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace FDK.Shop
{
    public class ShopConfirmationUIPanel : BaseUIPanel
    {
        [SerializeField] private TMP_Text _dialogue;
        [SerializeField] private Image _sellerPortrait;
        [SerializeField] private AmountChangerUI _amountChanger;
        [SerializeField] private TMP_Text _expense;
        [SerializeField] private Button _proceedButton;
        [SerializeField] private Button _returnButton;

        public ShopProductUIData ShopProductUIData { get; private set; }

        public UnityEvent<string, int> OnProceedEvent = new();

        private void Awake()
        {
            _returnButton.onClick.AddListener(OnReturn);
            _proceedButton.onClick.AddListener(OnProceed);
            _amountChanger.OnAmountChanged.AddListener(UpdateUI);
        }

        private void OnReturn()
        {
            Hide();
        }

        private void OnProceed()
        {
            if (ShopProductUIData == null) return;
            OnProceedEvent.Invoke(ShopProductUIData.Id, _amountChanger.Amount);
        }

        public void Initialize(ShopProductUIData shopProductUIData)
        {
            ShopProductUIData = shopProductUIData;

            _amountChanger.Initialize(30);
            UpdateDialogue(ShopProductUIData.Name, _amountChanger.Amount, shopProductUIData.Price);
            Show();
        }

        public override void UpdateUI()
        {
            if (ShopProductUIData == null) return;
            if (_amountChanger == null) return;
            var amount = _amountChanger.Amount;

            UpdateDialogue(ShopProductUIData.Name, amount, ShopProductUIData.Price);
        }

        private void UpdateDialogue(string product, int amount, int pricePerItem)
        {
            var totalExpense = pricePerItem * amount;

            _dialogue.text = $"How many {product} do you want to buy?";
            _expense.text = $"Expense {totalExpense}";
        }
    }
}
