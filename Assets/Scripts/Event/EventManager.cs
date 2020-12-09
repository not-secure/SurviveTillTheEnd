using System.Collections.Generic;
using Event.Events;
using Item;
using Item.Items;
using Player;
using UI.Dialog;
using UI.Note;
using UI.Status;
using UnityEngine;

namespace Event {
    public struct DayInfo {
        public DayInfo(int day, IEnumerable<EventBase> events, IEnumerable<ItemBase> attachments) {
            Day = day;
            Events = events;
            Attachments = attachments;
        }
        
        public int Day { get; }
        public IEnumerable<EventBase> Events { get; }
        public IEnumerable<ItemBase> Attachments { get; }
    }
    
    public class EventManager {
        private Dictionary<int, DayInfo> _eventTable = new Dictionary<int, DayInfo>();
        private readonly DayInfo _defaultEvent;
        private static EventManager _instance;
        private UIDialogManager _dialogManager;

        public EventManager() {
            // TODO add events
            _instance = this;
            _dialogManager = GameObject.FindGameObjectWithTag("DialogManager")
                .GetComponent<UIDialogManager>();

            _defaultEvent = new DayInfo(
                -1,
                new EventBase[] { }, 
                new[] { new ItemMagicalPowder(2) }
            );
        }

        private int _lastDate = -1;
        
        public void ExecuteEvents(PlayerController player) {
            var date = player.GameManager.DayCounter;
            if (date <= _lastDate)
                return;
            
            _lastDate = date;

            var letterCount = player.Inventory.GetCount(ItemLetter.Id);
            if (letterCount > 0) {
                UIStatusManager.GetInstance()?.AddText("Your outdated letters have been disposed!", 1f);
                player.Inventory.RemoveItem(new ItemLetter(letterCount));
            }
            
            var dialog = _dialogManager.OpenIfNoneOpened(DialogKey.Note, _dialogManager.note);
            dialog.GetComponent<UINote>().SetDay(player.GameManager.DayCounter);
            
            if (!_eventTable.TryGetValue(date, out var todayInfo))
                todayInfo = _defaultEvent;
            
            foreach (var todayEvent in todayInfo.Events) {
                todayEvent.OnExecute(player);
            }
        }
        
        private int _lastClaimDate = -1;

        public bool CanClaim(PlayerController player) {
            var date = player.GameManager.DayCounter;
            return date > _lastClaimDate;
        }
        
        public void ClaimItems(PlayerController player) {
            if (!CanClaim(player)) {
                UIStatusManager.GetInstance()?.AddText("You already have claimed today's attachments", 1f);
                return;
            }

            var date = player.GameManager.DayCounter;
            _lastClaimDate = date;
            
            if (!_eventTable.TryGetValue(date, out var todayInfo))
                todayInfo = _defaultEvent;
            
            foreach (var todayItem in todayInfo.Attachments) {
                player.GiveItemOrDrop((ItemBase) todayItem.Clone());
            }

            UIStatusManager.GetInstance()?.AddText("You have claimed today's attachments", 2.5f);
        }
        
        public static EventManager GetInstance() {
            return _instance ?? (_instance = new EventManager());
        }
    }
}