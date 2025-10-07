using DI;
using SystemHelper;
using UnityEngine;

namespace Inventory
{
    public class ItemSpawnSystem : MonoBehaviour
    {
        [field: SerializeField] public Transform ItemHolder { get; private set; }
        private GameConfig _gameConfig;
        private ItemConfig _itemConfig;
        private InventoryConfig _inventoryConfig;

        [Inject]
        public void Construct(GameConfig gameConfig, ItemConfig itemConfig, InventoryConfig inventoryConfig)
        {
            _gameConfig = gameConfig;
            _itemConfig = itemConfig;
            _inventoryConfig = inventoryConfig;
        }

        private void Start()
        {
            CreateStartItems();
        }

        public void ReturnItem(Item item)
        {
            item.RTransform.SetParent(ItemHolder);
        }

        public void CreateItem()
        {
            var newItem = ItemFactory.CreateRandomItem(_itemConfig, _gameConfig);
            Utils.AdaptItemToInventory(newItem, _inventoryConfig);
            newItem.RTransform.SetParent(ItemHolder, false);
        }

        private void CreateStartItems()
        {
            for (int i = 0; i < _gameConfig.StartItemsCount; i++)
            {
                CreateItem();
            }
        }
    }
}