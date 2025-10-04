using DI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Inventory
{
    [RequireComponent(typeof(Item), typeof(RectTransform))]
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private InventoryGrid _inventoryGrid;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Item _item;
        private bool _isDragging;

        [Inject]
        public void Construct(InventoryGrid inventoryGrid)
        {
            _inventoryGrid = inventoryGrid;
        }

        void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _item = GetComponent<Item>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            if (_item.PivotCell != null)
            {
                _inventoryGrid.RemoveItem(_item);
            }

            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            if (!_inventoryGrid.TryPlaceItem(_item))
            {
                _item.transform.position = _item.DeffPos;
            }
        }

        private void Update()
        {
            if (_isDragging && Mouse.current.rightButton.wasPressedThisFrame)
            {
                _item.RTransform.Rotate(0, 0, -90);
            }
        }
    }
}

