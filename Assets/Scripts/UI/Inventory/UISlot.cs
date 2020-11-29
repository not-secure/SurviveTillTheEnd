using System;
using Item;
using UI.Item;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory {
    public class UISlot: MonoBehaviour, IDropHandler {
        public GameObject item;

        private UIItem _currentItem;
        [NonSerialized] public ItemBase HoldingItem;
        [NonSerialized] public global::Item.Inventory Inventory;
        [NonSerialized] public int InventorySlot;
        
        public void SetItem(ItemBase newItem) {
            if (_currentItem) {
                if (newItem == null) {
                    Destroy(_currentItem.gameObject);
                    _currentItem = null;
                } else {
                    _currentItem.SetItem(newItem);
                }
            } else {
                var itemObject = Instantiate(item, transform);
                _currentItem = itemObject.GetComponent<UIItem>();
                _currentItem.Slot = this;
                _currentItem.SetItem(newItem);
            }

            HoldingItem = newItem;
        }

        public void OnDrop(PointerEventData eventData) {
            UIItem.DraggedSlot = this;
        }
    }
}