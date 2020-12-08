using Player;

namespace Item.Items {
    public class ItemStaminaAde: ItemBase {
        public ItemStaminaAde(int count) : base(count) {
        }
        
        public override int ItemId => 10;
        public override int MaxStack => 4;
        public override ItemType[] Type => new [] { ItemType.Drink };
        public override bool IsConsumed => true;
        public override string Name => "StaminaAde";

        public override string Description => "It is not a ade which name seems powerful. It is StaminaAde.\n" +
                                              "<color=#00c0ff>Recovers your stamina a little bit.</color>";

        protected override string GetTextureKey() {
            return "Sprites/Items/Potion/Blue Potion";
        }
        
        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            player.Stamina += 35;
        }
    }
}