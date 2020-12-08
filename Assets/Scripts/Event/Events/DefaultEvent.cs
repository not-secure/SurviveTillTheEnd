using Item.Items;
using Player;

namespace Event.Events {
    public class DefaultEvent: EventBase {
        public override void OnExecute(PlayerController player) {
            player.GiveItemOrDrop(new ItemMagicalPowder(2));
        }
    }
}