namespace Item.Items {
    public class ItemCoin: ItemBase {
        public ItemCoin(int count) : base(count) {
        }

        public override int ItemId => 6;
        public override int MaxStack => 64;
        public override ItemType[] Type => new []{ ItemType.Coin, ItemType.Silver };
        public override string Name => "Coin";
        public override string Description => "You might need this to use vending machine.\n" +
                                              "You cannot use this as a projectile of a railgun.";
        protected override string GetTextureKey() {
            return "Sprites/Items/Misc/Silver Coin";
        }
    }
}