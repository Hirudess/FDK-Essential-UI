using UnityEngine;

namespace FDK.UI.Base
{
    public class BaseUIItemGroup : BaseUiItem
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void ShowCanvasGroup()
        {
            _canvasGroup.Show();
        }

        public void HideCanvasGroup()
        {
            _canvasGroup.Hide();
        }
    }
}
