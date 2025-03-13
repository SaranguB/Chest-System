using ChestSystem.StateMachine;

namespace ChestSystem.Chest
{
    public class UnlockedState : IState
    {
        private ChestStateMachine chestStateMachine;
        private ChestController chestController;

        public UnlockedState(ChestController chestController, ChestStateMachine chestStateMachine)
        {
            this.chestController = chestController;
            this.chestStateMachine = chestStateMachine;
        }

        public void OnStateEnter()
        {
            GameService.Instance.SoundService.PlaySound(Sound.Sounds.ChestUnlocked);
            chestController.SetChestStateText("Unlocked");
        }

        public void Update()
        {
        }

        public void OnStateExit()
        {
        }
    }
}
