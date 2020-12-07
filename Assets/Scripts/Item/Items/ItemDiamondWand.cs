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

            var position = player.transform.position;
            var ent = player.Entities.SpawnEntity<EntityDiamondProjectile>(position.x, position.y, position.z);
            var rb = ent.Entity.GetComponent<Rigidbody>();

            var rot = player.transform.rotation.y;
            var force = new Vector3(Mathf.Cos(rot), 0, Mathf.Sin(rot));
            rb.AddForce(force * 3.0f);
        }
    }
}