using System;
using System.Collections.Generic;
using System.Linq;
using UI.Item;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Dialog {
    public enum DialogKey {
        Inventory,
        Note
    }
    
    internal readonly struct DialogItem {
        public DialogItem(DialogKey key, GameObject obj) {
            Key = key;
            Dialog = obj;
        }
        
        public DialogKey Key { get; }
        public GameObject Dialog { get; }
    }
    
    public class UIDialogManager: MonoBehaviour {
        public GameObject inventory;
        public GameObject note;
        private static UIItemDescriptionManager _itemDescription;
        private static readonly Stack<DialogItem> CurrentDialog = new Stack<DialogItem>();

        private void OnEnable() {
            _itemDescription = GameObject.FindGameObjectWithTag("ItemDescriptionManager")
                .GetComponent<UIItemDescriptionManager>();
        }

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
            
            if (Input.GetKeyDown(KeyCode.I))
                OpenIfNoneOpened(DialogKey.Inventory, inventory);
        }

        public GameObject OpenIfNoneOpened(DialogKey key, GameObject obj) {
            var dialog = CurrentDialog.FirstOrDefault(o => o.Key == key);
            if (!dialog.Equals(default(DialogItem)))
                return dialog.Dialog;
            
            var item = new DialogItem(key, Instantiate(obj, transform));
            CurrentDialog.Push(item);

            return item.Dialog;
        }

        public static void Close() {
            if (CurrentDialog.Count <= 0) return;
            
            if (_itemDescription)
                _itemDescription.HideItem(null);
            
            Object.Destroy(CurrentDialog.Pop().Dialog);
        }
    }
}