using ChestSystem.UI;
using System.Collections.Generic;
using ChestSystem.Sound;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        private ChestController chestController;
        private ChestPool chestPool;
        private bool isChestUnlocking = false;

        public ChestService(List<ChestScriptableObject> chestScriptableObject, ChestView chestPrefab)
        {
            chestPool = new ChestPool(chestScriptableObject, chestPrefab);
        }

        public void GenerateChest(SlotsUIController slotUIController, UnlockChestSelectionUIController unlockSelectionUIController)
        {
            if (slotUIController.CheckAnySlotAvailble())
            {
                chestController = chestPool.GetChest(slotUIController, unlockSelectionUIController);
                chestController.EnableChest();
                chestController.SetChest();
                GameService.Instance.SoundService.PlaySound(Sounds.ChestGenerated);
            }
            else
            {
                GameService.Instance.eventService.onSlotNotAvailableEvent.InvokeEvent();
                GameService.Instance.SoundService.PlaySound(Sounds.PopUpSound);
            }
        }

        public ChestController GetChest() => chestController;
   
        public void SetIsChestUnlocking(bool value) => isChestUnlocking = value;

        public bool GetIsChestUnlocking() => isChestUnlocking;

        public void ReturnChestToPool(ChestController chestController) => chestPool.ReturnToPool(chestController);
    }
}
