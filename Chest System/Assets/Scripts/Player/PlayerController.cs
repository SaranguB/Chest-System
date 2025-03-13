namespace ChestSystem.Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;

        public PlayerController(PlayerView playerView)
        {
            this.playerView = playerView;
            this.playerModel = new PlayerModel();
            this.playerView.SetController(this);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
            => GameService.Instance.eventService.onRewardCollectedEvent.AddListener(SetTotalGemsAndCoinsCount);

        public void Dispose()
            => UnSubscibeToEvents();

        private void UnSubscibeToEvents()
            => GameService.Instance.eventService.onRewardCollectedEvent.RemoveListener(SetTotalGemsAndCoinsCount);


        public void SetTotalGemsAndCoinsCount(int gems, int coins)
        {
            int totalGems = gems + GetGemsCount();
            int totalCoins = coins + GetCoinCount();

            SetGemsCount(totalGems);
            SetCoinCount(totalCoins);
        }

        public void SetGemsCount(int count)
        {
            playerModel.SetGemsCount(count);
            playerView.DisplayGemsCount();
        }

        public int GetGemsCount() 
            => playerModel.GetGemsCount();

        public void SetCoinCount(int count)
        {
            playerModel.SetCoinCount(count);
            playerView.DisplayCoinsCount();

        }

        public int GetCoinCount()
            => playerModel.GetCoinCount();
    }
}
