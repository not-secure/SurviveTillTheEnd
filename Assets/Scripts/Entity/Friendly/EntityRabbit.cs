using UnityEngine;

namespace Entity.Friendly {
    public class EntityRabbit: EntityLiving {
        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefabs/Entities/Rabbit");
            return prefab;
        }
    }
}