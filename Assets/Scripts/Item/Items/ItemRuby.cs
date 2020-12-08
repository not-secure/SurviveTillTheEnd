namespace Item.Items {
    public class ItemRuby: ItemBase {
        public ItemRuby(int count) : base(count) {
        }

        public override int ItemId => 16;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Ruby };
        public override string Name => "Ruby";
        public override string Description => "It seems there is *blazing* magical power in it.";

        protected override string GetTextureKey() {
            return "Sprites/Items/Ore & Gem/Cut Ruby";
        }
    }
}