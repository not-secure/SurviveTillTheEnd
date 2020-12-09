using Entity.Neutral;
using Event;
using Item.Items;
using Player;
using UI.Status;
using UnityEngine;

namespace Block {
    public class BlockPostboxController: BlockController {
        public override void OnInteract(PlayerController player) {
            base.OnInteract(player);

            if (player.Inventory.GetCount(ItemLetter.Id) > 0) {
                UIStatusManager.GetInstance()?.AddText("You already have a letter!", 1.0f);
                return;
            }

            if (!EventManager.GetInstance().CanClaim(player)) {
                UIStatusManager.GetInstance()?.AddText("You already have claimed today's attachments", 1.0f);
                return;
            }
            
            EntityItem.DropItem(
                player.Entities,
                transform.position + new Vector3(0, 4f, 0),
                new ItemLetter(1)
            );
        }

        public override string GetInteractDescription(PlayerController player) {
            return "Claim today's attachments";
        }

        public override string GetInteractProgress(PlayerController player) {
            return "Finding the Letter in the Postbox";
        }

        public override float GetInteractDuration(PlayerController player) {
            return 2;
        }
    }
}