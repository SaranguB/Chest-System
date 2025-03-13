using ChestSystem.Chest;
using ChestSystem.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilis;
using ChestSystem.Sound;

namespace ChestSystem.UI
{
    public class UnlockChestSelectionUIView : MonoBehaviour
    {
        private bool isChestUnlockedWithGems = false;
        private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button unlockChestWithGemsButton;
        [SerializeField] private Button undoButton;
        [SerializeField] private Button collectButton;
        [SerializeField] private GameObject chestAlreadyUnlockingPanel;

        [Header("Display Collected Values")]
        [SerializeField] private GameObject displayCollectedValues;
        [SerializeField] private TextMeshProUGUI collectedGemsText;
        [SerializeField] private TextMeshProUGUI collectedCoinsText;

        private void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            closeButton.onClick.AddListener(DisableUnlockSelection);
        }

        public void SetUnlockChestSelection(int gemsRequiredCount, string chestType, IState currentChestState,
            bool isChestUnlockedWithGems)
        {
            this.isChestUnlockedWithGems = isChestUnlockedWithGems;
            EnableUnlockSelection();
            SetGemsText(gemsRequiredCount);
            SetChestTypeText(chestType);
            SetButtons(currentChestState);
        }

        private void SetButtons(IState currentChestState)
        {
            DisableGameObject(startTimerButton.gameObject);
            DisableGameObject(unlockChestWithGemsButton.gameObject);           
            DisableGameObject(undoButton.gameObject);
            DisableGameObject(collectButton.gameObject);
            DisableGameObject(displayCollectedValues.gameObject);

            switch (currentChestState)
            {
                case LockedState:
                    EnableGameObject(startTimerButton.gameObject);
                    EnableGameObject(unlockChestWithGemsButton.gameObject);
                    break;

                case UnlockingState:
                    EnableGameObject(startTimerButton.gameObject);
                    EnableGameObject(unlockChestWithGemsButton.gameObject);
                    break;

                case UnlockedState:
                    if (isChestUnlockedWithGems)
                        EnableGameObject(undoButton.gameObject);
                    EnableGameObject(collectButton.gameObject);
                    break;

                case CollectedState:
                    EnableGameObject(displayCollectedValues.gameObject);
                    break;
            }
        }

        private void EnableUnlockSelection()
            => CanvasGroupExtension.Show(canvasGroup);

        public void DisableUnlockSelection()
            => CanvasGroupExtension.Hide(canvasGroup);

        private void SetGemsText(int value)
            => gemsText.text = value.ToString();

        private void SetChestTypeText(string value)
            => chestTypeText.text = value + " Chest";

        public Button GetstartTimerButton()
            => startTimerButton;

        public Button GetUnlockChestWithGemsButton()
            => unlockChestWithGemsButton;

        public Button GetUndoButton()
            => undoButton;

        public Button GetCollectButton()
            => collectButton;

        public void EnableDisplayCollectedValues()
           => EnableGameObject(displayCollectedValues.gameObject);

        public void DisableDisplayCollectedValues()
        {
            GameService.Instance.SoundService.PlaySound(Sounds.ButtonPressedSound);
            DisableGameObject(displayCollectedValues.gameObject);
        }

        public void EnableChestAlreadyUnlockingPanel()
            => EnableGameObject(chestAlreadyUnlockingPanel.gameObject);

        public void DisableChestAlreadyUnlockingPanel()
        {
            GameService.Instance.SoundService.PlaySound(Sounds.ButtonPressedSound);
            DisableGameObject(chestAlreadyUnlockingPanel.gameObject);
        }

        private void EnableGameObject(GameObject panel)
            => panel.SetActive(true);

        private void DisableGameObject(GameObject panel) 
            => panel.SetActive(false);

        public void SetCollectedValues(int collectedGems, int collectedCoins)
        {
            collectedGemsText.text = collectedGems.ToString();
            collectedCoinsText.text = collectedCoins.ToString();
        }

        public void SetIsChestUnlockedWithGems(bool value)
            => isChestUnlockedWithGems = value;
    }
}
