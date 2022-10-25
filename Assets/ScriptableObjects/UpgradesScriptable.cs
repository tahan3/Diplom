using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public struct UpgradeItem
    {
        public UpgradeType type;
        public int maxLvl;
        public string name;
        public float addPerLevel;
        public int startCost;

        public int CalculateLvl(float defaultValue, float current)
        {
            return (int)((current - defaultValue) / addPerLevel) + 1;
        }

        public int CalculateCost(int currentLvl)
        {
            return currentLvl * startCost;
        }
    }
    
    [CreateAssetMenu(fileName = "UpgradesData", menuName = "ScriptableObjects/UpgradesData", order = 1)]
    public class UpgradesScriptable : ScriptableObject
    {
        public List<UpgradeItem> items;
    }
}
