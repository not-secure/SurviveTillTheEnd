using System;
using Item;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    public class UIInventory: MonoBehaviour {
        public GameObject slot;
        public GameObject craft;
        public GameObject scrollPane;
        
        private PlayerController _player;
        private global::Item.Inventory _inventory;
        private readonly UISlot[] _slots = new UISlot[30];

        private void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<PlayerController>();
            _inventory = _player.Inventory;
            _inventory.OnChange += OnChange;

            InitializeSlots();
            InitializeRecipes();
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

        private void InitializeRecipes() {
            var recipes = _player.Craft.GetRecipes();
            var y = -40;
            foreach (var recipe in recipes) {
                var craftObject = Instantiate(craft, scrollPane.transform);
                
                var craftUi = craftObject.GetComponent<UICraft>();
                craftUi.InitializeRecipe(recipe, _player.Craft);
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