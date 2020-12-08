using System.Collections;
using Common;
using Entity.Neutral;
using Player;
using UnityEngine;
using UnityEngine.VFX;

namespace Item.Items {
    public class ItemEmeraldWand: ItemBase {
        public ItemEmeraldWand(int count) : base(count) {
        }
        
        public override int ItemId => 19;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Magic, ItemType.Wand, ItemType.Wooden, ItemType.Emerald };
        public override string Name => "Emerald Wand";
        public override int RequiredStamina => 7;
        
        public override string Description => "*Hasty* magical wand.\n" +
                                              "<color=#00c0ff>Makes an outstanding move.</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Emerald Staff";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            var vfx = player.vfxDash;
            SingletonMonoBehaviour.GetInstance()
                .StartCoroutine(UseCoroutine(player, vfx));
        }

        private IEnumerator UseCoroutine(PlayerController player, VisualEffect vfx) {
            var rot = Mathf.Deg2Rad * player.transform.rotation.eulerAngles.y;
            // rot += Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")); 
            
            var direction = new Vector3(Mathf.Sin(rot), 0, Mathf.Cos(rot));
            
            vfx.Play();
            for (var i = 0; i < 10; i++) {
                player.Controller.Move(direction * 0.5f);
                yield return new WaitForSeconds(0.05f);
            }

            vfx.Stop();
        }
    }
}