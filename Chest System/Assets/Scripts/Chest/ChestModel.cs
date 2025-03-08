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
        private Dictionary<ChestScriptableObject.ChestType, float> chestTypeChance;
        public ChestModel(ChestController chestController, List<ChestScriptableObject> chestScriptableObject)
        {
            this.chestController = chestController;
            this.chestScriptableObject = chestScriptableObject;

            InitializeChestImages();
            InitializeChestTypeChance();
        }

        private void InitializeChestTypeChance()
        {
            chestTypeChance = new Dictionary<ChestScriptableObject.ChestType, float>();

            foreach (var chestSO in chestScriptableObject)
            {
                if (!chestTypeChance.ContainsKey(chestSO.chestType))
                {
                    chestTypeChance[chestSO.chestType] = chestSO.chestGeneratingChance;
                }
            }



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
            if (chestImages.ContainsKey(chestType))
            {
                return chestImages[chestType];
            }
            return null;
        }

        public Dictionary<ChestScriptableObject.ChestType, float> GetChestTypeChance()
        {
            return chestTypeChance;
        }
    }
}
