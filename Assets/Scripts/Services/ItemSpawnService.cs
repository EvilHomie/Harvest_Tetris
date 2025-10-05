using Inventory;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawnService
{
    private readonly GameConfig _gameConfig;
    private readonly ItemConfig _itemConfig;
    private readonly Transform _holder;

    public ItemSpawnService(GameConfig gameConfig, ItemConfig itemConfig, Transform holder)
    {
        _gameConfig = gameConfig;
        _itemConfig = itemConfig;
        _holder = holder;
    }
    public Item CreateRandomItem()
    {
        int randomIndex = Random.Range(0, _gameConfig.Items.Length);
        var newItem = GameObject.Instantiate(_gameConfig.Items[randomIndex], _holder);
        InitItem(newItem);
        ConfigureItem(newItem);
        return newItem;
    }

    private void InitItem(Item item)
    {
        item.RTransform = item.GetComponent<RectTransform>();
        item.GridLayoutGroup = item.GetComponent<GridLayoutGroup>();
        item.Cells = item.GetComponentsInChildren<ItemCell>();

        foreach (var cell in item.Cells)
        {
            cell.IsMainCell = false;
        }
    }

    private void ConfigureItem(Item item)
    {
        int randomCellIndex = Random.Range(0, item.Cells.Length);
        item.MainCell = item.Cells[randomCellIndex];
        item.MainCell.Image.color = Color.grey;
        item.MainCell.IsMainCell = true;

        int randomTypeIndex = Random.Range(0, _itemConfig.ItemTypeDatas.Length);
        ResourceType type = (ResourceType)randomTypeIndex;
        item.ResourceType = type;
        var prodView = GameObject.Instantiate(_itemConfig.ItemProdViewPF, item.MainCell.transform);
        item.ProductionView = prodView;
        prodView.ProdImage.sprite = _itemConfig.ItemTypeDatas.First(data => data.ResourceType == type).ResourceSprite;
        prodView.RTransform.anchorMin = Vector2.zero;
        prodView.RTransform.anchorMax = Vector2.one;
        prodView.RTransform.offsetMin = Vector2.zero;
        prodView.RTransform.offsetMax = Vector2.zero;
    }
}
