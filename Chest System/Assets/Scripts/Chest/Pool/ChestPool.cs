using ChestSystem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestPool
    {
        private List<PooledChest> pooledChests = new List<PooledChest>();

        private List<ChestScriptableObject> chestScriptableObject;
        private GameObject chestPrefab;

        public ChestPool(List<ChestScriptableObject> chestScriptableObject, GameObject chestPrefab)
        {
            this.chestScriptableObject = chestScriptableObject;
            this.chestPrefab = chestPrefab;
        }

        public ChestController GetChest(SlotsUIController slotUIController,
            UnlockSelectionUIController unlockSelectionUIController)
        {
            if (pooledChests.Count > 0)
            {
                PooledChest pooledChest = pooledChests.Find(item => !item.isUsed);

                if (pooledChest != null)
                {
                    pooledChest.isUsed = true;
                    return pooledChest.chest;
                }
            }
            return CreateChest(slotUIController, unlockSelectionUIController);
        }

        private ChestController CreateChest(SlotsUIController slotUIController,
            UnlockSelectionUIController unlockSelectionUIController)
        {
            PooledChest pooledChest = new PooledChest();

            pooledChest.chest = new ChestController(chestScriptableObject, chestPrefab,
                slotUIController, unlockSelectionUIController);
            pooledChest.isUsed = true;

            pooledChests.Add(pooledChest);

            return pooledChest.chest;
        }

        public void ReturnToPool(ChestController returnedChest)
        {
            PooledChest pooledChest = pooledChests.Find(item => item.chest.Equals(returnedChest));
            pooledChest.isUsed = false;
        }

        public class PooledChest
        {
            public ChestController chest;
            public bool isUsed;
        }
    }
}
