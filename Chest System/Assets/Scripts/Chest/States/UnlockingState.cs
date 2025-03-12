using ChestSystem.StateMachine;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockingState : IState
    {
        private float remainingTime;
        private ChestController chestController;
        private ChestStateMachine chestStateMachine;

        public UnlockingState(ChestController chestController, ChestStateMachine chestStateMachine)
        {
            this.chestController = chestController;
            this.chestStateMachine = chestStateMachine;
        }

        public void OnStateEnter()
        {
            GameService.Instance.chestService.SetIsChestUnlocking(true);
            chestController.SetChestStateText("Unlocking");
            remainingTime = chestController.GetRemainingTimeInSeconds();
        }

        public void Update()
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                chestController.SetRemainingTime(remainingTime / 60);
                chestController.SetTimerText(remainingTime);
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;

                chestController.SetRemainingTime(remainingTime / 60);
                chestController.SetTimerText(remainingTime);
                chestStateMachine.ChangeState(ChestState.Unlocked);
            }
        }

        public void OnStateExit()
        {
            GameService.Instance.chestService.SetIsChestUnlocking(false);
        }
    }
}
