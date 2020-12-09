using Player;
using UnityEngine;

namespace Block {
    public abstract class BlockController: MonoBehaviour {
        private const float InteractDistance = 5f;
        private float _lastInteraction;

        public virtual float Cooltime => 5f;
        public virtual int RequiredStamina => 20;
        
        public virtual bool CanInteract(PlayerController player) {
            if (Time.time - _lastInteraction < Cooltime) return false;
            if (player.Stamina < RequiredStamina + 10) return false;
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

        public virtual void OnStartInteract(PlayerController player) {
        }
        
        public virtual void OnInteract(PlayerController player) {
            _lastInteraction = Time.time;
            player.Stamina -= RequiredStamina;
        }
    }
}