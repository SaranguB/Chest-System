
using ChestSystem.StateMachine;

namespace ChestSystem.Chest
{
    public class CollectedState : IState
    {
        private ChestStateMachine chestStateMachine;

        public CollectedState(ChestStateMachine chestStateMachine)
        {
            this.chestStateMachine = chestStateMachine;
        }

        public void OnStateEnter()
        {

        }
        public void Update()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
