using FDK.UI;

namespace FDK
{
    public interface IBaseSelectableUiItem : IBaseUI
    {
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }
}
