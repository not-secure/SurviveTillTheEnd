using Player;

namespace Item.Items {
    public class ItemCola: ItemBase {
        public ItemCola(int count) : base(count) {
        }
        
        public override int ItemId => 9;
        public override int MaxStack => 4;
        public override ItemType[] Type => new [] { ItemType.Drink };
        public override bool IsConsumed => true;
        public override string Name => "Cola";

        public override string Description => "It does not contain any cocaine, but it does contain some sugars.\n" +
                                              "<color=#00c0ff>Recovers your health a little bit.</color>";

        protected override string GetTextureKey() {
            return "Sprites/Items/Food/Wine 2";
        }
        
        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            player.Health += 10;
        }
    }
}