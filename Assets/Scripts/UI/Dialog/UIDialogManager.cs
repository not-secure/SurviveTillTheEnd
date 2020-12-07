using System.Collections.Generic;
using System.Linq;
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
        public GameObject dialog;
        private static readonly Stack<DialogItem> CurrentDialog = new Stack<DialogItem>();

        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape))
                Close();
            
            if (Input.GetKeyDown(KeyCode.I))
                OpenIfNoneOpened(DialogKey.Inventory, inventory);
        }

        public void OpenIfNoneOpened(DialogKey key, GameObject obj) {
            if (CurrentDialog.Any(o => o.Key == DialogKey.Inventory))
                return;
            
            var item = new DialogItem(key, Instantiate(obj, transform));
            CurrentDialog.Push(item);
        }

        public static void Close() {
            if (CurrentDialog.Count <= 0) return;
            
            Object.Destroy(CurrentDialog.Pop().Dialog);
        }
    }
}