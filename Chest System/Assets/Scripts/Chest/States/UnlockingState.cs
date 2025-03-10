
using ChestSystem.StateMachine;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class UnlockingState : IState
    {
        private float remainingTime;
        ChestController chestController;
        public UnlockingState(ChestController chestController)
        {
            this.chestController = chestController;
        }

        public void OnStateEnter()
        {
            remainingTime = chestController.GetTimeInSeconds();
        }

        public void Update()
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                chestController.SetTimerText(remainingTime);
            }
        }

        public void OnStateExit()
        {

        }
    }
}
