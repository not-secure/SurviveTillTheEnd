using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity {
    public class EntityManager {
        private Dictionary<int, EntityBase> entities = new Dictionary<int, EntityBase>();
        private GameObject baseEntity;

        public EntityManager(GameObject baseEntity) {
            this.baseEntity = baseEntity;
        }

        public EntityBase GetEntity(int eid) {
            bool hasEntity = entities.TryGetValue(eid, out var entity);
            if (hasEntity) {
                return entity;
            }

            return null;
        }

        public TEntity SpawnEntity<TEntity>(float x, float y, float z)
            where TEntity: EntityBase, new() {
            
            return SpawnEntity<TEntity>(x, y, z, Quaternion.identity);
        }

        public TEntity SpawnEntity<TEntity>(float x, float y, float z, Quaternion rotation)
            where TEntity: EntityBase, new() {
            
            var id = GetLastEntityId();
            var entity = new TEntity {
                id = id
            };
            entity.Spawn(baseEntity.transform);
            entities.Add(id, entity);

            return entity;
        }

        public int GetLastEntityId() {
            int i = 0;
            while (entities.ContainsKey(i)) {
                i++;
            }

            return i;
        }
    }
}