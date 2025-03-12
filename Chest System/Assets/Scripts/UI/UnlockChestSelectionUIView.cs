using ChestSystem.Chest;
using ChestSystem.StateMachine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilis;


namespace ChestSystem.UI
{
    public class UnlockChestSelectionUIView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button unlockChestWithGemsButton;
        [SerializeField] private Button undoButton;
        [SerializeField] private Button collectButton;
        [SerializeField] private GameObject displayCollectedValues;
        [SerializeField] private GameObject checkAlreadyUnlockingPanel;

        private void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            closeButton.onClick.AddListener(DisableUnlockSelection);
        }

        public void SetUnlockChestSelection(int gemsRequiredCount, string chestType, IState currentChestState)
        {
            EnableUnlockSelection();
            SetGemsText(gemsRequiredCount);
            SetChestTypeText(chestType);
            SetButtons(currentChestState);
        }

        private void SetButtons(IState currentChestState)
        {
            startTimerButton.gameObject.SetActive(false);
            unlockChestWithGemsButton.gameObject.SetActive(false);
            undoButton.gameObject.SetActive(false);
            collectButton.gameObject.SetActive(false);
            displayCollectedValues.gameObject.SetActive(false);

            switch (currentChestState)
            {
                case LockedState:
                    startTimerButton.gameObject.SetActive(true);
                    unlockChestWithGemsButton.gameObject.SetActive(true);
                    break;

                case UnlockingState:
                    unlockChestWithGemsButton.gameObject.SetActive(true);
                    break;

                case UnlockedState:
                    undoButton.gameObject.SetActive(true);
                    collectButton.gameObject.SetActive(true);
                    break;

                case CollectedState:
                    displayCollectedValues.gameObject.SetActive(true);
                    break;

            }
        }

        private void EnableUnlockSelection()
        {
            CanvasGroupExtension.Show(canvasGroup);
        }

        public void DisableUnlockSelection()
        {
            CanvasGroupExtension.Hide(canvasGroup);
        }

        private void SetGemsText(int value)
        {
            gemsText.text = value.ToString();
        }

        private void SetChestTypeText(string value)
        {
            chestTypeText.text = value + " Chest";
        }

        public Button GetstartTimerButton()
        {
            return startTimerButton;
        }

        public Button GetUnlockChestWithGemsButton()
        {
            return unlockChestWithGemsButton;
        }

        public Button GetUndoButton()
        {
            return undoButton;
        }

        public Button GetCollectButton()
        {
            return collectButton;
        }
    }
}
