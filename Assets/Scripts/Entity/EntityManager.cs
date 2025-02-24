﻿using System.Collections.Generic;
using Common;
using UnityEngine;
using World;

namespace Entity {
    public class EntityManager: MonoBehaviour {
        private WorldManager _world;
        private GameManager _game;
        private readonly Dictionary<int, EntityBase> _entities = new Dictionary<int, EntityBase>();

        public void OnEnable() {
            _game = GameObject.FindGameObjectWithTag("GameManager")
                .GetComponent<GameManager>();
            
            _world = GameObject.FindGameObjectWithTag("WorldManager")
                .GetComponent<WorldManager>();
        }

        public EntityBase GetEntity(int eid) {
            var hasEntity = _entities.TryGetValue(eid, out var entity);
            return hasEntity ? entity : null;
        }

        public TEntity SpawnEntity<TEntity>(float x, float y, float z)
            where TEntity: EntityBase, new() {
            
            return SpawnEntity<TEntity>(x, y, z, null);
        }

        public TEntity SpawnEntity<TEntity>(float x, float y, float z, Quaternion? rotation)
            where TEntity: EntityBase, new() {
            
            var id = GetLastEntityId();
            var entity = new TEntity {
                ID = id,
                World = _world,
                GameManager = _game,
                EntityManager = this
            };
            entity.Spawn(transform);
            entity.Entity.transform.position = new Vector3(x, y, z);
            if (rotation.HasValue) {
                entity.Entity.transform.rotation = rotation.Value;
            }
            entity.OnInit();
            _entities.Add(id, entity);

            return entity;
        }

        public void KillEntity(EntityBase ent) {
            _entities.Remove(ent.ID);
            ent.OnDead();
        }

        private int GetLastEntityId() {
            var i = 0;
            while (_entities.ContainsKey(i)) {
                i++;
            }

            return i;
        }
    }
}