using Player;
using UnityEngine;

namespace Block {
    public abstract class BlockController: MonoBehaviour {
        private const float InteractDistance = 10f;
        
        public virtual bool CanInteract(PlayerController player) {
            var distance = (transform.position - player.transform.position).magnitude;
            return distance < InteractDistance;
        }

        public virtual float GetInteractDuration(PlayerController player) {
            return 5;
        }

        public virtual string GetInteractDescription(PlayerController player) {
            return "Interact with Block";
        }

        public virtual string GetInteractProgress(PlayerController player) {
            return "Interacting";
        }
        
        public abstract void OnInteract(PlayerController player);
    }
}