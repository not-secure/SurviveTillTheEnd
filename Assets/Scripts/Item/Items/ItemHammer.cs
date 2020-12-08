namespace Item.Items {
    public class ItemHammer: ItemBase {
        public ItemHammer(int count) : base(count) {
        }

        public override int ItemId => 8;
        public override int MaxStack => 1;
        public override ItemType[] Type => new [] { ItemType.Hammer };
        public override string Name => "Hammer";

        public override string Description => "You feel like you became the Thor.\n" +
                                              "<color=#00c0ff>The time it takes to find item in the trash bin decreases.</color>";
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Hammer";
        }
    }
}