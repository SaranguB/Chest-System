namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController onChestNotUnlockedWithGemsEvent;
        public EventController<int,int> onRewardCollectedEvent;
        public EventController onSlotNotAvailableEvent;
        public EventService()
        {
            onChestNotUnlockedWithGemsEvent = new EventController();
            onRewardCollectedEvent = new EventController<int, int>();
            onSlotNotAvailableEvent = new EventController();
        }

        
    }
}
