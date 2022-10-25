using System;
using System.Collections;
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
            PlayFabManager.Instance.MoneyManager.OnGetUserInventory += GetCurrency;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            PlayFabManager.Instance.MoneyManager.GetVirtualCurrency();
        }

        private void GetCurrency(GetUserInventoryResult result)
        {
            goldCoinsText.text = result.VirtualCurrency[CurrencyType.GC.ToString()].ToString();
        }

        private void OnDisable()
        {
            PlayFabManager.Instance.MoneyManager.OnGetUserInventory -= GetCurrency;
        }
    }
}
