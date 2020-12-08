namespace Item.Items {
    public class ItemCoin: ItemBase {
        public ItemCoin(int count) : base(count) {
        }

        public override int ItemId => 6;
        public override int MaxStack => 64;
        public override ItemType[] Type => new []{ ItemType.Coin, ItemType.Gold };
        public override string Name => "Coin";
        public override string Description => "You might need this to use vending machine";
        protected override string GetTextureKey() {
            return "/Sprites/Items/Misc/Golden Coin";
        }
    }
}