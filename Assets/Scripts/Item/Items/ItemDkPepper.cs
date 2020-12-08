using Player;

namespace Item.Items {
    public class ItemDkPepper: ItemBase {
        public ItemDkPepper(int count) : base(count) {
        }
        
        public override int ItemId => 11;
        public override int MaxStack => 4;
        public override ItemType[] Type => new [] { ItemType.Drink };
        public override bool IsConsumed => true;
        public override string Name => "Dk. Pepper";

        public override string Description => "An intellectual drink for chosen people.\n" +
                                              "<color=00c0ff>Recovers your health a little bit.</color>";

        protected override string GetTextureKey() {
            return "Sprites/Items/Potion/Red Potion";
        }
        
        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            player.Health += 20;
        }
    }
}