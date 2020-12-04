using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI {
    public class DialogManager: MonoBehaviour {
        public GameObject inventory;
        private static GameObject _currentDialog;

        public void Update() {
            if (_currentDialog) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Close();
                }

                return;
            }
            
            if (Input.GetKeyDown(KeyCode.I)) {
                _currentDialog = Instantiate(inventory, transform);
            }
        }

        public static void Close() {
            if (!_currentDialog) return;
            
            Object.Destroy(_currentDialog);
            _currentDialog = null;
        }
    }
}