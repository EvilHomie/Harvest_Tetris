using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryCell : MonoBehaviour
    {
        public Item OccupyingItem { get; set; }
        public RectTransform RTransform { get; private set; }
        public TileModifier TileModifier { get; private set; }

        [SerializeField] Image _image;

        public void SetUp(TileModifier tileModifier, Color color, Vector4 padding)
        {
            TileModifier = tileModifier;
            _image.color = color;
            _image.raycastPadding = padding;
            RTransform = GetComponent<RectTransform>();
        }
    }
}

