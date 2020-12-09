using Entity.Neutral;
using Item;
using Item.Items;
using Player;
using UI.Status;
using UnityEngine;

namespace Block {
    public class BlockVendingMachineController: BlockController {
        public static DroprateTable Table;
        public BlockVendingMachineController() {
            Initialize();
        }

        public static void Initialize() {
            if (Table != null) return;
            
            Table = new DroprateTable();
            Table.AddDrop(new ItemCider(1), 30);
            Table.AddDrop(new ItemCola(1), 25);
            Table.AddDrop(new ItemDkPepper(1), 25);
            Table.AddDrop(new ItemStaminaAde(1), 30);
        }

        public override float Cooltime => 1f;
        public override string GetInteractDescription(PlayerController player) {
            return "Take a drink";
        }

        public override string GetInteractProgress(PlayerController player) {
            return "Taking a drink from the vending machine";
        }

        public override float GetInteractDuration(PlayerController player) {
            if (player.Inventory.GetCount(ItemType.Coin) == 0)
                return 0.3f;

            return 2f;
        }

        public override void OnInteract(PlayerController player) {
            base.OnInteract(player);
            if (!player.Inventory.RemoveItem(new ItemCoin(1))) {
                var statusManager = GameObject.FindGameObjectWithTag("StatusManager")
                    .GetComponent<UIStatusManager>();

                statusManager.AddText("You need a coin to take a drink from the vending machine.", 3f);
                return;
            }
            
            EntityItem.DropItem(
                player.Entities,
                transform.position + new Vector3(0, 2f, 0),
                Table.GetDrop()
            );
        }
    }
}