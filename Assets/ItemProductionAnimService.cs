using Inventory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemProductionAnimService
{
    private readonly List<CollectPopup> _collectPopups = new();
    private readonly ItemConfig _itemConfig;
    private readonly Canvas _canvas;

    public ItemProductionAnimService(ItemConfig itemConfig, Canvas canvas)
    {
        _itemConfig = itemConfig;
        _canvas = canvas;
    }

    public void Tick(List<Item> items)
    {
        foreach (Item item in items)
        {
            AnimateProduction(item);
        }

        var floatSpeed = _itemConfig.PopupFloatSpeed * Time.deltaTime * Vector3.up;

        for (int i = _collectPopups.Count - 1; i >= 0; i--)
        {
            var popup = _collectPopups[i];
            popup.ShowTime -= Time.deltaTime;
            popup.transform.position += floatSpeed;

            if (popup.ShowTime <= 0)
            {
                _collectPopups.Remove(popup);
                GameObject.Destroy(popup.gameObject);
            }
        }
    }

    public void AnimateCollection(Item item)
    {
        var popup = GameObject.Instantiate(_itemConfig.CollectPopupPF, item.MainCell.transform.position, Quaternion.identity, _canvas.transform);
        popup.ShowTime = _itemConfig.PopupShowTime;
        popup.ProdImage.sprite = _itemConfig.ItemTypeDatas.First(data => data.ResourceType == item.ResourceType).ResourceSprite;
        _collectPopups.Add(popup);
    }


    private void AnimateProduction(Item item)
    {
        float angle = item.AmountOfCollectedResources * 360;
        item.ProductionView.ProdImage.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}