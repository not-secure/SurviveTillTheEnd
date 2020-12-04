using Item;
using UnityEngine;

namespace Entity.Neutral {
    public class EntityItem: EntityBase {
        private ItemBase _item;
        private EntityItemController _controller;

        public override Object GetPrefab() {
            Object prefab = Resources.Load("Prefabs/Entities/Item");
            return prefab;
        }

        public override void OnInit() {
            base.OnInit();
            
            _controller = Entity.GetComponent<EntityItemController>();
        }

        public void SetItem(ItemBase item) {
            if (!_controller)
                return;
            
            _item = item;
            _controller.SetTexture(item.GetImage().texture);
        }

        public void AddRandomMotion() {
            if (!_controller)
                return;
            
            _controller.AddRandomMotion();
        }

        public static void DropItem(EntityManager entityManager, Vector3 position, ItemBase item) {
            if (item.Count == 0) return;
            
            var entityItem = entityManager.SpawnEntity<EntityItem>(
                position.x, position.y, position.z
            );
            entityItem.SetItem(item);
            entityItem.AddRandomMotion();
        }
    }
}