using System;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestService
    {
        private ChestController chestController;

        public ChestService()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
           
        }

        private void InstantiateChest()
        {
            chestController = new ChestController();
        }

        public ChestController GetChest()
        {
            return chestController;
        }
    }
}
