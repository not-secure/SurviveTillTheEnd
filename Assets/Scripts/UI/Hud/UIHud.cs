using Item;
using Player;
using UI.Common;
using UI.Inventory;
using UnityEngine;

namespace UI.Hud {
    public class UIHud: MonoBehaviour {
        public GameObject slot;
        public GameObject healthBar;
        public GameObject staminaBar;
        
        private PlayerController _player;
        private global::Item.Inventory _inventory;
        private readonly UISlot[] _slots = new UISlot[6];
        private UIProgress _healthBar;
        private UIProgress _staminaBar;

        private void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            _player = player.GetComponent<PlayerController>();
            _inventory = _player.Inventory;
            _inventory.OnChange += OnInventoryChange;
            
            InitializeSlots();
            
            _healthBar = healthBar.GetComponent<UIProgress>();
            _staminaBar = staminaBar.GetComponent<UIProgress>();
        }
        
        private void Update() {
            var healthRatio = (float) _player.Health / _player.MaxHealth;
            var staminaRatio = (float) _player.Stamina / _player.MaxStamina;
            
            _healthBar.SetProgress(healthRatio);
            _staminaBar.SetProgress(staminaRatio);
        }

        private void InitializeSlots() {
            var x = -250;
            var y = -30;

            for (var i = 0; i < 6; i++) {
                var slotObject = Instantiate(slot, transform);
                var slotController = slotObject.GetComponent<UISlot>();
                slotController.Inventory = _inventory;
                slotController.InventorySlot = i;

                var position = slotObject.transform.localPosition;
                position.x = x;
                position.y = y;
                
                slotObject.transform.localPosition = position;
                _slots.SetValue(slotController, i);
                
                var currentItem = _inventory.Get(i);
                if (currentItem != null) {
                    _slots[i].SetItem(currentItem);
                }
                
                x += 100;
            }
        }

        private void OnInventoryChange(global::Item.Inventory inv, InventoryEventArgs e) {
            var i = e.Index;
            if (i >= 6) return;
            
            var uiSlot = _slots[i];
            var invItem = _inventory.Get(i);
            uiSlot.SetItem(invItem);
        }
    }
}