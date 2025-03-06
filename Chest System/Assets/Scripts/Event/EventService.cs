using UnityEngine;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnGameStartedEvent;
        public EventController OnSlotAddedEvent;
        public EventService()
        {
            OnSlotAddedEvent = new EventController();
            OnGameStartedEvent = new EventController();
        }
    }
}
