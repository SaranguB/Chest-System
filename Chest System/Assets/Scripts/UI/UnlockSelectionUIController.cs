using ChestSystem.Chest;
using System;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UnlockSelectionUIController
    {
        private UnlockChestSelectionUIView unlockSelectionUIView;
        private ChestController currentChestController;
        public UnlockSelectionUIController(UnlockChestSelectionUIView unlockSelectionUIView)
        {
            this.unlockSelectionUIView = unlockSelectionUIView;
        }

        public void SetUnlockChestSelection(int gemsRequiredCount, string chestType, ChestController chestController)
        {
            currentChestController = chestController;

            unlockSelectionUIView.SetUnlockChestSelection(gemsRequiredCount, chestType, chestController.CurrentChestState());

            unlockSelectionUIView.GetstartTimerButton().onClick.RemoveAllListeners();
            unlockSelectionUIView.GetUnlockChestWithGemsButton().onClick.RemoveAllListeners();
            unlockSelectionUIView.GetUndoButton().onClick.RemoveAllListeners();

            unlockSelectionUIView.GetstartTimerButton().onClick.AddListener(SetTimer);

            unlockSelectionUIView.GetUnlockChestWithGemsButton().onClick.AddListener(SetUnlockChestWithGemsButton);
            unlockSelectionUIView.GetUndoButton().onClick.AddListener(SetUndoButton);

      
        }

        private void SetUndoButton()
        {
            currentChestController.UndoUnlockChestWithGems();
            DisableUnlockSelection();
        }

        private void SetUnlockChestWithGemsButton()
        {
            currentChestController.UnlockChestWithGems();
            DisableUnlockSelection();
        }

        public void DisableUnlockSelection()
        {
            unlockSelectionUIView.DisableUnlockSelection();
        }

        public void SetTimer()
        {
            currentChestController.ChangeState(ChestState.Unlocking);
            DisableUnlockSelection();
        }
    }
}
