using UnityEngine;
using World;

namespace Entity {
    public abstract class EntityBase {
        public readonly float Friction = .2f;
        public readonly float Speed = 1f;
        
        public int ID = -1;
        public GameObject Entity;
        public WorldManager World;
        public Vector3 Motion;

        public Vector3 Position => this.Entity.transform.position;
        public int x => Mathf.RoundToInt(this.Position.x / World.Width);
        public int y => Mathf.RoundToInt(this.Position.y / World.Height);

        public virtual void OnInit() {
            
        }
        
        public virtual void OnTick() {
            Motion *= (1 - Friction);
            this.Entity.transform.position += Motion;
        }

        public virtual bool MoveTowards(Vector2Int target) {
            var position = this.Entity.transform.position;
            
            this.Entity.transform.position =
                Vector3.MoveTowards(
                    position,
                    new Vector3(target.x * World.Width, position.y, target.y * World.Height),
                    Speed * Time.deltaTime
                );

            if (this.x == target.x && this.y == target.y) {
                return true;
            }

            return false;
        }

        public abstract Object GetPrefab();
        public void Spawn(Transform transform) {
            if (Entity)
                return;
            
            var prefab = GetPrefab();
            Entity = (GameObject) Object.Instantiate(prefab, transform);
            
            var controller = Entity.GetComponent<EntityController>();
            controller.Entity = this;
        }
    }
}