using Entity.Neutral;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemDiamondWand: ItemBase {
        public ItemDiamondWand(int count) : base(count) {
        }

        public override int ItemId => 4;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Magic, ItemType.Wand, ItemType.Diamond, ItemType.Wooden };
        public override string Name => "Diamond Wand";
        public override int RequiredStamina => 30;

        public override string Description => "*freezing* beautiful magical wand.\n" +
                                              "<color=#00c0ff>Fires a *freezing* beautiful projectile.</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Sapphire Staff";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            var transform = player.transform;
            var position = transform.position;
            
            var rot = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;

            var direction = new Vector3(Mathf.Sin(rot), 0, Mathf.Cos(rot));
            position += direction * 1.5f;
            
            var ent = player.Entities.SpawnEntity<EntityIceProjectile>(
                position.x, position.y + 1f, position.z
            );
            ent.Motion = direction * 0.3f; 
        }
    }
}