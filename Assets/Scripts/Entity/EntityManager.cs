using System.Collections.Generic;

namespace Entity {
    public class EntityManager {
        private Dictionary<int, EntityBase> entities;

        public EntityBase GetEntity(int eid) {
            bool hasEntity = entities.TryGetValue(eid, out var entity);
            if (hasEntity) {
                return entity;
            }

            return null;
        }
    }
}