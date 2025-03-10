using ChestSystem.Chest;
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

        public void SetUnlockChestSelection(int gemsCount, string chestType, ChestController chestController)
        {
            currentChestController = chestController;

            unlockSelectionUIView.SetUnlockChestSelection(gemsCount, chestType);

            unlockSelectionUIView.GetstartTimerButton().onClick.AddListener(SetTimer);
        }

        public void DisableUnlockSelection()
        {
            unlockSelectionUIView.DisableUnlockSelection();
        }

        public void SetTimer()
        {
            currentChestController.SetStateToUnlocking();
            unlockSelectionUIView.DisableUnlockSelection();
        }
    }
}
