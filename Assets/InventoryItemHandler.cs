using Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace SystemHelper
{
    public class InventoryItemHandler
    {
        public static bool TryPlaceItem(Item item, InventoryGrid inventoryGrid, Camera camera)
        {
            if (!IsItemInInventoryBounds(item, inventoryGrid, camera))
            {
                return false;
            }

            FindTouchedCells(item, inventoryGrid, camera, out List<InventoryCell> touchedCells);
            ReleaseCells(touchedCells);
            PlaceItemInInventory(item, touchedCells);
            return true;
        }

        public static void RemoveItem(Item item)
        {
            foreach (var cell in item.OccupiedCells)
            {
                cell.OccupyingItem = null;
            }

            item.OccupiedCells.Clear();
            item.IsInInventory = false;
            item.PivotCell = null;
        }

        private static bool IsItemInInventoryBounds(Item item, InventoryGrid inventoryGrid, Camera camera)
        {
            foreach (var cell in item.Cells)
            {
                if (!Utils.IsTargetOverElement(cell.RTransform, inventoryGrid.RTransform, camera))
                {
                    return false;
                }
            }

            return true;
        }

        private static void FindTouchedCells(Item item, InventoryGrid inventoryGrid, Camera camera, out List<InventoryCell> touchedCells)
        {
            touchedCells = new();

            foreach (var itemCell in item.Cells)
            {
                InventoryCell matchedCell = null;

                foreach (var inventoryCell in inventoryGrid.Cells)
                {
                    if (Utils.IsTargetOverElement(itemCell.RTransform, inventoryCell.RTransform, camera))
                    {
                        matchedCell = inventoryCell;
                        break;
                    }
                }

                if (matchedCell == null)
                {
                    matchedCell = FindNearestCell(itemCell, inventoryGrid.Cells);
                }

                if (matchedCell != null)
                {
                    touchedCells.Add(matchedCell);

                    if (itemCell.IsMainCell)
                    {
                        item.PivotCell = matchedCell;
                    }
                }
            }
        }

        // поиск ближайшей €чейки. Ќеобходимо если в сетке инвентар€ есть пустоты между €чейками. Ћучше сделать без пустот, а еЄ имитировать в спрайтах €чейки
        private static InventoryCell FindNearestCell(ItemCell itemCell, List<InventoryCell> inventoryCells)
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

        private static void PlaceItemInInventory(Item newItem, List<InventoryCell> touchedCells)
        {
            newItem.OccupiedCells = touchedCells;
            var mainCellRT = newItem.MainCell.RTransform;
            Vector3 pivotOffset = mainCellRT.position - newItem.transform.position;
            newItem.RTransform.position = newItem.PivotCell.RTransform.position - pivotOffset;

            foreach (var cell in touchedCells)
            {
                cell.OccupyingItem = newItem;
            }

            newItem.IsInInventory = true;
        }

        private static void ReleaseCells(List<InventoryCell> cells)
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
    }
}