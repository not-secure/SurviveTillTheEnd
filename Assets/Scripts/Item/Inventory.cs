using System;
using System.Collections.Generic;
using System.Linq;

namespace Item {
    public class InventoryEventArgs {
        public InventoryEventArgs(int i) {
            Index = i;
        }
        
        public int Index { get; }
    }
    
    public class Inventory {
        private readonly ItemBase[] _items;

        public Inventory(int maxItems) {
            this.MaxItems = maxItems;
            _items = new ItemBase[maxItems];
            
            for (var i = 0; i < MaxItems; i++)
                _items[i] = null;
        }
        
        public int MaxItems { get; }

        // Add given item to inventory, returns left Item
        public ItemBase AddItem(ItemBase addingItem) {
            if (addingItem.Meta != null) {
                // Non-stackable item
                for (var i = 0; i < MaxItems; i++) {
                    if (_items[i] != null) continue;
                    Set(i, addingItem);
                    return null;
                }
                
                return addingItem;
            }

            var leftCount = addingItem.Count;

            for (var i = 0; i < MaxItems; i++) {
                var item = _items[i];
                if (item == null) continue;
                
                if (item.Meta != null)
                    continue;

                if (item.ItemId != addingItem.ItemId)
                    continue;

                var addable = item.MaxStack - item.Count;
                var addAmount = Math.Min(addable, leftCount);

                item.Count += addAmount;
                leftCount -= addAmount;
                OnChange?.Invoke(this, new InventoryEventArgs(i));

                if (leftCount <= 0)
                    break;
            }

            if (leftCount <= 0) return null;
            
            addingItem.Count = leftCount;
            for (var i = 0; i < MaxItems; i++) {
                if (_items[i] != null) continue;
                Set(i, addingItem);
                return null;
            }

            return addingItem;
        }

        // Remove given item from inventory, returns true if successful
        public bool RemoveItem(ItemBase removingItem) {
            if (removingItem.Meta != null) {
                var i = Array.IndexOf(_items, removingItem);
                if (i < 0) return false;
                Set(i, null);
                return true;
            }

            // Check if all items can be removed
            var removings = new List<int>();
            var leftCount = removingItem.Count;

            for (var i = 0; i < MaxItems; i++) {
                var item = _items[i];
                if (item == null) continue;
                
                if (item.Meta != null)
                    continue;

                if (item.ItemId != removingItem.ItemId)
                    continue;
                
                var delAmount = Math.Min(item.Count, leftCount);
                leftCount -= delAmount;

                if (leftCount > 0) {
                    removings.Add(i);
                    continue;
                }

                // Start actual deletion
                item.Count -= delAmount;
                OnChange?.Invoke(this, new InventoryEventArgs(i));
                break;
            }

            if (leftCount > 0)
                return false;

            // Run actual deletion as we know all items can be removed
            foreach (var deletion in removings) {
                Set(deletion, null);
            }

            return true;
        }

        public ItemBase Get(int slot) {
            return _items[slot];
        }

        public void Set(int slot, ItemBase item) {
            _items[slot] = item;
            OnChange?.Invoke(this, new InventoryEventArgs(slot));
        }

        public int GetCount(int itemId) {
            return _items
                .Where(item => item.ItemId == itemId)
                .Sum(item => item.Count);
        }

        public int GetCount(ItemType type) {
            return _items
                .Where(item => item.Type.Contains(type))
                .Sum(item => item.Count);
        }

        public delegate void InventoryEventHandler(Inventory inv, InventoryEventArgs args);

        public event InventoryEventHandler OnChange;
    }
}