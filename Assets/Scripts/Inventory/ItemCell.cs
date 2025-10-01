using UnityEngine;

namespace Inventory
{
    public class ItemCell : MonoBehaviour
    {
        [field: SerializeField] public int LineIndex { get; private set; }
        [field: SerializeField] public int ColumnIndex { get; private set; }
    }
}