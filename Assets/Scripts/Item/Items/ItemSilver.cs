namespace Item.Items {
    public class ItemSilver: ItemBase {
        public ItemSilver(int count) : base(count) {
        }

        public override int ItemId => 2;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Silver };
        public override string Name => "Silver";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Ore & Gem/Silver Ingot";
        }
    }
}