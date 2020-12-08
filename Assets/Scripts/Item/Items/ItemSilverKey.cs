namespace Item.Items {
    public class ItemSilverKey: ItemBase {
        public ItemSilverKey(int count) : base(count) {
        }

        public override int ItemId => 15;
        public override int MaxStack => 64;
        public override ItemType[] Type => new ItemType[] { ItemType.Silver };
        public override string Name => "Silver Key";
        public override string Description => "You might want to melt this with your magic.";

        protected override string GetTextureKey() {
            return "Sprites/Items/Misc/Silver Key";
        }
    }
}