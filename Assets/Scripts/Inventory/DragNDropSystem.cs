using DI;
using Inventory;
using System;
using SystemHelper;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragNDropSystem : SystemBase
{
    public static Action<Item, PointerEventData> OnBeginDragGlobal;
    public static Action<Item, PointerEventData> OnDragGlobal;
    public static Action<Item, PointerEventData> OnEndDragGlobal;

    private Canvas _canvas;
    private Item _item;
    private bool _isDragging;
    private Camera _camera;
    private InventorySystem _inventorySystem;
    private DestroyItemSystem _destroyItemSystem;
    private ItemSpawnSystem _itemSpawnSystem;

    [Inject]
    public void Construct(InventorySystem inventorySystem, Canvas canvas, Camera camera, DestroyItemSystem destroyItemSystem, ItemSpawnSystem itemSpawnSystem)
    {
        _inventorySystem = inventorySystem;
        _canvas = canvas;
        _camera = camera;
        _destroyItemSystem = destroyItemSystem;
        _itemSpawnSystem = itemSpawnSystem;
    }

    protected override void Subscribe()
    {
        OnBeginDragGlobal += OnBeginDrag;
        OnDragGlobal += OnDrag;
        OnEndDragGlobal += OnEndDrag;
    }

    protected override void UnSubscribe()
    {
        OnBeginDragGlobal -= OnBeginDrag;
        OnDragGlobal -= OnDrag;
        OnEndDragGlobal -= OnEndDrag;
    }

    private void Update()
    {
        if (_isDragging && Mouse.current.rightButton.wasPressedThisFrame)
        {
            _item.RTransform.Rotate(0, 0, -90);
        }
    }

    public void OnBeginDrag(Item item, PointerEventData eventData)
    {
        if (!DragNDropHelper.IsLeftButton(eventData)) return;

        _isDragging = true;
        _item = item;

        if (_item.IsInInventory)
        {
            _inventorySystem.RemoveItem(_item);
        }

        item.RTransform.SetParent(_canvas.transform);
        item.RTransform.SetAsLastSibling();
    }

    public void OnDrag(Item item, PointerEventData eventData)
    {
        if (!DragNDropHelper.IsLeftButton(eventData)) return;

        item.RTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(Item item, PointerEventData eventData)
    {
        if (!DragNDropHelper.IsLeftButton(eventData)) return;

        _isDragging = false;
        _item = null;

        if (Utils.IsTargetOverElement(item.RTransform, _destroyItemSystem.DestroyItemArea.RTransform, _camera))
        {
            if (_destroyItemSystem.TryDestroyItem(item))
            {
                return;
            }
        }

        if (Utils.IsTargetOverElement(item.RTransform, _inventorySystem.InventoryGrid.RTransform, _camera))
        {
            if (_inventorySystem.TryPlaceItem(item))
            {
                return;
            }
        }

        _itemSpawnSystem.ReturnItem(item);
    }
}