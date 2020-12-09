using System.Collections;
using Common;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemSilverSword1: ItemBase {
        public ItemSilverSword1(int count) : base(count) {
        }
        
        public override int ItemId => 23;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Sword, ItemType.Silver };
        public override string Name => "Silver Sword";
        public override int RequiredStamina => 10;
        public override float Cooltime => 1f;
        public override string Description => "Your best negotiator friend.\n" +
                                              "<color=#00c0ff>You may attack those enemy with this</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Knife";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            SingletonMonoBehaviour.GetInstance()
                .StartCoroutine(UseCoroutine(player));
        }

        private IEnumerator UseCoroutine(PlayerController player) {
            player.StartAttack(1);
            player.GameManager.Enemies.AttackInRange(player.transform, 120, 6, 24);
            yield return new WaitForSeconds(0.5f);
            player.EndAttack();
        }
    }
}