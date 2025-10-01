using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public ItemCell[] Cells { get; private set; }
        [field: SerializeField] public ItemCell MainCell { get; private set; }
        public bool[,] Shape { get; private set; }
        public List<InventoryCell> OccupiedCells { get; set; }
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            FillShape();
        }

        private void FillShape()
        {
            int collumsCount = 0;
            int rowsCount = 0;

            foreach (var cell in Cells)
            {
                collumsCount = cell.ColumnIndex > collumsCount ? cell.ColumnIndex : collumsCount;
                rowsCount = cell.LineIndex > rowsCount ? cell.LineIndex : rowsCount;
            }

            Shape = new bool[rowsCount + 1, collumsCount + 1];

            foreach (var cell in Cells)
            {
                Shape[cell.LineIndex, cell.ColumnIndex] = true;
            }
        }

        public void RotateСlockwise()
        {
            int lineLength = Shape.GetLength(0);
            int columnLength = Shape.GetLength(1);
            bool[,] newShape = new bool[columnLength, lineLength];

            for (int l = 0; l < lineLength; l++)
            {
                for (int c = 0; c < columnLength; c++)
                {
                    newShape[c, lineLength - 1 - l] = Shape[l, c];
                }
            }

            Shape = newShape;
            _rectTransform.Rotate(0, 0, -90);
        }

        public void RotateСounterСlockwise()
        {
            int lineLength = Shape.GetLength(0);
            int columnLength = Shape.GetLength(1);
            bool[,] newShape = new bool[columnLength, lineLength];

            for (int l = 0; l < lineLength; l++)
            {
                for (int c = 0; c < columnLength; c++)
                {
                    newShape[columnLength - 1 - c, l] = Shape[l, c];
                }
            }

            Shape = newShape;
            _rectTransform.Rotate(0, 0, 90);
        }

        void TestShape()
        {
            string testString = null;

            int lineCount = Shape.GetLength(0);
            int columnsCount = Shape.GetLength(1);

            int colum = 0;
            int line = 0;

            for (int l = 0; l < lineCount; l++)
            {
                for (int c = 0; c < columnsCount; c++)
                {
                    testString += $"{Shape[line, colum]} ";
                    colum++;
                }

                testString += "\n";
                line++;
                colum = 0;
                Debug.Log(testString);
                testString = null;
            }
            Debug.Log("   ");
        }
    }
}