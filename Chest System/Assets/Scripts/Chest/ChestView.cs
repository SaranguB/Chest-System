using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.Chest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        [SerializeField] private Image chestImage;

        public void SetController(ChestController chestController)
        {
            this.chestController = chestController;
            SetChestImage();
        }

        private void SetChestImage()
        {
            chestImage.sprite = chestController.GetChestImage(ChestScriptableObject.ChestType.Common);
            chestImage.transform.localScale = new Vector2(.8f, .8f);
        }
    }
}
