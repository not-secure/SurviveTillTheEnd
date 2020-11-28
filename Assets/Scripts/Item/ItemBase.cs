using System;
using System.Collections.Generic;
using UnityEngine;

namespace Item {
    public abstract class ItemBase {
        public Dictionary<string, object> Meta;
        
        public ItemBase(int count) {
            this.Count = count;
        }
        
        public abstract int ItemId { get; }
        public abstract int MaxStack { get; }
        public abstract ItemType[] Type { get; }
        public abstract string Name { get; }

        public abstract Sprite GetImage();
        
        public int Count { get; set; }

        public virtual void UseItem() {}
    }
}