﻿using Common;
using Entity.Neutral;
using Item;
using Item.Items;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;

        public GameObject gameManager;

        private GameManager _manager;
        public readonly Inventory Inventory = new Inventory(30);

        public void GiveItemOrDrop(ItemBase addingItem) {
            var leftItem = Inventory.AddItem(addingItem);
            var position = transform.position;
            EntityItem.DropItem(_manager.World.EntityManager, position, addingItem);

            if (leftItem != null) {
                EntityItem.DropItem(_manager.World.EntityManager, position, leftItem);
            }
        }

        public void Start() {
            _manager = gameManager.GetComponent<GameManager>();
        }

        public void Update() {
            float horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
            float vertical = Input.GetAxis("Vertical") * moveSpeed * -1;

            gameObject.transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0));
            gameObject.transform.Rotate(new Vector3(0, 0, horizontal));

            if (Input.GetKey(KeyCode.Alpha1)) {
                GiveItemOrDrop(new ItemPlank(10));
            }
            if (Input.GetKey(KeyCode.Alpha2)) {
                GiveItemOrDrop(new ItemSilver(10));
            }
            if (Input.GetKey(KeyCode.Alpha3)) {
                Inventory.RemoveItem(new ItemPlank(10));
            }
        }
    }
}
