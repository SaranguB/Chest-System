using ChestSystem.Chest;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class GameService : GenericMonoSingelton<GameService>
    {
        public ChestService chestService;
        public UIService uiService;
        public PlayerService playerService;

        [Header("Chest")]
        [SerializeField] private List<ChestScriptableObject> chestScriptableObject;
        [SerializeField] private GameObject chestPrefab;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        public EventService eventService { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            playerService = new PlayerService(playerView);
            chestService = new ChestService(chestScriptableObject, chestPrefab);
            eventService = new EventService();
        }
    }
}
