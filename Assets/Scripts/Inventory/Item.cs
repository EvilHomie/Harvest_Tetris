using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ResourceType ResourceType { get; set; }
        public ItemCell[] Cells { get; set; }
        public ItemCell MainCell { get; set; }
        public List<InventoryCell> OccupiedCells { get; set; } = new();
        public InventoryCell PivotCell { get; set; }
        public float AmountOfCollectedResources { get; set; }
        public GridLayoutGroup GridLayoutGroup { get; set; }
        public RectTransform RTransform { get; set; }

        public void OnBeginDrag(PointerEventData eventData) => DragNDropSystem.OnBeginDragGlobal?.Invoke(this, eventData);
        public void OnDrag(PointerEventData eventData) => DragNDropSystem.OnDragGlobal?.Invoke(this, eventData);
        public void OnEndDrag(PointerEventData eventData) => DragNDropSystem.OnEndDragGlobal?.Invoke(this, eventData);
    }
}