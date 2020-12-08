namespace Item.Items {
    public class ItemDiamond: ItemBase {
        public ItemDiamond(int count) : base(count) {
        }

        public override int ItemId => 17;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Diamond };
        public override string Name => "Diamond";
        public override string Description => "It seems there is *freezing* magical power in it.";

        protected override string GetTextureKey() {
            return "Sprites/Items/Ore & Gem/Crystal";
        }
    }
}