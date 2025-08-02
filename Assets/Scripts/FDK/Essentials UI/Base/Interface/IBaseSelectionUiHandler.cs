using System.Collections.Generic;
using FDK.UI;

namespace FDK
{
    public interface IBaseSelectionUiHandler<T, U> where T : IBaseUiItemData where U : IBaseSelectableUiItem
    {
        T SelectedData { get; }
        List<U> Items { get; }
        void SelectItem(T item);
    }
}
