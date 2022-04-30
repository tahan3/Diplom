using System;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMoneyStatus : MonoBehaviour
    {
        [SerializeField] private Text goldCoinsText;

        private void Awake()
        {
            PlayFabManager.Instance.OnGetUserInventory += GetCurrency;
        }

        private void GetCurrency(GetUserInventoryResult result)
        {
            goldCoinsText.text = result.VirtualCurrency[CurrencyType.GC.ToString()].ToString();
        }
    }
}
