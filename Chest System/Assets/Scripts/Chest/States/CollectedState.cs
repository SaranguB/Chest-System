
using ChestSystem.StateMachine;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        private ChestStateMachine chestStateMachine;
        private ChestController chestController;

        public CollectedState(ChestController chestController, ChestStateMachine chestStateMachine)
        {
            this.chestController = chestController;
            this.chestStateMachine = chestStateMachine;
        }

        public void OnStateEnter()
        {
            chestController.SetChestStateText("Collected");

        }
        public void Update()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
