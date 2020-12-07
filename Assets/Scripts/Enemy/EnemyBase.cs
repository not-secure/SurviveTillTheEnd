using Player;
using UnityEngine;

namespace Enemy {
    public class EnemyBase : MonoBehaviour
    {
        public BoxCollider boxCollider;
        public GameObject player;

        public void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
            player = FindObjectOfType<PlayerController>().gameObject;
        }
    }
}