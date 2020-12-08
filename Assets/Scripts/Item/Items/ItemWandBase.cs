namespace Item.Items {
    public class ItemWandBase: ItemBase {
        public ItemWandBase(int count) : base(count) {
        }

        public override int ItemId => 12;
        public override int MaxStack => 1;
        public override ItemType[] Type => new [] { ItemType.Magic, ItemType.Wand, ItemType.Wooden, ItemType.Burnable };
        public override string Name => "Wand Base";
        public override string Description => "Is this just an ugly wooden thing?\n" +
                                              "<color=#00c0ff>This will be used to create your wand</color>";

        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Wooden Staff";
        }
    }
}