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

            var result = EventManager.GetInstance().ClaimItem(
                player, transform.position + new Vector3(0, 4f, 0),
                out var count
            );

            if (result) return;
            if (count > 0) {
                UIStatusManager.GetInstance()?.AddText("There were no letters but something came out", 2.5f);
                return;
            }
            
            UIStatusManager.GetInstance()?.AddText("The postbox is empty.", 1.0f);
        }

        public override string GetInteractDescription(PlayerController player) {
            return "Find today's letter";
        }

        public override string GetInteractProgress(PlayerController player) {
            return "Finding the Letter in the Postbox";
        }

        public override float GetInteractDuration(PlayerController player) {
            return 2;
        }
    }
}