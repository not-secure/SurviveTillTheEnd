using System.Linq;
using Block;
using Common;
using Entity;
using Entity.Neutral;
using Item;
using UI.Status;
using UnityEngine;
using World;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;

        public GameObject gameManager;

        public CraftManager Craft;
        public WorldManager World;
        public EntityManager Entities;
        public readonly Inventory Inventory = new Inventory(30);

        public KeyCode[] keyMap = new[] {
            KeyCode.LeftControl,
            KeyCode.LeftShift,
            KeyCode.Q,
            KeyCode.E,
            KeyCode.R,
            KeyCode.F
        };
        
        private GameManager _manager;
        private UIStatusManager _statusManager;
        private BlockController _interactingBlock;
        private int _interactingId;

        public void Start() {
            Craft = new CraftManager(this);

            _manager = gameManager.GetComponent<GameManager>();
            World = _manager.World;

            _statusManager = GameObject.FindGameObjectWithTag("StatusManager")
                .GetComponent<UIStatusManager>();

            Entities = GameObject.FindGameObjectWithTag("EntityManager")
                .GetComponent<EntityManager>();
        }

        public void Update() {
            var horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
            var vertical = Input.GetAxis("Vertical") * moveSpeed * -1;

            gameObject.transform.Translate(new Vector3(0, vertical * Time.deltaTime, 0));
            gameObject.transform.Rotate(new Vector3(0, 0, horizontal));

            for (var i = 0; i < 6; i++) {
                var key = keyMap[i];
                if (!Input.GetKey(key)) continue;
                
                var item = Inventory.Get(i);
                item?.UseItem(this);
            }

            if (_interactingBlock) {
                // Cancel if key is no longer pressed, or player is too far
                if (Input.GetKey(KeyCode.Space) && _interactingBlock.CanInteract(this))
                    return;
                
                _statusManager.CancelItem(_interactingId);
                _interactingBlock = null;
                _interactingId = -1;
            } else {
                if (!Input.GetKey(KeyCode.Space)) return;
                // Start interaction
                
                var interactableBlock = GetInteractableBlock();
                if (!interactableBlock) return;
                
                _interactingBlock = interactableBlock;
                _interactingId = _statusManager.AddProgress(
                    _interactingBlock.GetInteractProgress(this), 
                    _interactingBlock.GetInteractDuration(this), 
                    OnInteractFinish
                );
            }
        }

        public BlockController GetInteractableBlock() {
            var playerChunk = World.PlayerChunk;
            
            foreach (var block in playerChunk.ActiveBlocks())
                if (block.CanInteract(this))
                    return block;
            
            var chunks = World.GetNeighborChunks(playerChunk);
            return chunks
                .SelectMany(chunk => chunk.ActiveBlocks())
                .FirstOrDefault(block => block.CanInteract(this));
        }

        private void OnInteractFinish() {
            _interactingId = -1;
            var finishedBlock = _interactingBlock;
            _interactingBlock = null;
            
            finishedBlock.OnInteract(this);
        }

        public void GiveItemOrDrop(ItemBase addingItem) {
            var leftItem = Inventory.AddItem(addingItem);
            var position = transform.position;

            if (leftItem != null) {
                EntityItem.DropItem(Entities, position, leftItem);
            }
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
