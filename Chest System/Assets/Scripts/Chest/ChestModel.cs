using System;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace ChestSystem.Chest
{
    public class ChestModel
    {
        private ChestController chestController;
        private List<ChestScriptableObject> chestScriptableObject;
        private Dictionary<ChestScriptableObject.ChestType, Sprite> chestImages;
        public ChestModel(ChestController chestController, List<ChestScriptableObject> chestScriptableObject)
        {
            this.chestController = chestController;
            this.chestScriptableObject = chestScriptableObject;

            InitializeChestImages();
        }

        private void InitializeChestImages()
        {
            chestImages = new Dictionary<ChestScriptableObject.ChestType, Sprite>();

            foreach (var chestSO in chestScriptableObject)
            {
                if (!chestImages.ContainsKey(chestSO.chestType))
                {
                    Sprite chestImage = chestSO.chestImage;
                    chestImages.Add(chestSO.chestType, chestImage);
                }
            }
        }

        public Sprite GetChestImage(ChestScriptableObject.ChestType chestType)
        {
            if(chestImages.ContainsKey(chestType))
            {
                return chestImages[chestType];
            }
            return null;
        }
    }
}
