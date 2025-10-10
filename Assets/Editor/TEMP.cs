using UnityEngine;

public class TEMP
{
    
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