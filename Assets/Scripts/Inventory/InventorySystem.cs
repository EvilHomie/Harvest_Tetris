using DI;
using System.Collections.Generic;
using SystemHelper;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : SystemBase
    {
        public InventoryGrid InventoryGrid { get; private set; }
        public List<Item> PlacedItems { get; private set; } = new();
        private Canvas _canvas;
        private Camera _camera;
        private InventoryConfig _inventoryConfig;
        private ItemSpawnSystem _itemSpawnSystem;

        [Inject]
        public void Construct(Canvas canvas, InventoryConfig inventoryConfig, Camera camera, ItemSpawnSystem itemSpawnSystem)
        {
            _canvas = canvas;
            _camera = camera;
            _inventoryConfig = inventoryConfig;
            _itemSpawnSystem = itemSpawnSystem;
        }

        protected override void Subscribe()
        {
            GameFlowSystem.CustomStart += CreateInventory;
        }

        protected override void UnSubscribe()
        {
            GameFlowSystem.CustomStart -= CreateInventory;
        }

        public bool TryPlaceItem(Item item)
        {
            if (!InventoryItemHandler.TryPlaceItem(item, InventoryGrid, _camera))
            {
                return false;
            }

            PlacedItems.Add(item);
            return true;
        }

        public void RemoveItem(Item item)
        {
            InventoryItemHandler.RemoveItem(item);
            _itemSpawnSystem.ReturnItem(item);
            PlacedItems.Remove(item);
        }

        private void CreateInventory()
        {
            InventoryGrid = InventoryFactory.CreateInventoryGrid(_inventoryConfig, _canvas.transform);
        }
    }
}