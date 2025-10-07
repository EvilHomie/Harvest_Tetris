using UnityEngine;

namespace SystemHelper
{
    public class Utils
    {
        public static bool IsTargetOverElement(Transform target, RectTransform overElement, Camera camera)
        {
            Vector2 screenPoint = camera.WorldToScreenPoint(target.position);
            return RectTransformUtility.RectangleContainsScreenPoint(overElement, screenPoint, camera);
        }
    }
}

