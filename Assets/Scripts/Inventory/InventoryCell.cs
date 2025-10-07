using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryCell : MonoBehaviour
    {
        public Vector2Int GridPos {  get; set; }
        public TileModifier TileModifier { get; set; }
        public Item OccupyingItem { get; set; }
        [field: SerializeField] public RectTransform RTransform { get; private set; }
        [field: SerializeField] public  Image Image { get; private set; }
    }
}

