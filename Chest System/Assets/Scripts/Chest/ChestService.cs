using ChestSystem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        private ChestController chestController;

        private List<ChestScriptableObject> chestScriptableObject;
        private GameObject chestPrefab;

        public ChestService(List<ChestScriptableObject> chestScriptableObject, GameObject chestPrefab)
        {
            this.chestScriptableObject = chestScriptableObject;
            this.chestPrefab = chestPrefab;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {

        }

        public void GenerateChest(SlotsUIController slotUIController, UnlockSelectionUIController unlockSelectionUIController)
        {
            if (slotUIController.CheckAnySlotAvailble())
                chestController = new ChestController(chestScriptableObject, chestPrefab, slotUIController, unlockSelectionUIController);

        }

        public ChestController GetChest()
        {
            return chestController;
        }
    }
}
