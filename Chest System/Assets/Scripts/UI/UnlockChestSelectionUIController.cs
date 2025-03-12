using ChestSystem.Chest;
using System;
using UnityEngine;

namespace ChestSystem.UI
{
    public class UnlockChestSelectionUIController
    {
        private UnlockChestSelectionUIView unlockSelectionUIView;
        private ChestController currentChestController;
        private SlotsUIController currentSlotController;
        private SlotsUIView currentSlot;

        public UnlockChestSelectionUIController(UnlockChestSelectionUIView unlockChestSelectionUIView)
        {
            this.unlockSelectionUIView = unlockChestSelectionUIView;
        }

        public void SetUnlockChestSelection(int gemsRequiredCount, string chestType,
            ChestController chestController, SlotsUIController slotUIController, SlotsUIView currentSlot)
        {
            currentChestController = chestController;
            this.currentSlotController = slotUIController;
            this.currentSlot = currentSlot;

            unlockSelectionUIView.SetUnlockChestSelection(gemsRequiredCount, chestType, chestController.CurrentChestState());
            RemoveListeners();
            AdddListeners();

        }

        private void RemoveListeners()
        {
            unlockSelectionUIView.GetstartTimerButton().onClick.RemoveAllListeners();
            unlockSelectionUIView.GetUnlockChestWithGemsButton().onClick.RemoveAllListeners();
            unlockSelectionUIView.GetUndoButton().onClick.RemoveAllListeners();
            unlockSelectionUIView.GetCollectButton().onClick.RemoveAllListeners();
        }

        private void AdddListeners()
        {
            unlockSelectionUIView.GetstartTimerButton().onClick.AddListener(SetTimer);
            unlockSelectionUIView.GetUnlockChestWithGemsButton().onClick.AddListener(SetUnlockChestWithGemsButton);
            unlockSelectionUIView.GetUndoButton().onClick.AddListener(SetUndoButton);
            unlockSelectionUIView.GetCollectButton().onClick.AddListener(SetCollectButton);
        }

        private void SetCollectButton()
        {
            currentChestController.Collect();
            DisableUnlockSelection();
            currentSlotController.SetIsSlotHasAChest(currentSlot, true);
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
            DisableUnlockSelection();

            if (!GameService.Instance.chestService.GetIsChestUnlocking())
            {
                currentChestController.ChangeState(ChestState.Unlocking);
            }
            else
            {
                unlockSelectionUIView.EnableChestAlreadyUnlockingPanel();
            }
        }
        public void SetCollectedValues(int collectedGems, int collectedCoins)
        {
            unlockSelectionUIView.SetCollectedValues(collectedGems, collectedCoins);
        }
    }
}
