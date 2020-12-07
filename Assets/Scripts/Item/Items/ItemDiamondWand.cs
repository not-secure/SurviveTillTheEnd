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
            
            var rot = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;

            var direction = new Vector3(Mathf.Sin(rot), 0, Mathf.Cos(rot));
            position += direction;
            
            var ent = player.Entities.SpawnEntity<EntityIceProjectile>(
                position.x, position.y + 1f, position.z
            );
            ent.Motion = direction * 0.3f; 
        }
    }
}