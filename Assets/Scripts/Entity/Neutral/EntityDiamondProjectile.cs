using UnityEngine;

namespace Entity.Neutral {
    public class EntityDiamondProjectile: EntityBase {
        public override Object GetPrefab() {
            return Resources.Load("Prefabs/Entities/DiamondProjectile");
        }

        public override void OnInit() {
            base.OnInit();
            Friction = 0.0f;
            
            var rb = Entity.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }
}