using Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SystemHelper
{
    public class InventoryFactory
    {
        public static InventoryGrid CreateInventoryGrid(InventoryConfig config, Transform parent)
        {
            var newInventoryGrid = GameObject.Instantiate(config.InventoryPF, parent, false);
            ConfigureLayoutGroup(newInventoryGrid, config);
            CreateCells(newInventoryGrid, config);
            ConfigureProductionLayers(newInventoryGrid, config);
            ConfigureCells(newInventoryGrid);
            newInventoryGrid.transform.localPosition = config.LocalPosition;
            return newInventoryGrid;
        }

        public static void DestroyInventoryGrid(InventoryGrid inventoryGrid)
        {
            GameObject.Destroy(inventoryGrid.gameObject);
        }

        private static void ConfigureLayoutGroup(InventoryGrid newInventoryGrid, InventoryConfig config)
        {
            newInventoryGrid.GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            newInventoryGrid.GridLayoutGroup.constraintCount = config.ColumnCount;
            newInventoryGrid.GridLayoutGroup.cellSize = Vector2.one * config.CellSize;
            newInventoryGrid.GridLayoutGroup.spacing = Vector2.one * config.SpaceSize;
        }

        private static void CreateCells(InventoryGrid newInventoryGrid, InventoryConfig config)
        {
            List<InventoryCell> inventoryCells = new();

            for (int r = 0; r < config.RowCount; r++)
            {
                for (int c = 0; c < config.ColumnCount; c++)
                {
                    var cell = GameObject.Instantiate(config.CellPF, newInventoryGrid.transform);
                    cell.GridPos = new Vector2Int(r, c);
                    inventoryCells.Add(cell);
                }
            }

            newInventoryGrid.Cells = inventoryCells;
        }

        private static void ConfigureProductionLayers(InventoryGrid inventoryGrid, InventoryConfig config)
        {
            int minSideDimension = Mathf.Min(config.ColumnCount, config.RowCount);
            int totalLayerCount = Mathf.CeilToInt(minSideDimension / 2f); // хитрость дл€ нечетного размера. „тобы не потер€ть центральный слой, например, в 3х3
            int outerLayerCount = 1;
            int innerLayerCount = totalLayerCount - outerLayerCount;
            int yellowLayers = innerLayerCount == 0 ? 0 : Mathf.Max(1, Mathf.FloorToInt(innerLayerCount / 3f)); // тут можно использовать разное округление. Ѕольше желтого(CeilToInt) ћеньше желтого(FloorToInt).

            foreach (var cell in inventoryGrid.Cells)
            {
                int distanceToTop = cell.GridPos.x;                             // от верхней границы
                int distanceToBottom = config.RowCount - 1 - cell.GridPos.x;    // от нижней границы
                int distanceToLeft = cell.GridPos.y;                            // от левой границы
                int distanceToRight = config.ColumnCount - 1 - cell.GridPos.y;  // от правой границы

                int layer = Mathf.Min(distanceToLeft, distanceToRight, distanceToTop, distanceToBottom); // —лой определ€етс€ минимальным рассто€нием до границы сетки

                if (layer == 0)
                    cell.TileModifier = TileModifier.Red;
                else if (layer <= yellowLayers)
                    cell.TileModifier = TileModifier.Yellow;
                else
                    cell.TileModifier = TileModifier.Green;
            }
        }

        private static void ConfigureCells(InventoryGrid inventoryGrid)
        {
            foreach (var cell in inventoryGrid.Cells)
            {
                cell.Image.color = cell.TileModifier switch
                {
                    TileModifier.Red => Color.red,
                    TileModifier.Yellow => Color.yellow,
                    TileModifier.Green => Color.green,
                    _ => Color.black,
                };
            }
        }
    }
}
