using DI;
using Inventory;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragNDropSystem : MonoBehaviour
{
    public static Action<Item, PointerEventData> OnBeginDragGlobal;
    public static Action<Item, PointerEventData> OnDragGlobal;
    public static Action<Item, PointerEventData> OnEndDragGlobal;

    private InventoryGrid _inventoryGrid;
    private ItemSpawnerSystem _spawnSystem;
    private Canvas _canvas;
    private Item _item;
    private bool _isDragging;

    [Inject]
    public void Construct(InventoryGrid inventoryGrid, ItemSpawnerSystem spawnSystem, Canvas canvas)
    {
        _inventoryGrid = inventoryGrid;
        _spawnSystem = spawnSystem;
        _canvas = canvas;
    }

    private void OnEnable()
    {
        OnBeginDragGlobal += OnBeginDrag;
        OnDragGlobal += OnDrag;
        OnEndDragGlobal += OnEndDrag;
    }
    private void OnDisable()
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
        if (!IsLeftButton(eventData)) return;

        _isDragging = true;
        _item = item;

        if (_item.IsInInventory)
        {
            _inventoryGrid.RemoveItem(_item);
        }

        item.RTransform.SetParent(_canvas.transform);
        item.RTransform.SetAsLastSibling();
    }

    public void OnDrag(Item item, PointerEventData eventData)
    {
        if (!IsLeftButton(eventData)) return;

        item.RTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(Item item, PointerEventData eventData)
    {
        if (!IsLeftButton(eventData)) return;

        _isDragging = false;
        _item = null;

        if (!_inventoryGrid.TryPlaceItem(item))
        {
            _spawnSystem.ReturnItem(item);
        }
    }

    private bool IsLeftButton(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Left;
    }
}
