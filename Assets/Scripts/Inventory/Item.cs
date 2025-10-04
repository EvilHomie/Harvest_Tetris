using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        [field: SerializeField] public ItemCell[] Cells { get; private set; }
        [field: SerializeField] public ItemCell MainCell { get; private set; }
        public List<InventoryCell> OccupiedCells { get; set; } = new();
        public InventoryCell PivotCell { get; set; }
        public float AmountOfCollectedResources { get; set; }
        public GridLayoutGroup GridLayoutGroup { get; private set; }
        public Vector3 DeffPos { get; private set; }
        public RectTransform RTransform { get; private set; }

        private void Awake()
        {
            RTransform = GetComponent<RectTransform>();
            GridLayoutGroup = GetComponent<GridLayoutGroup>();
        }
        private void Start()
        {
            DeffPos = transform.position;
        }

        public void RotateСlockwise()
        {
            RTransform.Rotate(0, 0, -90);
        }

        public void RotateСounterСlockwise()
        {
            RTransform.Rotate(0, 0, 90);
        }
    }
}