using System;
using Item;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    public class UIInventory: MonoBehaviour {
        public GameObject slot;
        public GameObject craft;
        public GameObject availableCraft;
        public GameObject unavailableCraft;
        
        private PlayerController _player;
        private global::Item.Inventory _inventory;
        private readonly UISlot[] _slots = new UISlot[30];
        private UICraft[] _recipes;

        private void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<PlayerController>();
            _inventory = _player.Inventory;
            _inventory.OnChange += OnInventoryChange;
            _player.Craft.OnAvailRecipeChange += OnAvailRecipeChange;

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
            _recipes = new UICraft[recipes.Length];

            for (var i = 0; i < recipes.Length; i++) {
                var recipe = recipes[i];
                var craftObject = Instantiate(craft, availableCraft.transform);
                
                var craftUi = craftObject.GetComponent<UICraft>();
                craftUi.InitializeRecipe(recipe, _player.Craft);
                _recipes[i] = craftUi;
            }
            
            UpdateRecipes();
        }

        private void UpdateRecipes() {
            var craftManager = _player.Craft;
            var recipes = craftManager.GetRecipes();

            for (var i = 0; i < recipes.Length; i++) {
                var recipe = recipes[i];
                var recipeObject = _recipes[i];

                recipeObject.transform.SetParent(
                    craftManager.Available(recipe)
                    ? availableCraft.transform
                    : unavailableCraft.transform
                );
                recipeObject.SetAvailability(craftManager.Available(recipe));
            }
        }

        private void OnInventoryChange(global::Item.Inventory inv, InventoryEventArgs e) {
            var i = e.Index;
            var uiSlot = _slots[i];
            var invItem = _inventory.Get(i);
            uiSlot.SetItem(invItem);
            UpdateRecipes();
        }

        private void OnAvailRecipeChange(CraftManager manager) {
            UpdateRecipes();
        }

        public void OnDisable() {
            _inventory.OnChange -= OnInventoryChange;
            _player.Craft.OnAvailRecipeChange -= OnAvailRecipeChange;
        }
    }
}