using System;
using Item;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace UI.Item {
    public class UIItem: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public GameObject imageObject;
        public GameObject countObject;

        [NonSerialized] public UISlot Slot;
        [NonSerialized] public ItemBase Item;
        
        private Image _image;
        private TextMeshProUGUI _mesh;
        private Transform _canvas;
        private Transform _parent;

        public static UISlot DraggedSlot;
        
        public void OnEnable() {
            _canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
            _image = imageObject.GetComponent<Image>();
            _mesh = countObject.GetComponent<TextMeshProUGUI>();
        }
        
        public void SetItem(ItemBase newItem) {
            Item = newItem;
            _image.sprite = newItem.GetImage();
            _mesh.text = newItem.Count.ToString();
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            _parent = transform.parent;
            _image.raycastTarget = false;
            _mesh.raycastTarget = false;
            
            transform.SetParent(_canvas);
        }

        public void OnEndDrag(PointerEventData eventData) {
            var t = transform;
            t.SetParent(_parent);
            t.localPosition = Vector3.zero;
            
            _image.raycastTarget = true;
            _mesh.raycastTarget = true;

            if (!DraggedSlot) return;
            Slot.Inventory.Set(Slot.InventorySlot, null);
            DraggedSlot.Inventory.Set(DraggedSlot.InventorySlot, Item);
        }
    }
}