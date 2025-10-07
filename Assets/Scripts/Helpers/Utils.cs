using Inventory;
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

        public static void AdaptItemToInventory(Item item, InventoryConfig inventoryConfig)
        {
            item.GridLayoutGroup.cellSize = Vector2.one * inventoryConfig.CellSize;
            item.GridLayoutGroup.spacing = Vector2.one * inventoryConfig.SpaceSize;
            float singlePadding = -inventoryConfig.SpaceSize / 2;
            Vector4 totalPadding = Vector4.one * singlePadding; 

            foreach (var cell in item.Cells)
            {
                cell.Image.raycastPadding = totalPadding; // необходимо для работы кликов в пустоты между клетками
            }
        }
    }
}