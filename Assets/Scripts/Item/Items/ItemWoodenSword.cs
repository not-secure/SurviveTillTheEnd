using System.Collections;
using Common;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemWoodenSword: ItemBase {
        public ItemWoodenSword(int count) : base(count) {
        }
        
        public override int ItemId => 24;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Sword, ItemType.Wooden };
        public override string Name => "Wooden Sword";
        public override int RequiredStamina => 8;
        public override float Cooltime => 1.8f;
        public override string Description => "A good communication tool.\n" +
                                              "<color=#00c0ff>You may attack those enemy with this</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Wooden Sword";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            SingletonMonoBehaviour.GetInstance()
                .StartCoroutine(UseCoroutine(player));
        }

        private IEnumerator UseCoroutine(PlayerController player) {
            player.StartAttack(1);
            player.GameManager.Enemies.AttackInRange(player.transform, 120, 5, 15);
            yield return new WaitForSeconds(0.5f);
            player.EndAttack();
        }
    }
}