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
        private List<InventoryCell> _cells = new();
        private RectTransform _inventoryRect;
        private InventoryConfig _config;
        private GridLayoutGroup _layoutGroup;
        private Vector3 _deffPos;

        [Inject]
        public void Construct(InventoryConfig config)
        {
            _config = config;
        }

        private void Awake()
        {
            _layoutGroup = GetComponent<GridLayoutGroup>();
            _inventoryRect = GetComponent<RectTransform>();
            _deffPos = transform.localPosition;
        }

        private void Start()
        {
            _cells = InventoryService.GenerateGrid(_layoutGroup, _config, _deffPos);
        }

#if UNITY_EDITOR
        private void Update()
        {
            var newCells = InventoryService.ValidateInventory(_layoutGroup, _config, _deffPos);

            if (newCells != null)
            {
                _cells = newCells;
                ItemsInside.Clear();
            }
        }
#endif

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

        private InventoryCell FindTouchedCell(ItemCell itemCell, List<InventoryCell> inventoryCells)
        {
            InventoryCell touched = null;

            foreach (var inventoryCell in inventoryCells)
            {
                if (IsItemOverElement(itemCell, inventoryCell.RTransform))
                {
                    touched = inventoryCell;
                }
            }

            return touched;
        }

        private void PlaceItemInInventory(Item newItem, List<InventoryCell> touchedCells, InventoryCell pivotCell)
        {
            Transform pivotTransform = newItem.MainCell.transform;
            Vector3 pivotOffset = pivotTransform.position - newItem.transform.position;
            newItem.transform.position = pivotCell.transform.position - pivotOffset;
            touchedCells.Add(pivotCell);
            ReleaseCells(touchedCells);

            foreach (var inventoryCell in touchedCells)
            {
                inventoryCell.OccupyingItem = newItem;
            }

            newItem.OccupiedCells = touchedCells;
            ItemsInside.Add(newItem);
            newItem.PivotCell = pivotCell;
        }

        private void ReleaseCells(List<InventoryCell> touchedCells)
        {
            foreach (var cell in touchedCells)
            {
                if (cell.OccupyingItem != null)
                {
                    var occupyingItem = cell.OccupyingItem;
                    occupyingItem.transform.position = cell.OccupyingItem.DeffPos;
                    RemoveItem(occupyingItem);
                }
            }
        }

        public void RemoveItem(Item oldItem)
        {
            foreach (var cell in oldItem.OccupiedCells)
            {
                cell.OccupyingItem = null;
            }

            oldItem.OccupiedCells.Clear();
            ItemsInside.Remove(oldItem);
            oldItem.PivotCell = null;
        }
    }
}