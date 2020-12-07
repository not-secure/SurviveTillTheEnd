using System;
using UnityEngine;

namespace Entity {
    public class EntityController : MonoBehaviour {
        public EntityBase Entity;

        public void Update() {
            Entity?.OnTick();
        }

        public void FixedUpdate() {
            Entity?.OnFixedTick();
        }

        public void OnCollisionEnter(Collision other) {
            Entity?.OnCollisionEnter(other.gameObject);
        }

        public void OnControllerColliderHit(ControllerColliderHit hit) {
            Entity?.OnCollisionEnter(hit.gameObject);
        }
    }
}