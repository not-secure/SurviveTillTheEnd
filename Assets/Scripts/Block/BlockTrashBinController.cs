using Entity.Neutral;
using Item;
using Item.Items;
using Player;
using UnityEngine;

namespace Block {
    public class BlockTrashBinController: BlockController {
        public static DroprateTable Table;
        
        public BlockTrashBinController(): base() {
            Initialize();
        }

        public static void Initialize() {
            if (Table != null) return;
            
            Table = new DroprateTable();
            Table.AddDrop(new ItemApple(1), 10);
            Table.AddDrop(new ItemPlank(3), 20);
            Table.AddDrop(new ItemSilverKey(2), 20);
            Table.AddDrop(new ItemSilverKey(3), 10);
            Table.AddDrop(new ItemGear(5), 10);
        }

        public override string GetInteractDescription(PlayerController player) {
            return "Go through the trash bin";
        }

        public override float GetInteractDuration(PlayerController player) {
            if (player.Inventory.GetCount(ItemType.Hammer) > 0)
                return Random.Range(2, 3);

            return Random.Range(5, 6);
        }

        public override string GetInteractProgress(PlayerController player) {
            return "Going through the trash bin";
        }

        public override void OnInteract(PlayerController player) {
            base.OnInteract(player);
            EntityItem.DropItem(
                player.Entities,
                transform.position + new Vector3(0, 4f, 0),
                Table.GetDrop()
            );
        }
    }
}