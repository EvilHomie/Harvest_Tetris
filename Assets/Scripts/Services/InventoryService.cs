using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.STP;

namespace Service
{
    public class InventoryService
    {
        public static List<InventoryCell> GenerateGrid(GridLayoutGroup layoutGroup, InventoryConfig config, Vector3 deffPos)
        {
            ClearCells(layoutGroup);
            List<InventoryCell> inventoryCells = new();
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = config.ColumnCount;
            layoutGroup.cellSize = new Vector2(config.CellSize, config.CellSize);
            layoutGroup.spacing = new Vector2(config.SpaceSize, config.SpaceSize);
            float singlePadding = -config.SpaceSize / 2; // хитрость для корректной работы если имеется зазоры между ячейками
            Vector4 totalPadding = new(singlePadding, singlePadding, singlePadding, singlePadding);
            int minSideDimension = Mathf.Min(config.ColumnCount, config.RowCount);
            int totalLayers = (minSideDimension + 1) / 2; // хитрость для  нечетного размера. 
            int innerLayers = totalLayers - 1;

            int yellowLayers = 0;
            if (innerLayers > 0)
                yellowLayers = Mathf.Max(1, Mathf.FloorToInt(innerLayers / 3f)); // тут можно использовать разное округление. Больше желтого(CeilToInt) Меньше желтого(FloorToInt).

            for (int y = 0; y < config.RowCount; y++)
            {
                for (int x = 0; x < config.ColumnCount; x++)
                {
                    var cell = GameObject.Instantiate(config.CellPF, layoutGroup.transform);
                    inventoryCells.Add(cell);
                    int layer = Mathf.Min(x, config.ColumnCount - 1 - x, y, config.RowCount - 1 - y);

                    if (layer == 0)
                        cell.SetUp(TileModifier.Red, Color.red, totalPadding);
                    else if (layer <= yellowLayers)
                        cell.SetUp(TileModifier.Yellow, Color.yellow, totalPadding);
                    else
                        cell.SetUp(TileModifier.Green, Color.green, totalPadding);
                }
            }

            layoutGroup.transform.localPosition = deffPos;
            ValidateItem(config);
            return inventoryCells;
        }
        private static void ClearCells(GridLayoutGroup layoutGroup)
        {
            foreach (Transform child in layoutGroup.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

#if UNITY_EDITOR // реагирование на изменение конфига. Задумка только для редактора
        static int _lastColumnCount;
        static int _lastRowCount;
        static int _lastCellSize;
        static float _lastSpace;
        static bool _initFrame = true;
        public static List<InventoryCell> ValidateInventory(GridLayoutGroup layoutGroup, InventoryConfig config, Vector3 deffPos)
        {
            if (_lastColumnCount != config.ColumnCount || _lastRowCount != config.RowCount || _lastSpace != config.SpaceSize || _lastCellSize != config.CellSize)
            {
                if (_initFrame)
                {
                    _lastColumnCount = config.ColumnCount;
                    _lastRowCount = config.RowCount;
                    _lastSpace = config.SpaceSize;
                    _lastCellSize = config.CellSize;
                    _initFrame = false;
                    return null;
                }

                _lastColumnCount = config.ColumnCount;
                _lastRowCount = config.RowCount;
                _lastSpace = config.SpaceSize;
                _lastCellSize = config.CellSize;
                return GenerateGrid(layoutGroup, config, deffPos);
            }

            return null;
        }

        private static void ValidateItem(InventoryConfig config)
        {
            Item[] sceneItems = GameObject.FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            Vector2 cellsize = new(config.CellSize, config.CellSize);
            Vector2 spacing = new(config.SpaceSize, config.SpaceSize);
            float singlePadding = -config.SpaceSize / 2; // хитрость для корректной работы если имеется зазоры между ячейками
            Vector4 totalPadding = new(singlePadding, singlePadding, singlePadding, singlePadding);

            foreach (Item item in sceneItems)
            {
                item.GridLayoutGroup.cellSize = cellsize;
                item.GridLayoutGroup.spacing = spacing;

                foreach (var cell in item.Cells)
                {
                    cell.Image.raycastPadding = totalPadding;
                }

                item.RTransform.position = item.DeffPos;
                item.PivotCell = null; 
                item.OccupiedCells = null;
            }
        }
#endif
    }
}

