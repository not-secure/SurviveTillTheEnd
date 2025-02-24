﻿using System.Collections.Generic;
using UnityEngine;

namespace Item.Items {
    public class ItemPlank: ItemBase {
        public ItemPlank(int count) : base(count) {
        }

        public override int ItemId => 1;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Burnable, ItemType.Wooden };
        public override string Name => "Wooden Plank";
        public override string Description => "Who threw away such a good thing in the recycle bin?";

        protected override string GetTextureKey() {
            return "Sprites/Items/Material/Wooden Plank";
        }
    }
}