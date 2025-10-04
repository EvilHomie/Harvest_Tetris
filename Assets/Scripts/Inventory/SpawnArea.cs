using DI;
using UnityEngine;

namespace Inventory
{
    public class SpawnArea : MonoBehaviour
    {
        [field: SerializeField] public Transform ContentArea { get; private set; }

        GameConfig _config;

        [Inject]
        public void Construct(GameConfig gameConfig)
        {
            _config = gameConfig;
        }

        private void Start()
        {
            CreateStartItems();
        }

        private void CreateStartItems()
        {
            for (int i = 0; i < _config.StartItemsCount; i++)
            {
                int randomIndex = Random.Range(0, _config.Items.Length);
                Instantiate(_config.Items[randomIndex], ContentArea.transform);
            }
        }
    }
}