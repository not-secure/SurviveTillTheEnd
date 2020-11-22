using UnityEngine;

namespace Entity {
    public abstract class EntityBase {
        public int id = -1;
        public GameObject entity;
        
        public Vector3 GetPosition() {
            return entity.transform.position;
        }

        public GameObject GetObject() {
            return entity;
        }

        public virtual void OnTick() {
            
        }

        public abstract Object GetPrefab();
        public void Spawn(Transform transform) {
            if (entity)
                return;
            
            var prefab = GetPrefab();
            entity = (GameObject) Object.Instantiate(prefab, transform);
            
            var controller = entity.GetComponent<EntityController>();
            controller.entity = this;
        }
    }
}