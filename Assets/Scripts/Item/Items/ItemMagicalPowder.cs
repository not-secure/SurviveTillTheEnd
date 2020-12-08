namespace Item.Items {
    public class ItemMagicalPowder: ItemBase {
        public ItemMagicalPowder(int count) : base(count) {
        }

        public override int ItemId => 18;
        public override int MaxStack => 256;
        public override ItemType[] Type => new ItemType[] { ItemType.Magic };
        public override string Name => "Magical Powder";
        public override string Description => "It is used to empower materials";

        protected override string GetTextureKey() {
            return "Sprites/Items/Ore & Gem/Gold Nugget";
        }
    }
}