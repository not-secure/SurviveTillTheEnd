using System;
using System.Collections.Generic;
using Common;
using Player;
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
        public abstract string Description { get; }

        public float LastUse = 0f;
        public virtual float Cooltime => 3.0f;

        public Sprite GetImage() {
            return CachedResources.Load<Sprite>(GetTextureKey());
        }

        protected abstract string GetTextureKey();
        
        public int Count { get; set; }

        public virtual bool UseItem(PlayerController player) {
            if (LastUse + Cooltime > Time.time)
                return false;

            LastUse = Time.time;
            OnUseItem(player);
            return true;
        }

        public virtual void OnUseItem(PlayerController player) {}

        public object Clone() {
            return (ItemBase) Activator.CreateInstance(this.GetType(), new object[] { Count });
        }
    }
}