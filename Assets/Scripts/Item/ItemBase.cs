using System;
using System.Collections.Generic;
using Common;
using Player;
using UI.Status;
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
        public virtual int RequiredStamina => 0;
        public virtual bool IsConsumed => false;

        public Sprite GetImage() {
            return CachedResources.Load<Sprite>(GetTextureKey());
        }

        protected abstract string GetTextureKey();
        
        public int Count { get; set; }

        public virtual bool UseItem(PlayerController player) {
            if (LastUse + Cooltime > Time.time)
                return false;

            LastUse = Time.time;
            
            if (player.Stamina < RequiredStamina) {
                UIStatusManager.GetInstance()?.AddText("You need to take a rest!", 1.0f);
                
                return false;
            }

            player.Stamina -= RequiredStamina;
            if (IsConsumed) {
                for (var i = 0; i < player.Inventory.MaxItems; i++) {
                    if (player.Inventory.Get(i) != this) continue;
                    Count--;
                    player.Inventory.Set(i, (Count == 0) ? null : this);
                    break;
                }
            }
            
            OnUseItem(player);
            return true;
        }

        public virtual void OnUseItem(PlayerController player) {}

        public object Clone() {
            return (ItemBase) Activator.CreateInstance(this.GetType(), new object[] { Count });
        }
    }
}