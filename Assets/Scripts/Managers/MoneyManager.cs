using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Managers
{
    public class MoneyManager
    {
        public Action<GetUserInventoryResult> OnGetUserInventory;

        public int CurrentMoney { get; private set; }
        
        public void AddVirtualCurrency(CurrencyType currencyType, int value)
        {
            var request = new AddUserVirtualCurrencyRequest()
            {
                VirtualCurrency = currencyType.ToString(),
                Amount = value
            };

            PlayFabClientAPI.AddUserVirtualCurrency(request, OnUserCurrencyChanged, OnError);
        }
    
        public void SubVirtualCurrency(CurrencyType currencyType, int value)
        {
            var request = new SubtractUserVirtualCurrencyRequest
            {
                VirtualCurrency = currencyType.ToString(),
                Amount = value
            };
        
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnUserCurrencyChanged, OnError);
        }

        private void OnUserCurrencyChanged(ModifyUserVirtualCurrencyResult obj)
        {
            GetVirtualCurrency();
        }

        public void GetVirtualCurrency()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnError);
        }

        private void OnGetUserInventorySuccess(GetUserInventoryResult result)
        {
            CurrentMoney = result.VirtualCurrency[CurrencyType.GC.ToString()];
        
            OnGetUserInventory?.Invoke(result);
        }
        
        private void OnError(PlayFab.PlayFabError playFabError)
        {
            Debug.LogError(playFabError.GenerateErrorReport());
        }
    }
}
