using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : MonoBehaviour
    {
        private readonly int _width = 8;
        private readonly int _height = 8;
        private InventoryCell[,] _cells;
        private RectTransform _inventoryRect;

        void Awake()
        {
            _cells = new InventoryCell[_width, _height];
            _inventoryRect = GetComponent<RectTransform>();

            InitTiles();
        }

        private void InitTiles()
        {
            var tiles = GetComponentsInChildren<InventoryCell>();
            Vector2Int pos = new(0, 0);

            foreach (var tile in tiles)
            {
                tile.Init(pos);
                _cells[pos.x, pos.y] = tile;
                pos.x++;

                if (pos.x >= _width)
                {
                    pos.y++;
                    pos.x = 0;
                }
            }
        }

        public bool TryPlaceItem(Item item)
        {
            foreach (var cell in item.Cells)
            {
                if (!IsItemOverElement(cell, _inventoryRect))
                {
                    return false;
                }
            }

            var touchedCells = new List<InventoryCell>();
            InventoryCell pivotCell = null;

            foreach (var itemCell in item.Cells)
            {
                var matchCell = FindTouchedCell(itemCell, _cells); // поиск ячейки по границе 

                if (matchCell == null)
                {
                    matchCell = FindNearestCell(itemCell, _cells); // поиск ближайшей ячейки. Необходимо если в сетке инвентаря есть пустоты между ячейками. 
                }

                if (itemCell == item.MainCell)
                {
                    pivotCell = matchCell;
                }
                else
                {
                    touchedCells.Add(matchCell);
                }
            }

            PlaceItemInInventory(item, touchedCells, pivotCell);
            return true;
        }

        private bool IsItemOverElement(ItemCell cell, RectTransform elementRT)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(cell.transform.position);
            return RectTransformUtility.RectangleContainsScreenPoint(elementRT, screenPoint, Camera.main);
        }

        private InventoryCell FindNearestCell(ItemCell itemCell, InventoryCell[,] inventoryCells)
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

        private InventoryCell FindTouchedCell(ItemCell itemCell, InventoryCell[,] inventoryCells)
        {
            InventoryCell touched = null;

            foreach (var inventoryCell in inventoryCells)
            {
                if (IsItemOverElement(itemCell, inventoryCell.RT))
                {
                    touched = inventoryCell;
                }
            }

            return touched;
        }

        private void PlaceItemInInventory(Item item, List<InventoryCell> touchedCells, InventoryCell pivotCell)
        {
            Transform pivotTransform = item.MainCell.transform;
            Vector3 pivotOffset = pivotTransform.position - item.transform.position;
            item.transform.position = pivotCell.transform.position - pivotOffset;

            foreach (var inventoryCell in touchedCells)
            {
                if (inventoryCell.OccupyingItem != null)
                {
                    RemoveItem(inventoryCell.OccupyingItem);                    
                }
            }

            foreach (var inventoryCell in touchedCells)
            {
                inventoryCell.OccupyingItem = item;
            }

            item.OccupiedCells = touchedCells;
            item.OccupiedCells.Add(pivotCell);
        }

        private void RemoveItem(Item item)
        {
            item.transform.position = Vector3.zero;

            foreach (var inventoryCell in item.OccupiedCells)
            {
                inventoryCell.OccupyingItem = null;
            }
        }
    }
}