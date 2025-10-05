using DI;
using UnityEngine;

namespace Inventory
{
    public class ItemSpawnerSystem : MonoBehaviour
    {
        [field: SerializeField] public Transform ItemHolder { get; private set; }

        private ItemSpawnService _spawnService;
        private GameConfig _gameConfig;



        [Inject]
        public void Construct(GameConfig gameConfig, ItemConfig itemConfig)
        {
            _gameConfig = gameConfig;
            _spawnService = new(gameConfig, itemConfig, ItemHolder);
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
            _spawnService.CreateRandomItem();
        }

        private void CreateStartItems()
        {
            for (int i = 0; i < _gameConfig.StartItemsCount; i++)
            {
                _spawnService.CreateRandomItem();
            }
        }
    }
}