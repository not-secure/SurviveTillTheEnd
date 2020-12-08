using System.Collections;
using Entity.Neutral;
using Item;
using Player;
using UnityEngine;

namespace Enemy {
    public class EnemyBase : MonoBehaviour
    {
        public BoxCollider boxCollider;
        public GameObject player;

        private PlayerController _playerController;
        private EnemyManager _enemyManager;

        public void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            _playerController = FindObjectOfType<PlayerController>();
            player = _playerController.gameObject;

            _enemyManager = FindObjectOfType<EnemyManager>();
            _health = MaxHealth;
        }

        private float _deadAt = -1;
        private float _health = 1;
        private float _lastAttack = 0f;
        private Vector3 _motion = Vector3.zero;
        private const float Friction = 0.1f;
        private const float Drag = (1f - Friction);

        public virtual float Knockback => .5f;
        public virtual float PlayerKnockback => 0.3f;
        public virtual int Damage => 0;
        public virtual float AttackDuration => 1f;
        public virtual float MaxHealth => 100f;
        public virtual DroprateTable DroprateTable => new DroprateTable();
        
        public void Hurt(float damage, Vector3 damageSource) {
            if (_deadAt > 0)
                return;
            
            var displacement = (transform.position - damageSource).normalized;
            
            _motion += new Vector3(
                displacement.x * Knockback, Knockback, displacement.z * Knockback
            );

            _health -= damage;
            if (!(_health < 0)) return;
            
            EntityItem.DropItem(_playerController.Entities, transform.position, DroprateTable.GetDrop());
            _deadAt = Time.time;
        }

        public void Update() {
            var enemyTransform = transform;
            if (_deadAt > 0) {
                _motion.y += 1f * Time.deltaTime;

                if (Time.time - _deadAt > 3f)
                    Kill();

                return;
            }

            if (Time.time - _lastAttack < AttackDuration) return;
            
            var displacement = player.transform.position - enemyTransform.position;
            if (!(displacement.magnitude < 3f)) return;
            
            _lastAttack = Time.time;
            
            displacement.y = 1.0f;
            StartCoroutine(PlayerHurtCoroutine(displacement.normalized * PlayerKnockback));
        }

        public IEnumerator PlayerHurtCoroutine(Vector3 knockback) {
            for (var i = 0; i < 5; i++) {
                _playerController.Health -= Damage;
                _playerController.Controller.Move(knockback);
                yield return new WaitForSeconds(0.01f);
            }
        }

        public void Kill() {
            _enemyManager.KillEnemy(this);
        }

        public void FixedUpdate() {
            transform.position += _motion;
            _motion *= Drag;
        }
    }
}