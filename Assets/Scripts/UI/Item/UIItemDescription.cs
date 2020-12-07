using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Item {
    public class UIItemDescription: MonoBehaviour {
        public TextMeshProUGUI title;
        public TextMeshProUGUI description;
        public Image image;
        
        public void SetItem(ItemBase newItem) {
            image.sprite = newItem.GetImage();
            title.text = newItem.Name;
            description.text = newItem.Description;
        }
    }
}