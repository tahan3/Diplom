using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace UI.UIUpgrade
{
    public class UpgradesPanel : MonoBehaviour
    {
        [SerializeField] private UpgradesScriptable data;
        [SerializeField] private UIUpgradeItem upgradeItemPrefab;

        private Dictionary<UpgradeType, int> stages;

        private List<UIUpgradeItem> currentItems;

        private void OnEnable()
        {
            if (stages == null)
            {
                stages = CalculateStages();
            }

            SpawnItems();
        }

        private void SpawnItems()
        {
            
        }

        private void BuyUpgrade(UIUpgradeItem item)
        {
            
        }

        private void DeleteAll()
        {
            
        }
        
        private Dictionary<UpgradeType, int> CalculateStages()
        {
            Dictionary<UpgradeType, int> tmp = new Dictionary<UpgradeType, int>();
            
            foreach (var upgradeItem in data.items)
            {
                
            }

            return null;
        }

        private void OnDisable()
        {
            DeleteAll();
        }
    }
}
