using Inventory;
using System.Collections.Generic;
using UnityEngine;

public class ProductionAnimator
{
    private readonly List<CollectPopup> _collectPopups = new();
    private readonly ItemConfig _itemConfig;
    private readonly Canvas _canvas;

    public ProductionAnimator(ItemConfig itemConfig, Canvas canvas)
    {
        _itemConfig = itemConfig;
        _canvas = canvas;
    }

    public void AnimationTick(float tickTime, List<Item> items)
    {
        foreach (Item item in items)
        {
            AnimateProduction(item);
        }

        if (_collectPopups.Count > 0)
        {
            AnimateCollectPopups(tickTime);
        }
    }

    public void OnResourceProduced(Item item, GameResource resource)
    {
        var popup = GameObject.Instantiate(_itemConfig.CollectPopupPF, item.MainCell.transform.position, Quaternion.identity, _canvas.transform);
        popup.ShowTime = _itemConfig.PopupShowTime;
        popup.ProdImage.sprite = _itemConfig.ResourceVisualSet.GetSprite(resource.Type);
        popup.AmountText.text = $"+{resource.Amount}";
        _collectPopups.Add(popup);
    }

    private void AnimateProduction(Item item)
    {
        float angle = item.AmountOfCollectedResources * 360;
        item.ProductionView.ProdImage.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void AnimateCollectPopups(float tickTime)
    {
        var floatSpeed = _itemConfig.PopupFloatSpeed * tickTime * Vector3.up;

        for (int i = _collectPopups.Count - 1; i >= 0; i--)
        {
            var popup = _collectPopups[i];
            popup.ShowTime -= tickTime;
            popup.transform.position += floatSpeed;

            if (popup.ShowTime <= 0)
            {
                _collectPopups.Remove(popup);
                GameObject.Destroy(popup.gameObject);
            }
        }
    }
}