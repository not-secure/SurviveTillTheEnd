using Player;
using TMPro;
using UnityEngine;

namespace UI.Status {
    public class UIInteractionStatus: MonoBehaviour {
        public TextMeshProUGUI text;

        private CanvasGroup _group;
        private PlayerController _player;
        
        private void OnEnable() {
            _player = GameObject.FindGameObjectWithTag("Player")
                .GetComponent<PlayerController>();

            _group = GetComponent<CanvasGroup>();
            text.autoSizeTextContainer = true;
        }

        private void Update() {
            if (!_player.InteractingBlock) {
                _group.alpha = 1;
                
                var interactableBlock = _player.GetInteractableBlock();
                if (interactableBlock) {
                   text.SetText(interactableBlock.GetInteractDescription(_player));
                   return;
                }
            }

            _group.alpha = 0;
        }
    }
}