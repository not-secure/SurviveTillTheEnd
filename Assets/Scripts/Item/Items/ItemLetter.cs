using Event;
using Player;
using UI.Dialog;
using UI.Note;
using UI.Status;
using UnityEngine;

namespace Item.Items {
    public class ItemLetter: ItemBase {
        public ItemLetter(int count) : base(count) {
        }

        public const int Id = 13;
        
        public override int ItemId => 13;
        public override int MaxStack => 1;
        public override ItemType[] Type => new [] { ItemType.Burnable };
        public override string Name => "Today's Letter";

        public override string Description => "A letter for you.\n" +
                                              "<color=#ffc000>Story Item</color>";

        public override bool IsConsumed => true;

        protected override string GetTextureKey() {
            return "Sprites/Items/Misc/Envelope";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);
            
            // Remove all other letters
            player.Inventory.RemoveItem(new ItemLetter(player.Inventory.GetCount(ItemId)));
            
            EventManager.GetInstance().ReadLetter(player);
        }
    }
}