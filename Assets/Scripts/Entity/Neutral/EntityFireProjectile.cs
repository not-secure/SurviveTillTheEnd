using UnityEngine;

namespace Entity.Neutral {
    public class EntityFireProjectile: EntityBase {

        public override void OnInit() {
            base.OnInit();
            Friction = 0.1f;
            Motion +=  new Vector3(0, 0.9f, 0);
        }

        private bool _isAttacked = false;

        public override void OnTick() {
            base.OnTick();
            Motion.y -= 1f * Time.deltaTime;

            if (Entity.transform.position.y > 0f && !_isAttacked) {
                _isAttacked = true;
                GameManager.Enemies.AttackInRange(Entity.transform, 5, 100);
            }
            
            if (Entity.transform.position.y < -1f) {
                Kill();
            }
        }

        public override Object GetPrefab() {
            return Resources.Load("Prefabs/Entities/FireProjectile");
        }
    }
}