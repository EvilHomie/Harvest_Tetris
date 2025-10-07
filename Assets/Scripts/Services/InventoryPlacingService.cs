using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public class InventoryPlacingService
    {
        private readonly ItemSpawnerSystem _spawnSystem;
        private readonly Camera _camera;
        private readonly RectTransform _rTransform;
        private readonly List<Item> _itemsInside;

        public InventoryPlacingService(ItemSpawnerSystem spawnSystem, Camera camera, RectTransform rectTransform, List<Item> itemsInside)
        {
            _spawnSystem = spawnSystem;
            _camera = camera;
            _rTransform = rectTransform;
            _itemsInside = itemsInside;
        }

       

        private bool IsItemOverElement(Transform target, RectTransform overElement)
        {
            Vector2 screenPoint = _camera.WorldToScreenPoint(target.position);
            return RectTransformUtility.RectangleContainsScreenPoint(overElement, screenPoint, _camera);
        }

        

       



//#if UNITY_EDITOR // реагирование на изменение конфига. Задумка только для редактора
//        static int _lastColumnCount;
//        static int _lastRowCount;
//        static int _lastCellSize;
//        static float _lastSpace;
//        static bool _initFrame = true;
//        public List<InventoryCell> ValidateInventory()
//        {
//            if (_lastColumnCount != _config.ColumnCount || _lastRowCount != _config.RowCount || _lastSpace != _config.SpaceSize || _lastCellSize != _config.CellSize)
//            {
//                if (_initFrame)
//                {
//                    _lastColumnCount = _config.ColumnCount;
//                    _lastRowCount = _config.RowCount;
//                    _lastSpace = _config.SpaceSize;
//                    _lastCellSize = _config.CellSize;
//                    _initFrame = false;
//                    return null;
//                }

//                _lastColumnCount = _config.ColumnCount;
//                _lastRowCount = _config.RowCount;
//                _lastSpace = _config.SpaceSize;
//                _lastCellSize = _config.CellSize;
//                return GenerateGrid();
//            }

//            return null;
//        }

//        private void ValidateItem()
//        {
//            Item[] sceneItems = GameObject.FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.None);

//            Vector2 cellsize = new(_config.CellSize, _config.CellSize);
//            Vector2 spacing = new(_config.SpaceSize, _config.SpaceSize);
//            float singlePadding = -_config.SpaceSize / 2; // хитрость для корректной работы если имеется зазоры между ячейками
//            Vector4 totalPadding = new(singlePadding, singlePadding, singlePadding, singlePadding);

//            foreach (Item item in sceneItems)
//            {
//                item.GridLayoutGroup.cellSize = cellsize;
//                item.GridLayoutGroup.spacing = spacing;

//                foreach (var cell in item.Cells)
//                {
//                    cell.Image.raycastPadding = totalPadding;
//                }

//                _spawnSystem.ReturnItem(item);
//                item.OccupiedCells = null;
//            }
//        }
//#endif
    }
}

