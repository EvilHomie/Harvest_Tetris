using DI;
using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourcesCollectSystem : MonoBehaviour
    {
        private InventoryGrid _inventoryGrid;
        private ResourcesPanel _resourcesPanel;
        private Dictionary<ResourceType, int> _resources = new();

        [Inject]
        public void Construct(InventoryGrid inventoryGrid, ResourcesPanel resourcesPanel)
        {
            _inventoryGrid = inventoryGrid;
            _resourcesPanel = resourcesPanel;
            _resources.Add(ResourceType.Iron, 0);
            _resources.Add(ResourceType.Wheat, 0);
            _resources.Add(ResourceType.Wood, 0);
        }

        private void Update()
        {
            foreach (var item in _inventoryGrid.ItemsInside)
            {
                ProduceResources(item);
            }
        }

        private void ProduceResources(Item item)
        {
            var inventoryPivotCell = item.PivotCell;

            var tickCollect = inventoryPivotCell.TileModifier switch
            {
                TileModifier.Green => 1f * Time.deltaTime,
                TileModifier.Yellow => 0.4f * Time.deltaTime,
                _ => 0f
            };

            if (tickCollect == 0)
            {
                return;
            }

            item.AmountOfCollectedResources += tickCollect;

            if (item.AmountOfCollectedResources > 1)
            {
                var amount = Mathf.FloorToInt(item.AmountOfCollectedResources);
                item.AmountOfCollectedResources -= amount;
                CollectionTick(item, amount);
            }
        }

        private void CollectionTick(Item item, int amount)
        {
            ResourceType resourceType = item.ResourceType;
            _resources[resourceType] += amount;
            _resourcesPanel.UpdatePanel(resourceType, _resources[resourceType]);
        }
    }
}
