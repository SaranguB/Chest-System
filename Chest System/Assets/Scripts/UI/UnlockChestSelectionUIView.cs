using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ChestSystem.UI
{
    public class UnlockChestSelectionUIView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI gemsText;
        [SerializeField] private TextMeshProUGUI chestTypeText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button startTimerButton;
        public bool isCountingStarted = false;

        private void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            closeButton.onClick.AddListener(DisableUnlockSelection);

        }

        public void SetUnlockChestSelection(int gemsCount, string chestType)
        {
            EnableUnlockSelection();
            SetGemsText(gemsCount);
            SetChestTypeText(chestType);
        }

        private void EnableUnlockSelection()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;

           
        }

        private void DisableStartTimerButton()
        {
            startTimerButton.interactable = false;

            Image buttonImage = startTimerButton.image;
            Color color = buttonImage.color;
            color.a = 0.2f; 
            buttonImage.color = color;
        }

        public void DisableUnlockSelection()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void SetGemsText(int value)
        {
            gemsText.text = value.ToString();
        }

        private void SetChestTypeText(string value)
        {
            chestTypeText.text = value;
        }

        public Button GetstartTimerButton()
        {
            return startTimerButton;
        }
    }
}
