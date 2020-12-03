using System;
using Item;
using UI.Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory {
    public class UICraft : MonoBehaviour {
        public GameObject item;

        private Image _img;
        private Button _button;
        private CraftManager _manager;
        private CraftRecipe _recipe;

        public void OnEnable() {
            _img = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        public void InitializeRecipe(CraftRecipe craft, CraftManager manager) {
            _recipe = craft;
            _manager = manager;
            
            var x = -60;
            foreach (var recipeItem in craft.Recipe) {
                AddItem(recipeItem, x);
                x += 40;
            }
            
            AddItem(craft.Result, 60);
            SetAvailability(manager.Available(craft));
        }

        public void SetAvailability(bool available) {
            var color = _img.color;
            color.a = available ? 1.0f : 0.3f;
            _img.color = color;
        }

        private void AddItem(ItemBase addingItem, int x) {
            var itemObject = Instantiate(item, transform);
            itemObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            
            var pos = itemObject.transform.localPosition;
            pos.x = x;
            pos.y = 0;
            itemObject.transform.localPosition = pos;
            
            var itemController = itemObject.GetComponent<UIItem>();
            itemController.SetItem(addingItem);
        }

        private void OnClick() {
            _manager.Craft(_recipe);
        }
    }
}
