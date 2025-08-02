using UnityEngine;

#nullable enable

namespace FDK.UI
{
    public static class CanvasGroupExtension
    {
        public static void Show(this CanvasGroup canvasGroup, bool isShown)
        {
            if (isShown)
            {
                canvasGroup.Show();
            }
            else
            {
                canvasGroup.Hide();
            }
        }

        public static void Show(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void Hide(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void SetInteractable(this CanvasGroup canvasGroup, bool enabled)
        {
            canvasGroup.interactable = enabled;
            canvasGroup.blocksRaycasts = enabled;
        }

        public static bool IsActive(this CanvasGroup canvasGroup)
        {
            return canvasGroup.alpha >= 1;
        }
        public static void ShowAsVisualOnly(this CanvasGroup canvasGroup, bool isShown)
        {
            canvasGroup.alpha = isShown ? 1 : 0;
        }
    }
}