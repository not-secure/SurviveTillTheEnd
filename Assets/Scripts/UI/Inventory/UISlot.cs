using System;
using Item;
using UI.Item;
using UnityEngine;

namespace UI.Inventory {
    public class UISlot: MonoBehaviour {
        public GameObject item;

        private UIItem _currentItem;
        
        public void SetItem(ItemBase newItem) {
            if (_currentItem != null) {
                _currentItem.SetItem(newItem);
            } else {
                var itemObject = Instantiate(item);
                _currentItem = itemObject.GetComponent<UIItem>();
            }
        }
    }
}