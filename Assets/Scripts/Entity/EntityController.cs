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
    }
}