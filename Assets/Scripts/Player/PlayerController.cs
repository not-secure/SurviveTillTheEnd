﻿using System;
using System.Linq;
using Block;
using Common;
using Entity;
using Entity.Neutral;
using Item;
using UI.Status;
using UnityEngine;
using UnityEngine.VFX;
using World;

namespace Player {
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;
        public float rotateSpeed;

        public int staminaRegeneration = 1;

        public GameObject gameManager;
        public VisualEffect vfxSlash;
        public VisualEffect vfxDash;

        [NonSerialized] public CraftManager Craft;
        [NonSerialized] public WorldManager World;
        [NonSerialized] public EntityManager Entities;
        [NonSerialized] public readonly Inventory Inventory = new Inventory(30);

        public KeyCode[] keyMap = new[] {
            KeyCode.LeftControl,
            KeyCode.LeftShift,
            KeyCode.Q,
            KeyCode.E,
            KeyCode.R,
            KeyCode.F
        };

        private UIStatusManager _statusManager;
        private int _interactingId;
        
        private Animator _animator;
        private int _animMove;
        private int _animAttack;
        
        [NonSerialized]
        public CharacterController Controller;
        public BlockController InteractingBlock { get; private set; }

        public void Start() {
            Craft = new CraftManager(this);

            GameManager = gameManager.GetComponent<GameManager>();
            World = GameManager.World;

            _statusManager = GameObject.FindGameObjectWithTag("StatusManager")
                .GetComponent<UIStatusManager>();

            Entities = GameObject.FindGameObjectWithTag("EntityManager")
                .GetComponent<EntityManager>();

            Controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _animMove = Animator.StringToHash("Move");
            _animAttack = Animator.StringToHash("IsAttack");
        }

        public void Update() {
            if (Health <= 0) {
                World.GameOver();
                return;
            }

            if (transform.position.y < -5)
            {
                World.GameOver();
                return;
            }

            var horizontal = Input.GetAxis("Horizontal") * rotateSpeed;
            var vertical = Input.GetAxis("Vertical") * moveSpeed;

            _animator.SetInteger(_animMove, vertical == 0 ? 0 : 1);

            Controller.Move(
                gameObject.transform.rotation *
                new Vector3(0, -9.8f * Time.deltaTime, vertical * Time.deltaTime)
            );
            gameObject.transform.Rotate(new Vector3(0, horizontal, 0));

            if (Stamina < MaxStamina && Time.time - _lastStaminaUse > TimeToStaminaFill) {
                if (Time.time - _lastStaminaHeal > 0.05f) {
                    Stamina = Math.Min(
                        MaxStamina,
                        Stamina + staminaRegeneration
                    );
                    _lastStaminaHeal = Time.time;
                }
            }
            
            for (var i = 0; i < 6; i++) {
                var key = keyMap[i];
                if (!Input.GetKey(key)) continue;
                
                var item = Inventory.Get(i);
                item?.UseItem(this);
            }

            if (InteractingBlock) {
                // Cancel if key is no longer pressed, or player is too far
                if (Input.GetKey(KeyCode.Space) && InteractingBlock.CanInteract(this))
                    return;
                
                _statusManager.CancelItem(_interactingId);
                InteractingBlock = null;
                _interactingId = -1;
            } else {
                if (!Input.GetKey(KeyCode.Space)) return;
                // Start interaction
                
                var interactableBlock = GetInteractableBlock();
                if (!interactableBlock) return;
                
                InteractingBlock = interactableBlock;         

                InteractingBlock.OnStartInteract(this);
                _interactingId = _statusManager.AddProgress(
                    InteractingBlock.GetInteractProgress(this), 
                    InteractingBlock.GetInteractDuration(this), 
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
            var finishedBlock = InteractingBlock;
            InteractingBlock = null;
            
            finishedBlock.OnInteract(this);
        }

        public void GiveItemOrDrop(ItemBase addingItem) {
            var leftItem = Inventory.AddItem(addingItem);
            var position = transform.position;

            if (leftItem != null) {
                EntityItem.DropItem(Entities, position, leftItem);
            }
        }

        public void StartAttack(int isImproved) {
            _animator.SetBool(_animAttack, true);
            vfxSlash.SetInt("SwordLevel", isImproved);
            vfxSlash.Play();
        }

        public void EndAttack() {
            _animator.SetBool(_animAttack, false);
            vfxSlash.Stop();
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

        private const float TimeToStaminaFill = 6f;
        private float _lastStaminaUse = 0f;
        private float _lastStaminaHeal = 0f;
        private int _stamina = 100;
        public int Stamina {
            get => _stamina;
            set {
                if (_stamina > value)
                    _lastStaminaUse = Time.time;

                _stamina = value;
            }
        }

        public GameManager GameManager { get; private set; }
    }
}
