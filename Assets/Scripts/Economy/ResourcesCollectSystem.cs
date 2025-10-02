using DI;
using Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourcesCollectSystem : MonoBehaviour
    {
        private InventoryGrid _inventoryGrid;
        private ResourcesPanel _resourcesPanel;
        private Dictionary<ResourceType, int> _resources = new();
        private Func<Item, int, int> _modificator;
        private List<Item> _pendingToDestroy = new();

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

            foreach (var item in _pendingToDestroy)
            {
                _inventoryGrid.RemoveItem(item);
                Destroy(item.gameObject);
            }

            _pendingToDestroy.Clear();
        }

        public void AplyArtifact(Artifact artifact)
        {
            CancelArtifactEffects();

            if (artifact == Artifact.WheatMultipler)
            {
                _modificator += WheatArtifact;
            }
            else if (artifact == Artifact.WheatWithWood)
            {
                _modificator += WoodArtifact;
            }
            else if (artifact == Artifact.IronOverload)
            {
                _modificator += IronArtifact;
            }
        }

        public void CancelArtifactEffects()
        {
            _modificator = null;
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

            if (_modificator != null)
            {
                amount = _modificator.Invoke(item, amount);
            }

            _resources[resourceType] += amount;
            _resourcesPanel.UpdatePanel(resourceType, _resources[resourceType]);
        }

        private int WheatArtifact(Item item, int deffAmount)
        {
            if (item.ResourceType != ResourceType.Wheat)
            {
                return deffAmount;
            }

            int cellsCount = item.Cells.Length;
            return deffAmount * cellsCount;
        }

        private int WoodArtifact(Item item, int deffAmount)
        {
            if (item.ResourceType != ResourceType.Wood)
            {
                return deffAmount;
            }

            bool result = UnityEngine.Random.value < 0.5f;

            if (result)
            {
                _resources[ResourceType.Wheat] += deffAmount;
                _resourcesPanel.UpdatePanel(ResourceType.Wheat, _resources[ResourceType.Wheat]);
            }

            return deffAmount;
        }

        private int IronArtifact(Item item, int deffAmount)
        {
            if (item.ResourceType != ResourceType.Iron)
            {
                return deffAmount;
            }

            deffAmount *= 10;
            bool result = UnityEngine.Random.value < 0.1f;

            if (result)
            {
                _pendingToDestroy.Add(item);
            }

            return deffAmount;
        }
    }
}