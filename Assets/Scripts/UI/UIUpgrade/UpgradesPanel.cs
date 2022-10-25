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
        [SerializeField] private RectTransform itemsParent;

        private List<UIUpgradeItem> currentItems;

        private void OnEnable()
        {
            currentItems = new List<UIUpgradeItem>();
            
            SpawnItems();
        }

        private void SpawnItems()
        {
            for (var i = 0; i < data.items.Count; i++)
            {
                UIUpgradeItem item = Instantiate(upgradeItemPrefab, itemsParent);
                var i1 = i;
                item.Init(() => BuyUpgrade(data.items[i1], item));
                item.UpdateView(data.items[i1], data.items[i1].CalculateLvl(
                    PlayFabManager.Instance.UpgradesManager.GetDefaultUpgrades()[data.items[i1].type],
                    PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[data.items[i1].type]));
                
                currentItems.Add(item);
            }
        }
        
        private void BuyUpgrade(UpgradeItem item, UIUpgradeItem uiItem)
        {
            int lvl = item.CalculateLvl(
                PlayFabManager.Instance.UpgradesManager.GetDefaultUpgrades()[item.type],
                PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[item.type]);

            if (lvl >= item.maxLvl) return;

            int cost = item.CalculateCost(lvl);

            DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.OptionalWindow,
                "Do you want to buy a " + item.name + " upgrade for " + cost + " coins?",
                () =>
                {
                    if (PlayFabManager.Instance.MoneyManager.CurrentMoney >= cost)
                    {
                        PlayFabManager.Instance.MoneyManager.SubVirtualCurrency(CurrencyType.GC, cost);
                        PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[item.type] += item.addPerLevel;
                        uiItem.UpdateView(item, lvl + 1);
                    }
                    else
                    {
                        DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.WarningWindow,
                            "Not enough money");
                    }
                });
        }

        private void DeleteAll()
        {
            for (var i = 0; i < currentItems.Count; i++)
            {
                Destroy(currentItems[i].gameObject);
            }
            
            currentItems.Clear();
        }
        
        private void OnDisable()
        {
            PlayFabManager.Instance.UpgradesManager.SavePlayerUpgrades(PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades);
            DeleteAll();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            PlayFabManager.Instance.UpgradesManager.SavePlayerUpgrades(PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades);
        }
    }
}
