using System.Collections;
using Common;
using Entity.Neutral;
using Player;
using UnityEngine;

namespace Item.Items {
    public class ItemSilverSword3: ItemBase {
        public ItemSilverSword3(int count) : base(count) {
        }
        
        public override int ItemId => 20;
        public override int MaxStack => 1;
        public override ItemType[] Type => new[] { ItemType.Sword, ItemType.Silver };
        public override string Name => "Silver Sword (+2)";
        public override int RequiredStamina => 7;
        public override float Cooltime => 1f;

        public override string Description => "Your best negotiator friend. (Level 2)\n" +
                                              "<color=#00c0ff>You may attack those enemy with this</color>";
        
        protected override string GetTextureKey() {
            return "Sprites/Items/Weapon & Tool/Silver Sword";
        }

        public override void OnUseItem(PlayerController player) {
            base.OnUseItem(player);

            SingletonMonoBehaviour.GetInstance()
                .StartCoroutine(UseCoroutine(player));
        }

        private IEnumerator UseCoroutine(PlayerController player) {
            player.StartAttack(3);
            player.GameManager.Enemies.AttackInRange(player.transform, 120, 7, 34);
            yield return new WaitForSeconds(0.5f);
            player.EndAttack();
        }
    }
}