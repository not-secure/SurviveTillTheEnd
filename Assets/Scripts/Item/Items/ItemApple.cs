﻿using Player;

namespace Item.Items {
    public class ItemApple: ItemBase {
        public ItemApple(int count) : base(count) {
        }

        public override int ItemId => 3;
        public override int MaxStack => 16;
        public override ItemType[] Type => new[] { ItemType.Food };
        public override string Name => "Apple";
        public override bool IsConsumed => true;

        public override string Description =>
            "You should be good at English to eat this apple.\n" +
            "<color=#00c0ff>Recovers your health a little bit.</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Food/Apple";
        }
        
        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            player.Health += 50;
        }
    }
}