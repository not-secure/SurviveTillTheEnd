using System;
using TMPro;
using UnityEngine;

namespace UI.Hud {
    public class UIHudSlot: MonoBehaviour {
        public GameObject slotKey;
        public GameObject slotBackground;
        
        private TextMeshProUGUI _mesh;
        private RectTransform _keyRect;
        private RectTransform _slotRect;

        private void OnEnable() {
            _keyRect = slotKey.GetComponent<RectTransform>();
            _slotRect = slotBackground.GetComponent<RectTransform>();
            _mesh = slotKey.GetComponent<TextMeshProUGUI>();
            _mesh.autoSizeTextContainer = true;
        }

        public void SetShortcut(string keyName) {
            var scale = _mesh.GetPreferredValues(keyName);
            _mesh.SetText(keyName);
            _keyRect.sizeDelta = scale;
            _slotRect.sizeDelta = scale + new Vector2(10f, 6f);
        }
    }
}