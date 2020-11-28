using System.Collections.Generic;
using UnityEngine;

namespace Item.Items {
    public class ItemPickAxe: ItemBase {
        public ItemPickAxe(int count) : base(count) {
        }

        public override int ItemId => 1;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Burnable, ItemType.Wooden };
        public override string Name => "Wooden Plank";
        
        public override Sprite GetImage() {
            return Resources.Load<Sprite>("/Sprites/Items/Materials/Wooden Plank");
        }
    }
}