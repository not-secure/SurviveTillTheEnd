using System;
using Item;
using UnityEngine;

namespace UI.Item {
    public class UIItemDescriptionManager: MonoBehaviour {
        public GameObject description;
        private RectTransform _transform;
        private UIItemDescription _descriptionController;
        private ItemBase _item;

        private void OnEnable() {
            _transform = description.GetComponent<RectTransform>();
            _descriptionController = description.GetComponent<UIItemDescription>();
        }

        public void ShowItem(ItemBase item, Vector2 position) {
            description.SetActive(true);
            _item = item;
            transform.position = position;
            _descriptionController.SetItem(item);
        }

        public void HideItem(ItemBase item) {
            if (item != null && _item != item) return;
            
            description.SetActive(false);
            _item = null;
        }
    }
}