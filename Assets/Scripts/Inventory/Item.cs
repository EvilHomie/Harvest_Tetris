using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public ItemCell[] Cells { get; private set; }
        [field: SerializeField] public ItemCell MainCell { get; private set; }
        public List<InventoryCell> OccupiedCells { get; set; }
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void RotateСlockwise()
        {            
            _rectTransform.Rotate(0, 0, -90);
        }

        public void RotateСounterСlockwise()
        {           
            _rectTransform.Rotate(0, 0, 90);
        }
    }
}