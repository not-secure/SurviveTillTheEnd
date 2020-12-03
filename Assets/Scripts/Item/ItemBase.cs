using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Item {
    public abstract class ItemBase: ICloneable {
        public Dictionary<string, object> Meta;
        
        public ItemBase(int count) {
            this.Count = count;
        }
        
        public abstract int ItemId { get; }
        public abstract int MaxStack { get; }
        public abstract ItemType[] Type { get; }
        public abstract string Name { get; }

        public Sprite GetImage() {
            return CachedResources.Load<Sprite>(GetTextureKey());
        }

        protected abstract string GetTextureKey();
        
        public int Count { get; set; }

        public virtual void UseItem() {}

        public object Clone() {
            return (ItemBase) Activator.CreateInstance(this.GetType(), new object[] { Count });
        }
    }
}