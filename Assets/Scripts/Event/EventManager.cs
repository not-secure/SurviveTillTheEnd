using System.Collections.Generic;
using Event.Events;
using Player;

namespace Event {
    public class EventManager {
        private Dictionary<int, EventBase> _eventTable = new Dictionary<int, EventBase>();
        private readonly EventBase _defaultEvent;
        private static EventManager _instance;

        public EventManager() {
            // TODO add events
            _instance = this;
            _defaultEvent = new DefaultEvent();
        }

        private int _lastDate = -1;
        
        public void ExecuteEvents(PlayerController player) {
            var date = player.GameManager.DayCounter;
            if (date <= _lastDate)
                return;

            _lastDate = date;
            if (!_eventTable.TryGetValue(date, out var todayEvent))
                todayEvent = _defaultEvent;
            
            todayEvent.OnExecute(player);
        }

        public static EventManager GetInstance() {
            return _instance ?? (_instance = new EventManager());
        }
    }
}