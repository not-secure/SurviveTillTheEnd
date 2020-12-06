using Common;
using Entity.Neutral;
using Item;
using Item.Items;
using UI.Status;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;

        public GameObject gameManager;

        private GameManager _manager;
        public CraftManager Craft;
        public readonly Inventory Inventory = new Inventory(30);

        public void GiveItemOrDrop(ItemBase addingItem) {
            var leftItem = Inventory.AddItem(addingItem);
            var position = transform.position;

            if (leftItem != null) {
                EntityItem.DropItem(_manager.World.EntityManager, position, leftItem);
            }
        }

        public void Start() {
            Craft = new CraftManager(this);
            _manager = gameManager.GetComponent<GameManager>();
        }

        public void Update() {
            float horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
            float vertical = Input.GetAxis("Vertical") * moveSpeed * -1;

            gameObject.transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0));
            gameObject.transform.Rotate(new Vector3(0, 0, horizontal));
        }

        public void SetDead() {
            // TODO
        }

        private int _maxHealth = 100;
        public int MaxHealth {
            get => _maxHealth;
            set {
                _maxHealth = value;
                if (Health > _maxHealth) Health = _maxHealth;
            }
        }

        private int _health = 100;
        public int Health {
            get => _health;
            set {
                _health = value;
                if (_health <= 0) {
                    SetDead();
                }
            }
        }

        private int _maxStamina = 100;

        public int MaxStamina {
            get => _maxStamina;
            set {
                _maxStamina = value;
                if (Stamina > _maxStamina) Stamina = _maxStamina;
            }
        }

        public int Stamina = 100;
    }
}
