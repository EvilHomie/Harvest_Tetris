using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryCell : MonoBehaviour
    {
        public Item OccupyingItem { get; set; }
        [field: SerializeField] public TileModifier TileModifier { get; private set; }
        public RectTransform RT { get; private set; }

        public void Init()
        {
            if (TryGetComponent<Image>(out var image))
            {
                image.color = TileModifier switch
                {
                    TileModifier.Green => Color.green,
                    TileModifier.Yellow => Color.yellow,
                    TileModifier.Red => Color.red,
                    _ => Color.black
                };
            }
            else
            {
                throw new System.Exception($"No image on InventoryTile {gameObject.name}");
            }

            RT = GetComponent<RectTransform>();
        }
    }
}

