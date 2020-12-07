using UnityEngine;

namespace Entity.Neutral {
    public class EntityFireProjectile: EntityBase {

        public override void OnInit() {
            base.OnInit();
            Friction = 0.1f;
            Motion +=  new Vector3(0, 0.9f, 0);
        }

        public override void OnTick() {
            base.OnTick();
            Motion.y -= 1f * Time.deltaTime;

            if (Entity.transform.position.y < -1f) {
                Kill();
            }
        }

        public override Object GetPrefab() {
            return Resources.Load("Prefabs/Entities/FireProjectile");
        }
    }
}