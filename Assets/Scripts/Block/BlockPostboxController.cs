using Entity.Neutral;
using Item.Items;
using Player;
using UnityEngine;

namespace Block {
    public class BlockPostboxController: BlockController {
        public override void OnInteract(PlayerController player) {
            base.OnInteract(player);
            
            EntityItem.DropItem(
                player.Entities,
                transform.position + new Vector3(0, 2f, 0),
                new ItemPlank(10)
            );
        }

        public override string GetInteractDescription(PlayerController player) {
            return "Open Postbox";
        }

        public override string GetInteractProgress(PlayerController player) {
            return "Finding Letters in Postbox";
        }

        public override float GetInteractDuration(PlayerController player) {
            return 2;
        }
    }
}