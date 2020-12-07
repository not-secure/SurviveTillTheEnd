using Entity.Neutral;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemDiamondWand: ItemBase {
        public ItemDiamondWand(int count) : base(count) {
        }

        public override int ItemId => 4;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Wand };
        public override string Name => "Diamond Wand";

        public override string Description => "*deadly* beautiful magical wand.\n" +
                                              "<color=#00c0ff>Fires a *deadly* beautiful projectile.</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Sapphire Staff";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            var transform = player.transform;
            var position = transform.position;
            
            var playerRot = transform.rotation.eulerAngles;
            var rot = Quaternion.Euler(0, playerRot.y, 0);
            var rotY = playerRot.y * Mathf.Deg2Rad;
            
            var ent = player.Entities.SpawnEntity<EntityDiamondProjectile>(
                position.x, position.y + 0.5f, position.z, rot
            );
            ent.Motion = new Vector3(Mathf.Sin(rotY), 0, Mathf.Cos(rotY)) * 0.3f; 

            var rb = ent.Entity.GetComponent<Rigidbody>();
        }
    }
}