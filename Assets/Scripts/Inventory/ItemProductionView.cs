using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemProductionView : MonoBehaviour
    {
        [field: SerializeField] public Image ProdImage { get; private set; }
        [field: SerializeField] public RectTransform RTransform { get; private set; }
    }
}