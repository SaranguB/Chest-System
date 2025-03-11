using ChestSystem.Actions;
using ChestSystem.Chest;
using ChestSystem.Commands;
using ChestSystem.Events;
using ChestSystem.Player;
using ChestSystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class GameService : GenericMonoSingelton<GameService>
    {
        public ChestService chestService { get; private set; }
        public UIService uiService { get; private set; }
        public PlayerService playerService { get; private set; }
        public ActionService actionService { get; private set; }
        public EventService eventService { get; private set; }
        public CommandInvoker commandInvoker { get; private set; }

        [Header("Chest")]
        [SerializeField] private List<ChestScriptableObject> chestScriptableObject;
        [SerializeField] private GameObject chestPrefab;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
       

        protected override void Awake()
        {
            base.Awake();
            eventService = new EventService();
            playerService = new PlayerService(playerView);
            actionService = new ActionService();
            commandInvoker = new CommandInvoker(playerService);
            chestService = new ChestService(chestScriptableObject, chestPrefab);
        }
    }
}
