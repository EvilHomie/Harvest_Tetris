using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Generator
{
    public class ItemGenerator : MonoBehaviour
    {
        public int Width { get; set; } = 4;
        public int Height { get; set; } = 4;
        public bool[] Cells { get; set; }

        [SerializeField] string _newItemName;
        [SerializeField] Item _itemPF;
        [SerializeField] ItemCell _itemCellPF;
        [SerializeField] GameObject _emptyCellPF;

        GridLayoutGroup _gridLayoutGroup;

        public void Create()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            if (string.IsNullOrWhiteSpace(_newItemName) || string.IsNullOrEmpty(_newItemName))
            {
                Debug.Log("Нет имени для нового предмета");
                return;
            }

            var newItem = Instantiate(_itemPF, transform);
            newItem.name = _newItemName;

            _gridLayoutGroup = newItem.GetComponent<GridLayoutGroup>();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = Width;
            _gridLayoutGroup.cellSize = new Vector2(50, 50);
            _gridLayoutGroup.spacing = new Vector2(5, 5);

            foreach (var cell in Cells)
            {
                if (cell)
                {
                    Instantiate(_itemCellPF, newItem.transform);
                }
                else
                {
                    Instantiate(_emptyCellPF, newItem.transform);
                }
            }
        }
    }
}