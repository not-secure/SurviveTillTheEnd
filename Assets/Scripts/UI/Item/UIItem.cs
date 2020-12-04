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
        public bool draggable = true;

        [NonSerialized] public UISlot Slot;
        [NonSerialized] public ItemBase Item;
        
        private Image _image;
        private TextMeshProUGUI _mesh;
        private Transform _canvas;
        private Transform _parent;

        public static UISlot DraggedSlot;
        public static UIItem DraggedItem;
        
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
            if (!draggable) return;
            
            transform.position = eventData.position;
        }

        public void OnBeginDrag(PointerEventData eventData) {
            if (!draggable) return;
            
            DraggedItem = this;
            
            _parent = transform.parent;
            _image.raycastTarget = false;
            _mesh.raycastTarget = false;
            
            transform.SetParent(_canvas);
        }

        public void OnEndDrag(PointerEventData eventData) {
            if (!draggable) return;
            
            DraggedItem = null;
            
            var t = transform;
            t.SetParent(_parent);
            t.localPosition = Vector3.zero;
            
            _image.raycastTarget = true;
            _mesh.raycastTarget = true;

            if (!DraggedSlot) return;
            var currentItem = Item;
            var changingItem = DraggedSlot.Inventory.Get(DraggedSlot.InventorySlot);

            if (currentItem != null && changingItem != null) {
                // Merge two items
                
                var mergeable = (
                    (currentItem.Meta == null && changingItem.Meta == null) &&
                    (currentItem.ItemId == changingItem.ItemId)
                );

                if (mergeable) {
                    var addingCount =
                        Math.Min(currentItem.MaxStack, currentItem.Count + changingItem.Count)
                        - currentItem.Count;

                    currentItem.Count += addingCount;
                    changingItem.Count -= addingCount;
                    if (changingItem.Count == 0) {
                        changingItem = null;
                    }
                }
            }

            Slot.Inventory.Set(Slot.InventorySlot, changingItem);
            DraggedSlot.Inventory.Set(DraggedSlot.InventorySlot, currentItem);
        }
    }
}