using FDK.UI;

namespace FDK
{
    public abstract class BaseUiItem : BaseUi
    {
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
