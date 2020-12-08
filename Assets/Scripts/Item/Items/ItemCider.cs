using Player;

namespace Item.Items {
    public class ItemCider: ItemBase {
        public ItemCider(int count) : base(count) {
        }
        
        public override int ItemId => 7;
        public override int MaxStack => 4;
        public override ItemType[] Type => new [] { ItemType.Drink };
        public override string Name => "Cider";
        public override bool IsConsumed => true;

        public override string Description => "You don't need to eat sweet potatoes anymore.\n" +
                                              "<color=00c0ff>Recovers your stamina a little bit.</color>";

        protected override string GetTextureKey() {
            return "Sprites/Items/Food/Wine";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            player.Stamina += 10;
        }
    }
}