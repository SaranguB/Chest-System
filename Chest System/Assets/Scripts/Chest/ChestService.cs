using ChestSystem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

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
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {

        }

        public void GenerateChest(SlotsUIController slotUIController, UnlockChestSelectionUIController unlockSelectionUIController)
        {
            if (slotUIController.CheckAnySlotAvailble())
            {
                chestController = chestPool.GetChest(slotUIController, unlockSelectionUIController);
                chestController.EnableChest();
                chestController.SetChest();
            }

        }

        public ChestController GetChest()
        {
            return chestController;
        }

        public void SetIsChestUnlocking(bool value)
        {
            isChestUnlocking = value;
        }

        public bool GetIsChestUnlocking()
        {
            return isChestUnlocking;
        }

        public void ReturnChestToPool(ChestController chestController)
        {
            chestPool.ReturnToPool(chestController);
        }
    }
}
