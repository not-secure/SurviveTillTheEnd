using System.Collections.Generic;
using Entity.Neutral;
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
                new[] {new ItemMagicalPowder(2)}
            );
            
            _eventTable.Add(7, new DayInfo(
                7, 
                new [] { new ToggleLightDeathEvent(false) }, 
                new [] { new ItemMagicalPowder(2) }
            ));
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

        public bool ClaimItem(PlayerController player, Vector3 dropPosition, out int count) {
            count = 0;
            if (!CanClaim(player)) {
                return false;
            }
            
            var date = player.GameManager.DayCounter;
            _lastClaimDate = date;
            
            if (!_eventTable.TryGetValue(date, out var todayInfo))
                todayInfo = _defaultEvent;

            foreach (var todayItem in todayInfo.Attachments) {
                EntityItem.DropItem(player.Entities, dropPosition, (ItemBase) todayItem.Clone());
                count++;
            }

            var note = Resources.Load<TextAsset>("Texts/Notes/day_" + date);
            if (note)
                EntityItem.DropItem(
                    player.Entities,
                    dropPosition,
                    new ItemLetter(1)
                );

            return note;
        }

        public void ReadLetter(PlayerController player) {
            var date = player.GameManager.DayCounter;
            var note = Resources.Load<TextAsset>("Texts/Notes/day_" + date)?.text ?? "";
            
            var dialog = _dialogManager.OpenIfNoneOpened(DialogKey.Note, _dialogManager.note);
            var dialogController = dialog.GetComponent<UINote>();
            dialogController.SetDay(player.GameManager.DayCounter);
            dialogController.SetContent(note);
        }
        
        public static EventManager GetInstance() {
            return _instance ?? (_instance = new EventManager());
        }
    }
}