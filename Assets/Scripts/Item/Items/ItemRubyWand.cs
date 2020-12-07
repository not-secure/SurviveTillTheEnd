using System.Collections;
using Common;
using Entity.Neutral;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemRubyWand: ItemBase {
        public ItemRubyWand(int count) : base(count) {
        }

        public override int ItemId => 5;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Wand };
        public override string Name => "Ruby Wand";

        public override string Description => "*blazingly* beautiful magical wand.\n" +
                                              "<color=#00c0ff>Fires a *blazingly* beautiful projectile.</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Ruby Staff";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            SingletonMonoBehaviour.GetInstance()
                .StartCoroutine(UseCoroutine(player));
        }

        private IEnumerator UseCoroutine(PlayerController player) {
            var rot = Mathf.Deg2Rad * player.transform.rotation.eulerAngles.y;
            
            var direction = new Vector3(Mathf.Sin(rot), 0, Mathf.Cos(rot));
            var position = player.transform.position;

            for (var i = 0; i < 3; i++) {
                position += direction * 3;
                var entity = player.Entities.SpawnEntity<EntityFireProjectile>(position.x, position.y, position.z);
                entity.Motion += direction * 0.1f;
                
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}