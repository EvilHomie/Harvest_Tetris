using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemCell : MonoBehaviour
    {
        [field: SerializeField] public Image Image { get; private set; }
        public bool IsMainCell { get; set; }
    }
}