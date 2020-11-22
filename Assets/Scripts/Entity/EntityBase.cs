using UnityEngine;

namespace Entity {
    public abstract class EntityBase {
        public int id = -1;
        public GameObject entity;
        
        public Vector3 GetPosition() {
            return this.entity.transform.position;
        }

        public GameObject GetObject() {
            return this.entity;
        }

        public virtual void OnTick() {
            
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