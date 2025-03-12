using ChestSystem.Chest;
using ChestSystem.Player;

namespace ChestSystem.Commands
{
    public class OpenChestWithGemsCommand : ICommand
    {
        private ChestController chestController;
        private PlayerController playerController;
        private int GemsRequiredToUnlockCount;
        private int gemsCount;

        public void Execute(PlayerService playerService, ChestController chestController)
        {
            playerController = playerService.GetPlayerController();
            this.chestController = chestController;
            GemsRequiredToUnlockCount = chestController.GetGemsRequiredToUnlockCount();
            gemsCount = playerController.GetGemsCount();

            if (GemsRequiredToUnlockCount <= gemsCount)
            {
                int remainingGems = gemsCount - GemsRequiredToUnlockCount;
                this.chestController.ChangeState(ChestState.Unlocked);
                this.chestController.DisableTimerText();
                this.chestController.SetIsChestUnlockedWithGems(true);
                playerController.SetGemsCount(remainingGems);
                GameService.Instance.actionService.GetOpenChestWithGemsAction().PerformAction();
            }
            else
            {
                GameService.Instance.eventService.OnChestNotUnlockedWithGemsEvent.InvokeEvent();
            }
        }

        public void Undo()
        {
            playerController.SetGemsCount(gemsCount);
            chestController.ChangeState(ChestState.Locked);
            chestController.SetTimerText();
        }
    }
}
