namespace FDK.UI
{
    public interface IBaseUI
    {
        bool IsBlocked { get; }
        void Show();
        void Hide();
        void UpdateUI();
    }
}
