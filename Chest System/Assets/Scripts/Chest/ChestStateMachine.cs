using ChestSystem.StateMachine;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class ChestStateMachine
    {
        private IState currentState;
        public Dictionary<ChestState, IState> states;
        private ChestState currentChestStateEnum;

        public ChestStateMachine(ChestController chestController)
        {
            CreateStates(chestController);
        }

        private void CreateStates(ChestController chestController)
        {
            states = new Dictionary<ChestState, IState>()
            {
                {ChestState.Locked, new LockedState(chestController, this)},
                {ChestState.Unlocked, new UnlockedState(chestController, this) },
                {ChestState.Unlocking, new UnlockingState(chestController, this) },
                {ChestState.Collected, new CollectedState(chestController, this) }
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

        public void Update() => currentState?.Update();

        public IState GetCurrentState() => currentState;

        public Dictionary<ChestState, IState> GetStates() => states;
    }
}