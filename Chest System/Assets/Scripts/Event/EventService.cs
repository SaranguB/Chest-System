namespace ChestSystem.Events
{
    public class EventService
    {
        public EventController OnChestNotUnlockedWithGemsEvent;
        public EventController<int,int> OnRewardCollectedEvent;

        public EventService()
        {
            OnChestNotUnlockedWithGemsEvent = new EventController();
            OnRewardCollectedEvent = new EventController<int, int>();
        }
    }
}
