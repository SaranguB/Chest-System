using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.Chest
{
    [CreateAssetMenu(fileName = "new chest", menuName ="chest")]
    public class ChestScriptableObject : ScriptableObject
    {
        public Sprite chestImage;
        public ChestType chestType;
        public ChestRewards chestRewards;
        public ChestTimer chestTimer;
        public float chestGeneratingChance;

        [Serializable]
        public class ChestTimer
        {
            public int timeInMinutes;
        }

        [Serializable]
        public class ChestRewards
        {
            public int minCoin;
            public int maxCoin;
            public int minGems;
            public int maxGems;
        }

        [Serializable]
        public enum ChestType
        {
            Common,
            Rare,
            Epic,
            Legendary
        }
    }
}
