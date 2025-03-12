using UnityEngine;

namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnGameStartedEvent;
        public EventController<GameObject> OnChestGeneratedEvent;
        public EventController OnTimerStartedEvent;
        public EventController<int,int> OnRewardCollectedEvent;
        public EventService()
        {
            OnChestGeneratedEvent = new EventController<GameObject>();
            OnGameStartedEvent = new EventController();
            OnTimerStartedEvent = new EventController();
            OnRewardCollectedEvent = new EventController<int, int>();
        }
    }
}
