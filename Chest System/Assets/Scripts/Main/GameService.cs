using ChestSystem.Chest;
using ChestSystem.Events;
using ChestSystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class GameService : GenericMonoSingelton<GameService>
    {
        public ChestService chestService;
        public UIService uiService;

        [Header("Chest")]
        [SerializeField] private List<ChestScriptableObject> chestScriptableObject;
        [SerializeField] private GameObject chestPrefab;


        public EventService eventService { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            chestService = new ChestService(chestScriptableObject, chestPrefab);
            eventService = new EventService();
        }
    }
}
