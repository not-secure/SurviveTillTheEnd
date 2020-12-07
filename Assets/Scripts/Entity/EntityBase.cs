using System;
using UnityEngine;
using World;
using Object = UnityEngine.Object;

namespace Entity {
    public abstract class EntityBase {
        public float Friction = .2f;
        public float Speed = 1f;
        
        public int ID = -1;
        public GameObject Entity;
        public EntityManager EntityManager;
        public WorldManager World;
        public Vector3 Motion = Vector3.zero;

        public Vector3 Position => this.Entity.transform.position;
        public int x => Mathf.RoundToInt(this.Position.x / World.Width);
        public int y => Mathf.RoundToInt(this.Position.y / World.Height);

        
        protected CharacterController Controller;
        protected Rigidbody Rigidbody;

        public virtual void OnInit() {
            Controller = Entity.GetComponent<CharacterController>();
            Rigidbody = Entity.GetComponent<Rigidbody>();
        }
        
        public virtual void OnTick() { }

        public virtual void OnFixedTick() {
            Motion *= (1 - Friction);
            if (Controller)
                Controller.Move(Motion);
            else if (Rigidbody)
                Rigidbody.MovePosition(Entity.transform.position + Motion);
            else
                Entity.transform.position += Motion;
        }
        
        public virtual void OnCollisionEnter(GameObject other) { }

        [Obsolete]
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

        public virtual void OnDead() {
            if (Entity)
                Object.Destroy(Entity);
        }

        public void Kill() {
            EntityManager.KillEntity(this);
        }
    }
}