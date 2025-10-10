using DI;
using Economy;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using Item = Inventory.Item;

namespace Economy
{
    public class ResourceSystem : SystemBase
    {
        private ResourceStorage _resourceStorage;
        private ResourcesPanel _resourcesPanel;
        private ResourceProducer _resourceProducer;
        private ProductionAnimator _productionAnimator;

        private InventorySystem _inventorySystem;
        private GameConfig _gameConfig;
        private ItemConfig _itemConfig;
        RelicSystem _relicSystem;
        private Canvas _canvas;

        private List<(Item item, GameResource resource)> _tickCollectData;

        [Inject]
        public void Construct(InventorySystem inventorySystem, GameConfig gameConfig, ItemConfig itemConfig, Canvas canvas, ResourcesPanel resourcesPanel, RelicSystem relicSystem)
        {
            _inventorySystem = inventorySystem;
            _productionAnimator = new(itemConfig, canvas);
            _gameConfig = gameConfig;
            _itemConfig = itemConfig;
            _canvas = canvas;
            _resourcesPanel = resourcesPanel;
            _relicSystem = relicSystem;
        }

        protected override void Subscribe()
        {
            GameFlowSystem.CustomStart += Init;
            GameFlowSystem.CustomUpdate += ProductionTick;
            GameFlowSystem.CustomUpdate += _resourcesPanel.UpdateTimers;
        }

        protected override void UnSubscribe()
        {
            GameFlowSystem.CustomStart -= Init;
            GameFlowSystem.CustomUpdate -= ProductionTick;
            GameFlowSystem.CustomUpdate -= _resourcesPanel.UpdateTimers;
        }

        private void Init()
        {
            _resourceProducer = new(_gameConfig);
            _resourceStorage = new();
            _productionAnimator = new(_itemConfig, _canvas);
            _resourcesPanel.Init();
        }

        public void AddResource(GameResource resource)
        {
            _resourceStorage.Add(resource);
            UpdatePanel(resource.Type, true);
        }

        public bool TryConsume(GameResource resource)
        {
            if (!_resourceStorage.HasEnoughResources(resource))
            {
                return false;
            }

            _resourceStorage.Consume(resource);
            UpdatePanel(resource.Type, false);

            return true;
        }

        public bool TryConsume(GameResource[] resources)
        {
            if (!_resourceStorage.HasEnoughResources(resources))
            {
                return false;
            }

            _resourceStorage.Consume(resources);

            foreach (var resource in resources)
            {
                UpdatePanel(resource.Type, false);
            }

            return true;
        }
        private void ProductionTick(float tickTime)
        {
            _tickCollectData = _resourceProducer.ProductionTick(tickTime, _inventorySystem.PlacedItems);

            if (_tickCollectData != null && _tickCollectData.Count > 0)
            {
                OnResourceProduced(_tickCollectData);
            }

            _productionAnimator.AnimationTick(tickTime, _inventorySystem.PlacedItems);
        }

        private void OnResourceProduced(List<(Item item, GameResource resource)> collectData)
        {
            foreach (var (item, resource) in collectData)
            {
                var prodContext = new ResourceProductionContext(item, resource, _resourceStorage, _inventorySystem.PlacedItems.Count);
                _relicSystem.ApplyEffects(ref prodContext);
                _productionAnimator.OnResourceProduced(prodContext.ProductionItem, prodContext.ProducedResource);
                AddResources(prodContext);
            }

        }

        private void AddResources(ResourceProductionContext productionContext)
        {
            _resourceStorage.Add(productionContext.ProducedResource);
            UpdatePanel(productionContext.ProducedResource.Type, true);

            foreach (var bonusRes in productionContext.BonusResources)
            {
                _resourceStorage.Add(bonusRes);
                UpdatePanel(bonusRes.Type, true);
            }
        }

        private void UpdatePanel(ResourceType resourceType, bool isAdd)
        {
            var totalAmount = _resourceStorage.GetAmount(resourceType);
            _resourcesPanel.UpdatePanel(resourceType, totalAmount, isAdd);
        }
    }
}

public struct ResourceProductionContext
{
    public Item ProductionItem;
    public GameResource ProducedResource;
    public List<GameResource> BonusResources;
    public ResourceStorage ResourceStorage;
    public int InventoryItemsCount;

    public ResourceProductionContext(Item productionItem, GameResource producedResource, ResourceStorage resourceStorage, int inventoryItemsCount)
    {
        ProductionItem = productionItem;
        ProducedResource = producedResource;
        BonusResources = new();
        ResourceStorage = resourceStorage;
        InventoryItemsCount = inventoryItemsCount;
    }

    public readonly void AddBonusResource(ResourceType type, int amount)
    {
        int index = BonusResources.FindIndex(res => res.Type == type);

        if (index >= 0)
        {
            BonusResources[index].Add(amount);
        }
        else
        {
            var res = new GameResource(type, amount);
            BonusResources.Add(res);
        }
    }
}