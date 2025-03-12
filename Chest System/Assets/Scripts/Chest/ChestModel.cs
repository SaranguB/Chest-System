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
        private Dictionary<ChestScriptableObject.ChestType, float> chestTimer;
        private Dictionary<ChestScriptableObject.ChestType, ChestScriptableObject.ChestRewards> chestRewards;
        private float remainingTime;

        private ChestScriptableObject.ChestType currentChestType;
        public ChestModel(ChestController chestController, List<ChestScriptableObject> chestScriptableObject)
        {
            this.chestController = chestController;
            this.chestScriptableObject = chestScriptableObject;

            InitializeChestImages();
            InitializeChestTypeChance();
            InitializeChestTimer();
            InitializeChesetRewards();
        }

        private void InitializeChesetRewards()
        {
            chestRewards = new Dictionary<ChestScriptableObject.ChestType, ChestScriptableObject.ChestRewards>();

            foreach (var chestSO in chestScriptableObject)
            {
                if (!chestRewards.ContainsKey(chestSO.chestType))
                {
                    chestRewards[chestSO.chestType] = chestSO.chestRewards;
                }
            }
        }

        private void InitializeChestTimer()
        {
            chestTimer = new Dictionary<ChestScriptableObject.ChestType, float>();
            foreach (var chestSO in chestScriptableObject)
            {
                if (!chestTimer.ContainsKey(chestSO.chestType))
                {
                    chestTimer[chestSO.chestType] = chestSO.chestTimer;
                }
            }
        }

        public ChestScriptableObject.ChestRewards GetChestRewards(ChestScriptableObject.ChestType chestType)
        {
            if (chestRewards.ContainsKey(chestType))
            {
                return chestRewards[chestType];
            }
            return null;
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

        public float GetChestTimer()
        {
            if (chestTimer.ContainsKey(currentChestType))
            {
                return chestTimer[currentChestType];
            }
            return 0;
        }
        public void SetCurrentChestType(ChestScriptableObject.ChestType chestType)
        {
            currentChestType = chestType;
        }

        public ChestScriptableObject.ChestType GetCurrentChestType()
        {
            return currentChestType;
        }

        public void SetRemainingTime(float time)
        {
            remainingTime = time;
        }
        public float GetRemainingTime()
        {
            return remainingTime;
        }
    }
}
