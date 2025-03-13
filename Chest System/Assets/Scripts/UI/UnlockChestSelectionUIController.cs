using ChestSystem.Chest;
using UnityEngine.UI;
using ChestSystem.Sound;

namespace ChestSystem.UI
{
    public class UnlockChestSelectionUIController
    {
        private UnlockChestSelectionUIView unlockSelectionUIView;
        private ChestController currentChestController;
        private SlotsUIController currentSlotController;
        private SlotsUIView currentSlot;
        private bool isChestunlockedWithGems = false;
        public UnlockChestSelectionUIController(UnlockChestSelectionUIView unlockChestSelectionUIView)
        {
            this.unlockSelectionUIView = unlockChestSelectionUIView;
        }

        public void SetUnlockChestSelection(int gemsRequiredCount, string chestType,
            ChestController chestController, SlotsUIController slotUIController, SlotsUIView currentSlot,
            bool isChestunlockedWithGems)
        {
            currentChestController = chestController;
            currentSlotController = slotUIController;
            this.currentSlot = currentSlot;
            this.isChestunlockedWithGems = isChestunlockedWithGems;

            unlockSelectionUIView.SetUnlockChestSelection(gemsRequiredCount, chestType,
                chestController.CurrentChestState(), isChestunlockedWithGems);

            RemoveListeners();
            AdddListeners();
        }

        private void RemoveListeners()
        {
            RemoveListenerToButton(unlockSelectionUIView.GetstartTimerButton());
            RemoveListenerToButton(unlockSelectionUIView.GetUnlockChestWithGemsButton());
            RemoveListenerToButton(unlockSelectionUIView.GetUnlockChestWithGemsButton());
            RemoveListenerToButton(unlockSelectionUIView.GetUndoButton());
            RemoveListenerToButton(unlockSelectionUIView.GetCollectButton());
        }

        private void AdddListeners()
        {
            unlockSelectionUIView.GetstartTimerButton().onClick.AddListener(SetTimer);
            unlockSelectionUIView.GetUnlockChestWithGemsButton().onClick.AddListener(SetUnlockChestWithGemsButton);
            unlockSelectionUIView.GetUndoButton().onClick.AddListener(SetUndoButton);
            unlockSelectionUIView.GetCollectButton().onClick.AddListener(SetCollectButton);
        }

        private void RemoveListenerToButton(Button button)
        {
            button.onClick.RemoveAllListeners();
        }

        private void SetCollectButton()
        {
            PlayButtonPressedSound();
            GameService.Instance.SoundService.PlaySound(Sounds.ChestOpen);
            currentChestController.Collect();
            DisableUnlockSelection();
            currentSlotController.SetIsSlotHasAChest(currentSlot, true);
        }

        private void SetUndoButton()
        {
            PlayButtonPressedSound();
            currentChestController.UndoUnlockChestWithGems();
            DisableUnlockSelection();
        }

        private void SetUnlockChestWithGemsButton()
        {
            currentChestController.UnlockChestWithGems();
            DisableUnlockSelection();
        }

        public void DisableUnlockSelection()
            => unlockSelectionUIView.DisableUnlockSelection();

        public void SetTimer()
        {
            DisableUnlockSelection();
            PlayButtonPressedSound();

            if (!GameService.Instance.chestService.GetIsChestUnlocking())
            {
                currentChestController.ChangeState(ChestState.Unlocking);
            }
            else
            {
                unlockSelectionUIView.EnableChestAlreadyUnlockingPanel();
                GameService.Instance.SoundService.PlaySound(Sounds.PopUpSound);
            }
        }

        private void PlayButtonPressedSound()
        {
            GameService.Instance.SoundService.PlaySound(Sounds.ButtonPressedSound);
        }

        public void SetCollectedValues(int collectedGems, int collectedCoins)
            => unlockSelectionUIView.SetCollectedValues(collectedGems, collectedCoins);

        public void SetIsChestUnlockedWithGems(bool value)
           => unlockSelectionUIView.SetIsChestUnlockedWithGems(value);
    }
}
