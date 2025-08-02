using Cysharp.Threading.Tasks;
using FDK.UI;
using UnityEngine;

namespace FDK.Notification
{
    public class GameNotificationUIItem : BaseNotificationUIItem
    {
        private const float _fadeSpeed = 1f;

        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
        public bool IsShowing { get; set; }

        public override void ShowNotification(string text, int delay)
        {
            ShowUIThenFadeAsync(text, delay).Forget();
        }

        private async UniTask ShowUIThenFadeAsync(string text, int delay)
        {
            IsShowing = true;
            base.ShowNotification(text);

            await UniTask.Delay(delay);
            Hide();
        }

        public override void Show()
        {
            base.Show();
            CanvasGroup.Show();
        }

        public override void Hide()
        {
            //CanvasGroup.DOFade(0, _fadeSpeed).onComplete = () =>
            //{
            //    base.Hide();
            //    CanvasGroup.Hide();
            //    IsShowing = false;
            //};
        }
    }
}
