using DI;
using Service;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryGrid : MonoBehaviour
    {
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

            _placingService = new(layoutGroup, _config, deffPos, _spawnSystem, camera, inventoryRect, ItemsInside);
        }

        private void Start()
        {
            _inventoryCells = _placingService.GenerateGrid();
        }

#if UNITY_EDITOR
        private void Update()
        {
            var newCells = _placingService.ValidateInventory();

            if (newCells != null)
            {
                _inventoryCells = newCells;
                ItemsInside.Clear();
            }
        }
#endif

        public bool TryPlaceItem(Item item)
        {
            if (!_placingService.IsCellsOverInventory(item.Cells))
            {
                return false;
            }

            _placingService.FindTouchedCells(item.Cells, _inventoryCells, out List<InventoryCell> touchedCells, out InventoryCell pivotCell);
            _placingService.ReleaseCells(touchedCells);
            _placingService.PlaceItemInInventory(item, touchedCells, pivotCell);
            return true;
        }

        public void RemoveItem(Item item)
        {
            _placingService.RemoveItem(item);
        }
    }
}