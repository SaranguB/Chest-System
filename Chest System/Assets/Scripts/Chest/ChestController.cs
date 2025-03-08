using ChestSystem.StateMachine;
using ChestSystem.UI;
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
        private SlotsUIController slotUIController;
        private ChestStateMachine stateMachine;


        public ChestController(List<ChestScriptableObject> chestScriptableObject, GameObject chestPrefab,
            SlotsUIController slotUIController)
        {
            this.slotUIController = slotUIController;
            this.chestScriptableObject = chestScriptableObject;
            this.chestPrefab = chestPrefab;

            chestModel = new ChestModel(this, chestScriptableObject);
            GenerateChest();
            createStateMachine();
            stateMachine.ChangeState(ChestState.Locked);
        }

        private void createStateMachine()
        {
            stateMachine = new ChestStateMachine();
        }

        private void GenerateChest()
        {
            GameObject newChest = GameObject.Instantiate(chestPrefab);
            Transform parentTransform = slotUIController.GetChestSlotPosition();

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

        public ChestScriptableObject.ChestType GetRandomChestType()
        {
            Dictionary<ChestScriptableObject.ChestType, float> chestTypeChance = chestModel.GetChestTypeChance();
            float totalWeight = 0f;

            foreach (var value in chestTypeChance.Values)
            {
                totalWeight += value;
            }

            float randomvalue = UnityEngine.Random.Range(0, totalWeight);
            float cumilativeWeight = 0f;

            foreach (var entry in chestTypeChance)
            {
                cumilativeWeight += entry.Value;

                if (randomvalue < cumilativeWeight)
                {
                    return entry.Key;
                }
            }
            return ChestScriptableObject.ChestType.Common;
        }
    }
}
