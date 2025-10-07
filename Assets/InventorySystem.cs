using DI;
using Inventory;
using System.Collections.Generic;
using SystemHelper;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public InventoryGrid InventoryGrid { get; private set; }

    private Canvas _canvas;
    private Camera _camera;
    private InventoryConfig _inventoryConfig;
    private ItemSpawnerSystem _spawnerSystem;

    public List<Item> PlacedItems { get; private set; } = new();


    [Inject]
    public void Construct(Canvas canvas, InventoryConfig inventoryConfig, Camera camera, ItemSpawnerSystem spawnerSystem)
    {
        _canvas = canvas;
        _camera = camera;
        _inventoryConfig = inventoryConfig;
        _spawnerSystem  = spawnerSystem;
    }

    private void Start()
    {
        InventoryGrid = InventoryFactory.CreateInventoryGrid(_inventoryConfig, _canvas.transform);
    }

    public void TryPlaceItem(Item item)
    {
        if (!InventoryItemHandler.TryPlaceItem(item, InventoryGrid, _camera))
        {
            _spawnerSystem.ReturnItem(item);
            return;
        }

        PlacedItems.Add(item);
    }

    public void RemoveItem(Item item)
    {
        InventoryItemHandler.RemoveItem(item);
        _spawnerSystem.ReturnItem(item);
        PlacedItems.Remove(item);
    }
}
