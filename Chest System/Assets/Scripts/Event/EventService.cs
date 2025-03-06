using UnityEngine;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnGameStartedEvent;
        public EventController<GameObject> OnChestGeneratedEvent;
        public EventService()
        {
            OnChestGeneratedEvent = new EventController<GameObject>();
            OnGameStartedEvent = new EventController();
        }
    }
}
