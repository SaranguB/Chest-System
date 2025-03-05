using UnityEngine;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnGameStatedEvent;

        public EventService()
        {
            OnGameStatedEvent = new EventController();
        }
    }
}
