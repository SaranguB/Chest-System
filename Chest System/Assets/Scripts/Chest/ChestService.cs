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

        public void GenerateChest(SlotsUIController slotUIController)
        {
            if (slotUIController.CheckAnySlotAvailble())
                chestController = new ChestController(chestScriptableObject, chestPrefab, slotUIController);

        }

        public ChestController GetChest()
        {
            return chestController;
        }
    }
}
