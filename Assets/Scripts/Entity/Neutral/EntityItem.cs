using Item;
using UnityEngine;

namespace Entity.Neutral {
    public class EntityItem: EntityBase {
        private ItemBase _item;
        
        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefabs/Entities/Rabbit");
            return prefab;
        }

        public void SetItem(ItemBase item) {
            _item = item;
        }
    }
}