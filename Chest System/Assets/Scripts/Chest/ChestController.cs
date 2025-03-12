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
        private ChestView chestView;
        private ChestModel chestModel;
        private SlotsUIController slotUIController;
        private ChestStateMachine stateMachine;
        private UnlockChestSelectionUIController unlockSelectionUIController;
        public bool isCountingStarted = false;
        private int GemsRequiredToUnlockChest;
        private SlotsUIView currentSlot;

        public ChestController(List<ChestScriptableObject> chestScriptableObject, ChestView chestView,
            SlotsUIController slotUIController, UnlockChestSelectionUIController unlockSelectionUIController)
        {
            this.slotUIController = slotUIController;
            this.chestView = chestView;
            this.unlockSelectionUIController = unlockSelectionUIController;

            chestModel = new ChestModel(chestScriptableObject);
            createStateMachine();
        }

        private void createStateMachine()
        {
            stateMachine = new ChestStateMachine(this);
        }

        public void SetChest()
        {
            Transform parentTransform = slotUIController.GetChestSlotPosition();
            currentSlot = slotUIController.GetCurrentSlot();

            chestView.transform.SetParent(parentTransform, false);
            chestView.transform.localPosition = Vector3.zero;
            chestView.SetController(this);

            stateMachine.ChangeState(ChestState.Locked);
        }

        public Sprite GetChestImage(ChestScriptableObject.ChestType chestType) => chestModel.GetChestImage(chestType);

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

        public void UndoUnlockChestWithGems() => GameService.Instance.commandInvoker.Undo();

        public void EnableUnlockSelection() => unlockSelectionUIController.SetUnlockChestSelection(GetGemsRequiredToUnlockCount(),
                chestModel.GetCurrentChestType().ToString(), this, slotUIController, currentSlot);

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

        public void SetTimerText() => chestView.SetTimerText(GetRemainingTimeInSeconds());

        public float GetRemainingTimeInSeconds() => chestModel.GetRemainingTime() * 60;

        public void SetTimerText(float timeInSeconds) => chestView.SetTimerText(timeInSeconds);

        public void UpdateState() => stateMachine.Update();

        public IState CurrentChestState() => stateMachine.GetCurrentState();

        public void ChangeState(ChestState state)
        {
            if (CurrentChestState() != stateMachine.GetStates()[state])
                stateMachine.ChangeState(state);
        }

        public void SetChestStateText(string state) => chestView.SetChestStateText(state);

        public void SetRemainingTime(float time) => chestModel.SetRemainingTime(time);

        public void DisableTimerText() => chestView.DisableTimerText();

        public void Collect()
        {
            ChangeState(ChestState.Collected);
            RemoveChest();

            int gems = GetCollectedGems();
            int coins = GetCollectedCoins();

            unlockSelectionUIController.SetCollectedValues(gems, coins);
            GameService.Instance.eventService.OnRewardCollectedEvent.InvokeEvent(gems, coins);
        }

        private int GetCollectedCoins()
        {
            ChestScriptableObject.ChestRewards rewards = chestModel.GetChestRewards(
                chestModel.GetCurrentChestType());

            int minimumCoins = rewards.minCoin;
            int maximumCoins = rewards.maxCoin;

            int collectedCoins = UnityEngine.Random.Range(minimumCoins, maximumCoins);
            return collectedCoins;
        }

        private int GetCollectedGems()
        {
            ChestScriptableObject.ChestRewards rewards = chestModel.GetChestRewards(
                chestModel.GetCurrentChestType());

            int minimumGems = rewards.minGems;
            int maximumGems = rewards.maxGems;

            int collectedGems = UnityEngine.Random.Range(minimumGems, maximumGems);
            return collectedGems;
        }

        public void RemoveChest()
        {
            GameService.Instance.chestService.ReturnChestToPool(this);
            chestView.gameObject.SetActive(false);
        }

        public void EnableChest() => chestView.gameObject.SetActive(true);
    }
}
