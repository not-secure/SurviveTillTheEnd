using System;
using UnityEngine;

namespace Entity {
    public class EntityController : MonoBehaviour {
        public EntityBase entity;

        public void Update() {
            entity?.OnTick();
        }
    }
}