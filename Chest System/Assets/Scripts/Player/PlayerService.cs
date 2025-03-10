using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerService
    {
        private PlayerView playerView;
        private PlayerController playerController;

        public PlayerService(PlayerView playerView)
        {
            this.playerView = playerView;
            playerController = new PlayerController(playerView);
        }

        public PlayerController GetPlayerController()
        {
            return playerController;
        }
    }
}
