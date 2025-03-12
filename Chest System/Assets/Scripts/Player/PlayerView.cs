using System;
using TMPro;
using UnityEngine;

namespace ChestSystem.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController playerController;
        [SerializeField] private TextMeshProUGUI numberOfGemsText;
        [SerializeField] private TextMeshProUGUI numberOfCoinsText;

        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
            DisplayGemsCount();
            DisplayCoinsCount();
        }

        private void OnDisable()
        {
            playerController.Dispose();
        }

        public void DisplayGemsCount()
        {
            numberOfGemsText.text = playerController.GetGemsCount().ToString();
        }  
        
        public void DisplayCoinsCount()
        {
            numberOfCoinsText.text = playerController.GetCoinCount().ToString();
        }
    }
}
