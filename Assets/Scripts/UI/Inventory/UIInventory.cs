using System;
using Item;
using Player;
using UnityEngine;

namespace UI.Inventory {
    public class UIInventory: MonoBehaviour {
        public GameObject slot;

        private global::Item.Inventory _inventory;
        private readonly UISlot[] _slots = new UISlot[30];

        private void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            _inventory = player.GetComponent<PlayerController>().Inventory;
            _inventory.OnChange += OnChange;

            InitializeSlots();
        }

        private void InitializeSlots() {
            var startX = -90;
            var startY = 185;
            var gap = 85;
            
            for (var y = 0; y < 5; y++) {
                for (var x = 0; x < 6; x++) {
                    var i = y * 6 + x;
                    var slotObject = Instantiate(slot, transform);
                    var slotController = slotObject.GetComponent<UISlot>();
                    slotController.Inventory = _inventory;
                    slotController.InventorySlot = i;
                    
                    var position = slotObject.transform.localPosition;
                    position.x = startX + gap * x;
                    position.y = startY - gap * y;

                    slotObject.transform.localPosition = position;
                    _slots.SetValue(slotController, i);

                    var currentItem = _inventory.Get(i);
                    if (currentItem != null) {
                        _slots[i].SetItem(currentItem);
                    }
                }
            }
        }

        private void OnChange(global::Item.Inventory inv, InventoryEventArgs e) {
            var i = e.Index;
            var uiSlot = _slots[i];
            var invItem = _inventory.Get(i);
            uiSlot.SetItem(invItem);
        }

        public void OnDisable() {
            _inventory.OnChange -= OnChange;
        }
    }
}