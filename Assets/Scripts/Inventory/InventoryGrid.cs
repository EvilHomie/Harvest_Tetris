using DI;
using Service;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryGrid : MonoBehaviour
    {
        [field: SerializeField] public GridLayoutGroup GridLayoutGroup { get; private set; }
        [field: SerializeField] public RectTransform RTransform { get; private set; }
        public List<InventoryCell> Cells {  get; set; }




        public List<Item> ItemsInside { get; private set; } = new();
        private List<InventoryCell> _inventoryCells = new();
        private InventoryConfig _config;
        private ItemSpawnerSystem _spawnSystem;
        private InventoryPlacingService _placingService;


        [Inject]
        public void Construct(InventoryConfig config, ItemSpawnerSystem spawnSystem)
        {
            _config = config;
            _spawnSystem = spawnSystem;
        }

        private void Awake()
        {
            var layoutGroup = GetComponent<GridLayoutGroup>();
            var inventoryRect = GetComponent<RectTransform>();
            var deffPos = transform.localPosition;
            var camera = Camera.main;

            //_placingService = new(layoutGroup, _config, deffPos, _spawnSystem, camera, inventoryRect, ItemsInside);
        }

//#if UNITY_EDITOR
//        private void Update()
//        {
//            var newCells = _placingService.ValidateInventory();

//            if (newCells != null)
//            {
//                _inventoryCells = newCells;
//                ItemsInside.Clear();
//            }
//        }
//#endif

       
    }
}