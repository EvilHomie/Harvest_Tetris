using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryGrid : MonoBehaviour
    {
        [field: SerializeField] public GridLayoutGroup GridLayoutGroup { get; private set; }
        [field: SerializeField] public RectTransform RTransform { get; private set; }
        public List<InventoryCell> Cells {  get; set; }
    }
}