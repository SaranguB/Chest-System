using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        [SerializeField] private Image chestImage;
        private Button chestButton;
        [SerializeField]private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI chestStatetext;
        private void Start()
        {
            chestButton = GetComponent<Button>();
            chestButton.onClick.AddListener(chestController.EnableUnlockSelection);
        }

        private void Update()
        {
            if(chestController!=null)
            {
                if(chestController.CurrentChestState() is UnlockingState)
                {
                    chestController.UpdateState();
                }
            }
        }

        private void OnDisable()
        {
            chestController.UnSubscribeToEvents();
        }
        public void SetController(ChestController chestController)
        {
            this.chestController = chestController;
            SetChestImage();
        }

        private void SetChestImage()
        {
            chestImage.sprite = chestController.GetChestImage(chestController.GetRandomChestType());
            chestImage.transform.localScale = new Vector2(.7f, .7f);
        }

        public void SetTimerText(float timeInSeconds)
        {
            timerText.text = chestController.FormatTime(timeInSeconds);
        }

        public void SetChestStateText(string state)
        {
            chestStatetext.text = state;
        }
    }
}
