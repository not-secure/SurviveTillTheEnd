using Player;

namespace Event {
    public abstract class EventBase {
        public abstract void OnExecute(PlayerController player);
    }
}