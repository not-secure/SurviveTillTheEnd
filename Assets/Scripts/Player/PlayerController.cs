using System;
using Common;
using Entity.Neutral;
using Item;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;

        public GameObject gameManager;

        private GameManager _manager;
        public readonly Inventory Inventory = new Inventory(32);

        public void GiveItemOrDrop(ItemBase addingItem) {
            var leftItem = Inventory.AddItem(addingItem);
            var position = transform.position;
            _manager.World.EntityManager.SpawnEntity<EntityItem>(
                position.x, position.y, position.z
            );
        }

        public void Start() {
            _manager = gameManager.GetComponent<GameManager>();
        }

        public void Update() {
            float horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
            float vertical = Input.GetAxis("Vertical") * moveSpeed * -1;

            gameObject.transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0));
            gameObject.transform.Rotate(new Vector3(0, 0, horizontal));
        }
    }
}
