using ChestSystem.Events;
using UnityEngine;

namespace ChestSystem
{
    public class GameService : GenericMonoSingelton<GameService>
    {
        public EventService eventService { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            eventService = new EventService();
        }
    }
}
