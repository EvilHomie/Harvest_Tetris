using DI;
using UnityEngine;

namespace Inventory
{
    public class ItemSpawnerSystem : MonoBehaviour
    {
        [field: SerializeField] public Transform ItemHolder { get; private set; }

        private GameConfig _gameConfig;
        private ItemConfig _itemConfig;

        [Inject]
        public void Construct(GameConfig gameConfig, ItemConfig itemConfig)
        {
            _gameConfig = gameConfig;
            _itemConfig = itemConfig;
        }

        private void Start()
        {
            CreateStartItems();
        }

        public void ReturnItem(Item item)
        {
            item.RTransform.SetParent(ItemHolder);
        }

        private void CreateStartItems()
        {
            for (int i = 0; i < _gameConfig.StartItemsCount; i++)
            {
                ItemService.CreateRandomItem(_gameConfig, _itemConfig, ItemHolder);
            }
        }
    }
}