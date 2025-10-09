using Inventory;
using System.Linq;
using UnityEngine;

namespace SystemHelper
{
    public class ItemFactory
    {
        public static Item CreateRandomItem(ItemConfig itemConfig, GameConfig gameConfig)
        {
            int randomIndex = Random.Range(0, gameConfig.Items.Length);
            var newItem = GameObject.Instantiate(gameConfig.Items[randomIndex]);
            SetRandomCellAsMain(newItem);
            SetRandomType(newItem, itemConfig);
            return newItem;
        }

        private static void SetRandomCellAsMain(Item item)
        {
            foreach (var cell in item.Cells)
            {
                cell.Image.color = Color.blue;
                cell.IsMainCell = false;
            }

            int randomCellIndex = Random.Range(0, item.Cells.Length);
            item.MainCell = item.Cells[randomCellIndex];
            item.MainCell.Image.color = Color.grey;
            item.MainCell.IsMainCell = true;
        }

        private static void SetRandomType(Item item, ItemConfig itemConfig)
        {
            int randomTypeIndex = Random.Range(0, itemConfig.ResourceVisualSet.ResourceVisuals.Length);
            ResourceType type = (ResourceType)randomTypeIndex;
            item.ResourceType = type;
            var prodView = GameObject.Instantiate(itemConfig.ItemProdViewPF, item.MainCell.transform);
            item.ProductionView = prodView;
            prodView.ProdImage.sprite = itemConfig.ResourceVisualSet.GetSprite(type);
            prodView.RTransform.anchorMin = Vector2.zero;
            prodView.RTransform.anchorMax = Vector2.one;
            prodView.RTransform.offsetMin = Vector2.zero;
            prodView.RTransform.offsetMax = Vector2.zero;
        }
    }
}

