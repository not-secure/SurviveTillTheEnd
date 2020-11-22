using UnityEngine;

namespace Entity.Friendly {
    public class EntityRabbit: EntityBase {
        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefab/Entities/Rabbit");
            return prefab;
        }
    }
}