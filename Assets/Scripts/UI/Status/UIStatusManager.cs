using System.Collections.Generic;
using UI.Common;
using UnityEngine;

namespace UI.Status {
    public delegate void OnFinish();
    internal readonly struct StatusItem {
        public StatusItem(GameObject status, float duration, OnFinish callback) {
            StatusObject = status;
            StatusElement = status.GetComponent<UIProgress>();
            Duration = duration;
            Start = Time.time;
            Callback = callback;
        }

        public GameObject StatusObject { get; }
        public UIProgress StatusElement { get; }

        public float Duration { get; }
        public float Start { get; }
        public float End => Start + Duration;
        
        public OnFinish Callback { get; }
    }
    
    public class UIStatusManager: MonoBehaviour {
        public GameObject progress;
        public GameObject text;
        
        private readonly Dictionary<int, StatusItem> _items = new Dictionary<int, StatusItem>();
        
        public int AddItem(GameObject obj, string str, float duration, OnFinish callback) {
            var itemObject = Instantiate(obj, transform);
            var item = new StatusItem(itemObject, duration, callback);
            item.StatusElement.SetText(str);
            item.StatusElement.SetProgress(0);
            
            var lastId = GetLastItemId();
            _items.Add(lastId, item);
            
            return lastId;
        }
        
        private int GetLastItemId() {
            for (var i = 0;; i++) {
                if (!_items.ContainsKey(i))
                    return i;
            }
        }

        public void CancelItem(int itemId) {
            if (!_items.TryGetValue(itemId, out var item)) return;
            
            Destroy(item.StatusObject);
            _items.Remove(itemId);
        }

        public int AddText(string str, float duration) {
            return AddItem(text, str, duration, null);
        }
        
        public int AddProgress(string str, float duration, OnFinish callback) {
            return AddItem(progress, str, duration, callback);
        }

        private void Update() {
            var removedItems = new List<int>();
            
            foreach (var pair in _items) {
                var item = pair.Value;
                var elapsed = Time.time - item.Start;
                var percentage = elapsed / item.Duration;
                item.StatusElement.SetProgress(Mathf.Min(1,percentage));

                if (percentage >= 1) {
                    removedItems.Add(pair.Key);
                    item.Callback?.Invoke();
                }
            }

            foreach (var key in removedItems) {
                CancelItem(key);
            }
        }
    }
}