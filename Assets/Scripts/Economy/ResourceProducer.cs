using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ResourceProducer
    {
        private readonly List<(Item, GameResource)> _tickCollectData;
        private readonly GameConfig _gameConfig;

        public ResourceProducer(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _tickCollectData = new();
        }

        public List<(Item, GameResource)> ProductionTick(float tickTime, List<Item> inventoryItems)
        {
            _tickCollectData.Clear();

            if (inventoryItems.Count == 0)
            {
                return null;
            }

            for (int i = inventoryItems.Count - 1; i >= 0; i--)
            {
                ProduceResources(tickTime, inventoryItems[i]);
            }

            return _tickCollectData;
        }



        private void ProduceResources(float tickTime, Item item)
        {
            var inventoryCell = item.PivotCell;
            var prodMod = _gameConfig.ProductionModSet.GetMod(inventoryCell.TileModifier);

            if (prodMod == 0)
            {
                return;
            }

            var tickCollect = prodMod * tickTime;
            item.AmountOfCollectedResources += tickCollect;

            if (item.AmountOfCollectedResources > 1)
            {
                var amount = Mathf.FloorToInt(item.AmountOfCollectedResources);
                item.AmountOfCollectedResources -= amount;
                _tickCollectData.Add((item, new GameResource(item.ResourceType, amount)));
            }
        }
    }
}