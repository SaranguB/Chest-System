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
        private UnlockSelectionUIController unlockSelectionUIController;

        public ChestController(List<ChestScriptableObject> chestScriptableObject, GameObject chestPrefab,
            SlotsUIController slotUIController, UnlockSelectionUIController unlockSelectionUIController)
        {
            this.slotUIController = slotUIController;
            this.chestScriptableObject = chestScriptableObject;
            this.chestPrefab = chestPrefab;
            this.unlockSelectionUIController = unlockSelectionUIController;

            chestModel = new ChestModel(this, chestScriptableObject);
            GenerateChest();
            createStateMachine();
            stateMachine.ChangeState(ChestState.Locked);
            SubscribeToEvent();
        }


        private void SubscribeToEvent()
        {
            GameService.Instance.eventService.OnTimerStartedEvent.AddListener(SetStateToUnlocking);
        }

        public void UnSubscribeToEvents()
        {
            GameService.Instance.eventService.OnTimerStartedEvent.RemoveListener(SetStateToUnlocking);
        }

        private void createStateMachine()
        {
            stateMachine = new ChestStateMachine(this);
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
                    chestModel.SetCurrentChestType(entry.Key);
                    return entry.Key;
                }
            }
            return ChestScriptableObject.ChestType.Common;
        }

        public void EnableUnlockSelection()
        {
            unlockSelectionUIController.SetUnlockChestSelection(GetGemsRequiredToUnlockCount(),
                chestModel.GetCurrentChestType().ToString(), this);
        }
        private int GetGemsRequiredToUnlockCount()
        {
            float timer = chestModel.GetChestTimer();

            float gemsRequired = timer / 10f;

            return (int)Math.Ceiling(gemsRequired);
        }

        public string FormatTime(float time)
        {
            float timeInSeconds = time;

            int hours = (int)(timeInSeconds / 3600);

            int minutes = (int)((timeInSeconds % 3600) / 60);

            int seconds = (int)(timeInSeconds % 60);

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public float GetTimeInSeconds()
        {
            return chestModel.GetChestTimer() * 60;
        }

        public void SetTimerText(float timeInSeconds)
        {
            chestView.SetTimerText(timeInSeconds);
        }

        public void UpdateState()
        {
            stateMachine.Update();
        }

        public IState CurrentChestState()
        {
            return stateMachine.GetCurrentState();
        }

        public void SetStateToUnlocking()
        {
            if (CurrentChestState() is not UnlockingState)
                stateMachine.ChangeState(ChestState.Unlocking);
        }
    }
}
