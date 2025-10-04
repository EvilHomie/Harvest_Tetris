using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class Item : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ResourceType ResourceType { get; private set; }
        public ItemCell[] Cells { get; private set; }
        public ItemCell MainCell { get; private set; }
        public List<InventoryCell> OccupiedCells { get; set; } = new();
        public InventoryCell PivotCell { get; set; }
        public float AmountOfCollectedResources { get; set; }
        public GridLayoutGroup GridLayoutGroup { get; private set; }
        public RectTransform RTransform { get; private set; }

        private void Awake()
        {
            RTransform = GetComponent<RectTransform>();
            GridLayoutGroup = GetComponent<GridLayoutGroup>();
            Cells = GetComponentsInChildren<ItemCell>();
        }
        private void Start()
        {           
            Configure();
        }

        private void Configure()
        {
            int randomIndex = Random.Range(0, Cells.Length);
            MainCell = Cells[randomIndex];
            MainCell.Image.color = Color.magenta;
        }

        public void OnBeginDrag(PointerEventData eventData) => DragNDropSystem.OnBeginDragGlobal?.Invoke(this, eventData);
        public void OnDrag(PointerEventData eventData) => DragNDropSystem.OnDragGlobal?.Invoke(this, eventData);
        public void OnEndDrag(PointerEventData eventData) => DragNDropSystem.OnEndDragGlobal?.Invoke(this, eventData);
    }
}