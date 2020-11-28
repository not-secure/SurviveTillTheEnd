using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Item {
    public class Inventory {
        public readonly ObservableCollection<ItemBase> Items =
            new ObservableCollection<ItemBase>();

        public Inventory(int maxItems) {
            this.MaxItems = maxItems;
            for (var i = 0; i < maxItems; i++)
                Items[i] = null;
        }
        
        public int MaxItems { get; }

        // Add given item to inventory, returns left Item
        public ItemBase AddItem(ItemBase addingItem) {
            if (addingItem.Meta != null) {
                // Non-stackable item

                if (Items.Count >= MaxItems)
                    return addingItem;
                
                Items.Add(addingItem);
                return null;
            }

            var leftCount = addingItem.Count;
                
            foreach (var item in Items) {
                if (item.Meta != null)
                    continue;

                if (item.ItemId != addingItem.ItemId)
                    continue;

                var addable = addingItem.MaxStack - addingItem.Count;
                var addAmount = Math.Min(addable, leftCount);

                item.Count += addAmount;
                leftCount -= addAmount;

                if (leftCount <= 0)
                    break;
            }

            if (leftCount <= 0) return null;
            
            addingItem.Count = leftCount;
            if (Items.Count >= MaxItems)
                return addingItem;
                
            Items.Add(addingItem);
            return null;
        }

        // Remove given item from inventory, returns true if successful
        public bool RemoveItem(ItemBase removingItem) {
            if (removingItem.Meta != null)
                return Items.Remove(removingItem);

            // Check if all items can be removed
            List<ItemBase> removings = new List<ItemBase>();
            var leftCount = removingItem.Count;
            
            foreach (var item in Items) {
                if (item.Meta != null)
                    continue;

                if (item.ItemId != removingItem.ItemId)
                    continue;
                
                var delAmount = Math.Min(item.Count, leftCount);
                leftCount -= delAmount;

                if (leftCount > 0) {
                    removings.Add(item);
                    continue;
                }

                // Start actual deletion
                item.Count -= delAmount;
                break;
            }

            if (leftCount > 0)
                return false;

            // Run actual deletion as we know all items can be removed
            foreach (var deletion in removings)
                Items.Remove(deletion);

            return true;
        }

        public int GetCount(int itemId) {
            return Items
                .Where(item => item.ItemId == itemId)
                .Sum(item => item.Count);
        }

        public int GetCount(ItemType type) {
            return Items
                .Where(item => item.Type.Contains(type))
                .Sum(item => item.Count);
        }
    }
}