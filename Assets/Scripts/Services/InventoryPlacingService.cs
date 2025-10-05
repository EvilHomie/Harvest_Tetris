using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class InventoryPlacingService
    {
        private readonly GridLayoutGroup _layoutGroup;
        private readonly InventoryConfig _config;
        private readonly Vector3 _deffPos;
        private readonly ItemSpawnerSystem _spawnSystem;
        private readonly Camera _camera;
        private readonly RectTransform _rTransform;
        private readonly List<Item> _itemsInside;

        public InventoryPlacingService(GridLayoutGroup layoutGroup, InventoryConfig config, Vector3 deffPos, ItemSpawnerSystem spawnSystem, Camera camera, RectTransform rectTransform, List<Item> itemsInside)
        {
            _layoutGroup = layoutGroup;
            _config = config;
            _deffPos = deffPos;
            _spawnSystem = spawnSystem;
            _camera = camera;
            _rTransform = rectTransform;
            _itemsInside = itemsInside;
        }

        public List<InventoryCell> GenerateGrid()
        {
            ClearCells(_layoutGroup);
            List<InventoryCell> inventoryCells = new();
            _layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _layoutGroup.constraintCount = _config.ColumnCount;
            _layoutGroup.cellSize = new Vector2(_config.CellSize, _config.CellSize);
            _layoutGroup.spacing = new Vector2(_config.SpaceSize, _config.SpaceSize);
            float singlePadding = -_config.SpaceSize / 2; // хитрость дл€ корректной работы если имеетс€ зазоры между €чейками
            Vector4 totalPadding = new(singlePadding, singlePadding, singlePadding, singlePadding);
            int minSideDimension = Mathf.Min(_config.ColumnCount, _config.RowCount);
            int totalLayers = (minSideDimension + 1) / 2; // хитрость дл€  нечетного размера. 
            int innerLayers = totalLayers - 1;

            int yellowLayers = 0;
            if (innerLayers > 0)
                yellowLayers = Mathf.Max(1, Mathf.FloorToInt(innerLayers / 3f)); // тут можно использовать разное округление. Ѕольше желтого(CeilToInt) ћеньше желтого(FloorToInt).

            for (int y = 0; y < _config.RowCount; y++)
            {
                for (int x = 0; x < _config.ColumnCount; x++)
                {
                    var cell = GameObject.Instantiate(_config.CellPF, _layoutGroup.transform);
                    inventoryCells.Add(cell);
                    int layer = Mathf.Min(x, _config.ColumnCount - 1 - x, y, _config.RowCount - 1 - y);

                    if (layer == 0)
                        cell.SetUp(TileModifier.Red, Color.red, totalPadding);
                    else if (layer <= yellowLayers)
                        cell.SetUp(TileModifier.Yellow, Color.yellow, totalPadding);
                    else
                        cell.SetUp(TileModifier.Green, Color.green, totalPadding);
                }
            }

            _layoutGroup.transform.localPosition = _deffPos;
            ValidateItem();
            return inventoryCells;
        }
        private void ClearCells(GridLayoutGroup layoutGroup)
        {
            foreach (Transform child in layoutGroup.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public bool IsCellsOverInventory(ItemCell[] cells)
        {
            foreach (var cell in cells)
            {
                if (!IsItemOverElement(cell.transform, _rTransform))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsItemOverElement(Transform target, RectTransform overElement)
        {
            Vector2 screenPoint = _camera.WorldToScreenPoint(target.position);
            return RectTransformUtility.RectangleContainsScreenPoint(overElement, screenPoint, _camera);
        }

        public void FindTouchedCells(ItemCell[] itemCells, List<InventoryCell> inventoryCells, out List<InventoryCell> touchedCells, out InventoryCell pivotCell)
        {
            touchedCells = new();
            pivotCell = null;

            foreach (var itemCell in itemCells)
            {
                InventoryCell matchedCell = null;

                foreach (var inventoryCell in inventoryCells)
                {
                    if (IsItemOverElement(itemCell.transform, inventoryCell.RTransform))
                    {
                        matchedCell = inventoryCell;
                        break;
                    }
                }

                if (matchedCell == null)
                {
                    matchedCell = FindNearestCell(itemCell, inventoryCells);
                }

                if (matchedCell != null)
                {
                    touchedCells.Add(matchedCell);

                    if (itemCell.IsMainCell)
                    {
                        pivotCell = matchedCell;
                    }
                }
            }
        }

        // поиск ближайшей €чейки. Ќеобходимо если в сетке инвентар€ есть пустоты между €чейками. ≈сли сами спрайты €чейки с имитацией пустоты то логика излишн€€
        private InventoryCell FindNearestCell(ItemCell itemCell, List<InventoryCell> inventoryCells)
        {
            InventoryCell nearest = null;
            float minDist = float.MaxValue;

            foreach (var invCell in inventoryCells)
            {
                float dist = Vector3.Distance(itemCell.transform.position, invCell.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = invCell;
                }
            }

            return nearest;
        }

        public void PlaceItemInInventory(Item newItem, List<InventoryCell> touchedCells, InventoryCell pivotCell)
        {
            newItem.OccupiedCells = touchedCells;
            Transform pivotTransform = newItem.MainCell.transform;
            Vector3 pivotOffset = pivotTransform.position - newItem.transform.position;
            newItem.transform.position = pivotCell.transform.position - pivotOffset;

            foreach (var cell in touchedCells)
            {
                cell.OccupyingItem = newItem;
            }

            newItem.IsInInventory = true;
            newItem.PivotCell = pivotCell;
            _itemsInside.Add(newItem);
        }

        public void ReleaseCells(List<InventoryCell> cells)
        {
            foreach (var cell in cells)
            {
                if (cell.OccupyingItem != null)
                {
                    RemoveItem(cell.OccupyingItem);
                }

                cell.OccupyingItem = null;
            }
        }

        public void RemoveItem(Item item)
        {
            foreach (var cell in item.OccupiedCells)
            {
                cell.OccupyingItem = null;
            }

            item.OccupiedCells.Clear();
            item.IsInInventory = false;
            item.PivotCell = null;
            _spawnSystem.ReturnItem(item);
            _itemsInside.Remove(item);
        }



#if UNITY_EDITOR // реагирование на изменение конфига. «адумка только дл€ редактора
        static int _lastColumnCount;
        static int _lastRowCount;
        static int _lastCellSize;
        static float _lastSpace;
        static bool _initFrame = true;
        public List<InventoryCell> ValidateInventory()
        {
            if (_lastColumnCount != _config.ColumnCount || _lastRowCount != _config.RowCount || _lastSpace != _config.SpaceSize || _lastCellSize != _config.CellSize)
            {
                if (_initFrame)
                {
                    _lastColumnCount = _config.ColumnCount;
                    _lastRowCount = _config.RowCount;
                    _lastSpace = _config.SpaceSize;
                    _lastCellSize = _config.CellSize;
                    _initFrame = false;
                    return null;
                }

                _lastColumnCount = _config.ColumnCount;
                _lastRowCount = _config.RowCount;
                _lastSpace = _config.SpaceSize;
                _lastCellSize = _config.CellSize;
                return GenerateGrid();
            }

            return null;
        }

        private void ValidateItem()
        {
            Item[] sceneItems = GameObject.FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            Vector2 cellsize = new(_config.CellSize, _config.CellSize);
            Vector2 spacing = new(_config.SpaceSize, _config.SpaceSize);
            float singlePadding = -_config.SpaceSize / 2; // хитрость дл€ корректной работы если имеетс€ зазоры между €чейками
            Vector4 totalPadding = new(singlePadding, singlePadding, singlePadding, singlePadding);

            foreach (Item item in sceneItems)
            {
                item.GridLayoutGroup.cellSize = cellsize;
                item.GridLayoutGroup.spacing = spacing;

                foreach (var cell in item.Cells)
                {
                    cell.Image.raycastPadding = totalPadding;
                }

                _spawnSystem.ReturnItem(item);
                item.OccupiedCells = null;
            }
        }
#endif
    }
}

