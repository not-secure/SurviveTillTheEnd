using UnityEngine;

namespace Entity {
    public abstract class EntityBase {
        public int id;
        public GameObject entity;
        
        public EntityBase() {
            this.id = -1;
        }

        public Vector3 GetPosition() {
            return this.entity.transform.position;
        }

        public GameObject GetObject() {
            return this.entity;
        }

        public abstract Object GetPrefab();
        public void Spawn(Transform transform) {
            if (this.entity)
                return;
            
            var prefab = this.GetPrefab();
            this.entity = (GameObject) Object.Instantiate(prefab, transform);
            
            var controller = this.entity.GetComponent<EntityController>();
            controller.entity = this;
        }
    }
}