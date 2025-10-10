using UnityEngine;
using UnityEngine.EventSystems;

namespace SystemHelper
{
    public class DragNDropHelper
    {
        public static bool IsLeftButton(PointerEventData eventData)
        {
            return eventData.button == PointerEventData.InputButton.Left;
        }
    }
}