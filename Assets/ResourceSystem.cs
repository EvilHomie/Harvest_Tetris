using DI;
using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourceSystem : SystemBase
    {
        [SerializeField] ResourcesPanel _resourcesPanel;
        private ResourceProducer _resourceProducer;
        private ResourceStorage _resourceStorage;
        private ProductionAnimator _productionAnimator;

        private InventorySystem _inventorySystem;
        private GameConfig _gameConfig;
        private ItemConfig _itemConfig;
        private Canvas _canvas;

        private List<(Item, GameResource)> _tickCollectData;

        [Inject]
        public void Construct(InventorySystem inventorySystem, GameConfig gameConfig, ItemConfig itemConfig, Canvas canvas)
        {
            _inventorySystem = inventorySystem;
            _productionAnimator = new(itemConfig, canvas);
            _gameConfig = gameConfig;
            _itemConfig = itemConfig;
            _canvas = canvas;
        }

        protected override void Subscribe()
        {
            GameFlowSystem.CustomStart += Init;
            GameFlowSystem.CustomUpdate += ProductionTick;
        }

        protected override void UnSubscribe()
        {
            GameFlowSystem.CustomStart -= Init;
            GameFlowSystem.CustomUpdate -= ProductionTick;
        }

        private void Init()
        {
            _resourceProducer = new(_gameConfig);
            _resourceStorage = new();
            _productionAnimator = new(_itemConfig, _canvas);
        }

        public void Add(GameResource resource)
        {
            _resourceStorage.Add(resource);
        }

        public bool TryConsume(GameResource resource)
        {
            if (_resourceStorage.TryConsume(resource))
            {
                var totalAmount = _resourceStorage.GetAmount(resource.Type);
                _resourcesPanel.UpdatePanel(resource.Type, totalAmount, false);
                return true;
            }

            return false;
        }

        public bool TryConsume(GameResource[] resources)
        {
            if (!_resourceStorage.TryConsume(resources))
            {
                return false;
            }

            foreach (var resource in resources)
            {
                var totalAmount = _resourceStorage.GetAmount(resource.Type);
                _resourcesPanel.UpdatePanel(resource.Type, totalAmount, false);
            }

            return true;
        }

        private void ProductionTick(float tickTime)
        {
            _tickCollectData = _resourceProducer.ProductionTick(tickTime, _inventorySystem.PlacedItems);

            if (_tickCollectData != null && _tickCollectData.Count > 0)
            {
                OnCollect(_tickCollectData);
            }
            _productionAnimator.AnimationTick(tickTime, _inventorySystem.PlacedItems);
        }

        private void OnCollect(List<(Item, GameResource)> collectData)
        {
            ShowCollectPopups(collectData);

            foreach (var (_, resource) in collectData)
            {
                _resourceStorage.Add(resource);
                var totalAmount = _resourceStorage.GetAmount(resource.Type);
                _resourcesPanel.UpdatePanel(resource.Type, totalAmount, true);
            }
        }

        private void ShowCollectPopups(List<(Item, GameResource)> collectData)
        {
            foreach (var (item, resource) in collectData)
            {
                _productionAnimator.ShowCollectPopup(item, resource);
            }
        }
    }
}