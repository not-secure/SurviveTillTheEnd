using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialog {
    public class UIDialogClose: MonoBehaviour {
        private Button _button;
        public void Start() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private static void OnClick() {
            UIDialogManager.Close();
        }
    }
}