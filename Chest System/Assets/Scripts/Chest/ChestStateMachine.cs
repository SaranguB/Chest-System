using ChestSystem.StateMachine;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestStateMachine
    {
        private IState currentState;

        private Dictionary<ChestState, IState> states;

        public ChestStateMachine(ChestController chestController)
        {
            CreateStates(chestController);
        }

        private void CreateStates(ChestController chestController)
        {
            states = new Dictionary<ChestState, IState>()
            {
                {ChestState.Locked, new LockedState()},
                {ChestState.Unlocked, new UnlockedState() },
                {ChestState.Unlocking, new UnlockingState(chestController) },
                {ChestState.Collected, new CollectedState() }
            };
        }

        public void ChangeState(ChestState newState)
        {
            if (states.ContainsKey(newState))
                ChangeState(states[newState]);
        }

        private void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public IState GetCurrentState() => currentState;
    }
}