using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestController
    {
        private List<ChestScriptableObject> chestScriptableObject;
        private GameObject chestPrefab;
        private ChestView chestView;
        private ChestModel chestModel;
        public ChestController(List<ChestScriptableObject> chestScriptableObject, GameObject chestPrefab)
        {
            this.chestScriptableObject = chestScriptableObject;
            this.chestPrefab = chestPrefab;
            chestModel = new ChestModel(this, chestScriptableObject);
            GenerateChest();
        }

        private void GenerateChest()
        {
            GameObject newChest = GameObject.Instantiate(chestPrefab);
            Transform parentTransform = GameService.Instance.uiService.GetSlots().GetChestSlotPosition();

            newChest.transform.SetParent(parentTransform, false);
            newChest.transform.localPosition = Vector3.zero;

            SetChestView(newChest);
        }

        private void SetChestView(GameObject newChest)
        {
            chestView = newChest.GetComponent<ChestView>();
            chestView.SetController(this);
        }

        public Sprite GetChestImage(ChestScriptableObject.ChestType chestType)
        {
            return chestModel.GetChestImage(chestType);
        }
    }
}
