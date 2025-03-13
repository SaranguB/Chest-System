namespace ChestSystem.Player
{
    public class PlayerModel
    {
        private int gemsCount;
        private int coinCount;
        private int initialPlayerGemsCount = 20;
        private int initialPlayerCoinCount = 100;

        public PlayerModel()
        {
            gemsCount = initialPlayerGemsCount;
            coinCount = initialPlayerCoinCount;
        }

        public void SetGemsCount(int count) 
            => gemsCount = count;

        public int GetGemsCount()
            => gemsCount;

        public void SetCoinCount(int count)
            => coinCount = count;

        public int GetCoinCount() 
            => coinCount;



    }
}
