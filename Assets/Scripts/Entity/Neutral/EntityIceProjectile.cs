using UnityEngine;

namespace Entity.Neutral {
    public class EntityIceProjectile: EntityBase {
        public float Lifetime = 5f;
        
        public override Object GetPrefab() {
            return Resources.Load("Prefabs/Entities/IceProjectile");
        }

        private float _born;
        
        public override void OnInit() {
            base.OnInit();
            Friction = 0.0f;
            _born = Time.time;
        }

        public override void OnTick() {
            base.OnTick();
            if (Time.time - _born > Lifetime)
                Kill();
        }

        public override void OnCollisionEnter(GameObject other) {
            Kill();
        }
    }
}