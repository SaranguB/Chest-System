
using ChestSystem.StateMachine;

namespace ChestSystem.Chest
{
    public class LockedState : IState
    {
        private ChestController chestController;

        public LockedState(ChestController chestController, ChestStateMachine chestStateMachine)
        {
            this.chestController = chestController;
        }

        public void OnStateEnter()
        {
            chestController.SetTimerText();
            chestController.SetChestStateText("Locked");
        }

        public void Update()
        {

        }

        public void OnStateExit()
        {
        }
    }
}
