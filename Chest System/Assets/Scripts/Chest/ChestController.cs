using ChestSystem.Commands;
using ChestSystem.StateMachine;
using ChestSystem.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

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
        public bool isCountingStarted = false;
        private int GemsRequiredToUnlockChest;
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

        }

        public void UnSubscribeToEvents()
        {

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
                    chestModel.SetRemainingTime(chestModel.GetChestTimer());
                    return entry.Key;
                }
            }
            return ChestScriptableObject.ChestType.Common;
        }

        public void UnlockChestWithGems()
        {
            ICommand openChestWithGemsCommand = new OpenChestWithGemsCommand();

            GameService.Instance.commandInvoker.ProcessCommands(this, openChestWithGemsCommand);
        }

        public void UndoUnlockChestWithGems()
        {
            GameService.Instance.commandInvoker.Undo();
        }

        public void EnableUnlockSelection()
        {
            unlockSelectionUIController.SetUnlockChestSelection(GetGemsRequiredToUnlockCount(),
                chestModel.GetCurrentChestType().ToString(), this);
        }

        public int GetGemsRequiredToUnlockCount()
        {
            float timer = chestModel.GetRemainingTime();

            float gemsRequired = timer / 10f;

            GemsRequiredToUnlockChest = (int)Math.Ceiling(gemsRequired);

            return GemsRequiredToUnlockChest;
        }

        public string FormatTime(float time)
        {
            float timeInSeconds = time;

            int hours = (int)(timeInSeconds / 3600);

            int minutes = (int)((timeInSeconds % 3600) / 60);

            int seconds = (int)(timeInSeconds % 60);

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        public void SetTimerText()
        {
            chestView.SetTimerText(GetRemainingTimeInSeconds());
        }

        public float GetRemainingTimeInSeconds()
        {
            return chestModel.GetRemainingTime() * 60;
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

        public void ChangeState(ChestState state)
        {

            if (CurrentChestState() != stateMachine.GetStates()[state])
            {
                stateMachine.ChangeState(state);
            }
        }

        public void SetChestStateText(string state)
        {
            chestView.SetChestStateText(state);
        }

        public void SetRemainingTime(float time)
        {
            chestModel.SetRemainingTime(time);
        }

        public void DisableTimerText()
        {
            chestView.DisableTimerText();
        }


    }
}
