
namespace ChestSystem.Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        public PlayerController(PlayerView playerView)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel();
        }

        public void SetGemsCount(int count) => playerModel.SetGemsCount(count);
        public int GetGemsCount() => playerModel.GetGemsCount();

        public void SetCoinCount(int count) => playerModel.SetCoinCount(count);
        public int GetCoinCount() => playerModel.GetCoinCount();
    }
}
