using System;
using System.Collections.Specialized;
using Player;
using UnityEngine;

namespace UI.Inventory {
    public class UIInventory: MonoBehaviour {
        public GameObject slot;
        public GameObject player;

        // private global::Item.Inventory _inventory;
        // private UISlot[] slots = new UISlot[30];

        private void OnEnable() {
            // _inventory = player.GetComponent<PlayerController>().Inventory;
            // _inventory.Items.CollectionChanged += OnChange;

            InitializeSlots();
        }

        private void InitializeSlots() {
            /*
            var startX = 270;
            var startY = 80;
            var gap = 100;
            
            for (var y = 0; y < 4; y++) {
                for (var x = 0; x < 6; x++) {
                    var i = y * 6 + x;
                    var slotObject = Instantiate(slot);
                    var position = slotObject.transform.localPosition;
                    position.x = startX + gap * x;
                    position.y = startY + gap * y;

                    slotObject.transform.position = position;
                    slots[i] = slotObject.GetComponent<UISlot>();

                    var currentItem = _inventory.Items[i];
                    if (currentItem != null) {
                        slots[i].SetItem(currentItem);
                    }
                }
            }
            */
        }

        private void OnChange(object sender, NotifyCollectionChangedEventArgs e) {
            // TODO implement
        }
    }
}