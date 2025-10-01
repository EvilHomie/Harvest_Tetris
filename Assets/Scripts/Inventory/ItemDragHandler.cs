using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Inventory
{
    [RequireComponent(typeof(Item), typeof(RectTransform))]
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler/*, IPointerClickHandler*/
    {
        [SerializeField ] InventoryGrid grid;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Item _itemUI;
        private bool _isDragging;
                

        void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _itemUI = GetComponent<Item>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            Debug.Log(grid.TryPlaceItem(_itemUI));
        }

        //логика если нужно поворачивать по щелчку. Нужно будет дописать логику размещения при повороте внутри инвентаря
        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    if (_isDragging)
        //    {
        //        return;
        //    }

        //    if (eventData.button == PointerEventData.InputButton.Right)
        //    {
        //        _itemUI.RotateСlockwise();
        //    }
        //}

        private void Update()
        {
            if (_isDragging && Mouse.current.rightButton.wasPressedThisFrame)
            {
                _itemUI.RotateСlockwise();
            }
        }
    }
}

