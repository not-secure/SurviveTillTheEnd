using Enemy;
using Player;

namespace Event.Events {
    public class ToggleLightDeathEvent: EventBase {
        private bool _toggle;
        public ToggleLightDeathEvent(bool toggle) : base() {
            _toggle = toggle;
        }
        
        public override void OnExecute(PlayerController player) {
            EnemyBase.DieOnLight = _toggle;
        }
    }
}