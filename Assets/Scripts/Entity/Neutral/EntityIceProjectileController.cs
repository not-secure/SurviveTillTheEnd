using UnityEngine;

namespace Entity.Neutral {
    public class EntityIceProjectileController: EntityController {
        public void OnEnable() {
            var player = GameObject.FindGameObjectWithTag("Player");
            
            Physics.IgnoreCollision(
                gameObject.GetComponentInChildren<Collider>(),
                player.GetComponentInChildren<Collider>()
            );
        }
    }
}