using UnityEngine;

namespace Entity.Neutral {
    public class EntityItem: EntityBase {
        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefabs/Entities/Rabbit");
            return prefab;
        }
    }
}