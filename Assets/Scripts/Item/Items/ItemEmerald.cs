namespace Item.Items {
    public class ItemEmerald: ItemBase {
        public ItemEmerald(int count) : base(count) {
        }
        
        public override int ItemId => 21;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Emerald };
        public override string Name => "Emerald";
        public override string Description => "It seems there is *hasty* magical power in it.";

        protected override string GetTextureKey() {
            return "Sprites/Items/Ore & Gem/Cut Emerald";
        }
    }
}