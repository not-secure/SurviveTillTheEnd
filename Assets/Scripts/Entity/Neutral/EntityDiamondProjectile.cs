using UnityEngine;

namespace Entity.Neutral {
    public class EntityDiamondProjectile: EntityBase {
        public override Object GetPrefab() {
            return Resources.Load("Prefabs/Entities/DiamondProjectile");
        }

        public override void OnInit() {
            base.OnInit();
            
            var rb = Entity.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.drag = 0.0f;
        }
    }
}