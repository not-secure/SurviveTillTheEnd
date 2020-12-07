namespace Item.Items {
    public class ItemApple: ItemBase {
        public ItemApple(int count) : base(count) {
        }

        public override int ItemId => 3;
        public override int MaxStack => 16;
        public override ItemType[] Type => new[] { ItemType.Food };
        public override string Name => "Apple";

        public override string Description =>
            "You should be good at English to use this item.\n" +
            "<color=#00c0ff>Heals HP</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Food/Apple";
        }
    }
}