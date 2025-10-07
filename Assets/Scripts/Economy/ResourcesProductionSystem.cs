using DI;
using Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Economy
{
    public class ResourcesProductionSystem : MonoBehaviour
    {
        public event Action<Item, int> ResourceCollect;
        public Dictionary<ResourceType, int> CollectedResources { get; private set; } = new();

        private InventorySystem _inventorySystem;
        private ResourcesPanel _resourcesPanel;
        private ItemProductionAnimService _itemAnimationService;
        private GameConfig _gameConfig;

        [Inject]
        public void Construct(InventorySystem inventorySystem, ResourcesPanel resourcesPanel, GameConfig gameConfig, ItemConfig itemConfig, Canvas canvas)
        {
            _inventorySystem = inventorySystem;
            _resourcesPanel = resourcesPanel;
            CollectedResources.Add(ResourceType.Iron, 0);
            CollectedResources.Add(ResourceType.Wheat, 0);
            CollectedResources.Add(ResourceType.Wood, 0);
            _itemAnimationService = new(itemConfig, canvas);
            _gameConfig = gameConfig;
        }

        private void Update()
        {
            _itemAnimationService.Tick(_inventorySystem.PlacedItems);

            if (_inventorySystem.PlacedItems.Count == 0)
            {
                return;
            }

            for (int i = _inventorySystem.PlacedItems.Count - 1; i >= 0; i--)
            {
                ProduceResources(_inventorySystem.PlacedItems[i]);
            }
        }

        public bool TrySpendResources(Cost[] Costs)
        {
            foreach (var cost in Costs)
            {
                if (CollectedResources[cost.ResourceType] < cost.Amount)
                {
                    return false;
                }
            }

            foreach (var cost in Costs)
            {
                CollectedResources[cost.ResourceType] -= cost.Amount;
            }

            foreach (var res in CollectedResources)
            {
                _resourcesPanel.UpdatePanel(res.Key, res.Value, false);
            }

            return true;
        }

        public void AddResources(ResourceType resourceType, int amount)
        {
            CollectedResources[resourceType] += amount;
            _resourcesPanel.UpdatePanel(resourceType, CollectedResources[resourceType], true);
        }

        private void ProduceResources(Item item)
        {
            var inventoryPivotCell = item.PivotCell;

            var tickmod = _gameConfig.ProductionSpeeds.First(data => data.TileModifier == inventoryPivotCell.TileModifier).Mod;
            var tickCollect = tickmod * Time.deltaTime;

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
            _itemAnimationService.AnimateCollection(item);
            AddResources(resourceType, amount);
            ResourceCollect?.Invoke(item, amount);
        }
    }
}