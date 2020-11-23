using UnityEngine;

namespace Entity {
    public abstract class EntityBase {
        public float friction = .2f;
        public float speed = 1f;
        
        public int id = -1;
        public GameObject entity;
        public WorldManager world;
        public Vector3 motion;

        public Vector3 Position => this.entity.transform.position;
        public int x => Mathf.RoundToInt(this.Position.x / world.Width);
        public int y => Mathf.RoundToInt(this.Position.y / world.Height);

        public virtual void OnInit() {
            
        }
        
        public virtual void OnTick() {
            motion *= (1 - friction);
            this.entity.transform.position += motion;
        }

        public virtual bool MoveTowards(Vector2Int target) {
            var position = this.entity.transform.position;
            
            this.entity.transform.position =
                Vector3.MoveTowards(
                    position,
                    new Vector3(target.x * world.Width, position.y, target.y * world.Height),
                    speed * Time.deltaTime
                );

            if (this.x == target.x && this.y == target.y) {
                return true;
            }

            return false;
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