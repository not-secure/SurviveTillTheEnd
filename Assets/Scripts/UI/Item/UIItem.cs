using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Item {
    public class UIItem: MonoBehaviour {
        public GameObject imageObject;
        public GameObject countObject;

        private Image _image;
        private TextMeshPro _mesh;

        public void OnEnable() {
            _image = imageObject.GetComponent<Image>();
            _mesh = imageObject.GetComponent<TextMeshPro>();
        }
        
        public void SetItem(ItemBase newItem) {
            _image.sprite = newItem.GetImage();
            throw new System.NotImplementedException();
        }
    }
}