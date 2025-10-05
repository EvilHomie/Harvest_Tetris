using Inventory;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemService
{
    public static Item CreateRandomItem(GameConfig gameConfig, ItemConfig itemConfig, Transform holder)
    {
        int randomIndex = Random.Range(0, gameConfig.Items.Length);
        var newItem = GameObject.Instantiate(gameConfig.Items[randomIndex], holder);
        InitItem(newItem);
        ConfigureItem(newItem, itemConfig);
        return newItem;
    }

    private static void InitItem(Item item)
    {
        item.RTransform = item.GetComponent<RectTransform>();
        item.GridLayoutGroup = item.GetComponent<GridLayoutGroup>();
        item.Cells = item.GetComponentsInChildren<ItemCell>();

        foreach (var cell in item.Cells)
        {
            cell.IsMainCell = false;
        }
    }

    private static void ConfigureItem(Item item, ItemConfig itemConfig)
    {
        int randomCellIndex = Random.Range(0, item.Cells.Length);
        item.MainCell = item.Cells[randomCellIndex];
        item.MainCell.Image.color = Color.grey;
        item.MainCell.IsMainCell = true;

        int randomTypeIndex = Random.Range(0, itemConfig.ItemTypeDatas.Length);
        ResourceType type = (ResourceType)randomTypeIndex;
        item.ResourceType = type;
        var prodView = GameObject.Instantiate(itemConfig.ItemProdViewPF, item.MainCell.transform);
        prodView.ProdImage.sprite = itemConfig.ItemTypeDatas.First(data => data.ResourceType == type).ResourceSprite;
        prodView.RTransform.anchorMin = Vector2.zero;
        prodView.RTransform.anchorMax = Vector2.one;
        prodView.RTransform.offsetMin = Vector2.zero;
        prodView.RTransform.offsetMax = Vector2.zero;
    }
}
