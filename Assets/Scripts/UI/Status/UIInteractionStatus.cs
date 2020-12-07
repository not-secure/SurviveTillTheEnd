using Player;
using TMPro;
using UnityEngine;

namespace UI.Status {
    public class UIInteractionStatus: MonoBehaviour {
        public TextMeshProUGUI text;
        
        private PlayerController _player;
        
        private void OnEnable() {
            _player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerController>();

            text.autoSizeTextContainer = true;
        }

        private void Update() {
            var interactableBlock = _player.GetInteractableBlock();
            if (interactableBlock) {
               text.SetText(interactableBlock.GetInteractDescription(_player));
            }
        }
    }
}