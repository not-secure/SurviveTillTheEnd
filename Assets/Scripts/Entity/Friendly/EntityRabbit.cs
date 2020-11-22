using UnityEngine;

namespace Entity.Friendly {
    public class EntityRabbit: EntityBase {
        public EntityRabbit(int id) : base(id) {
        }

        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefab/Entities/Rabbit");
            return prefab;
        }
    }
}