using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

namespace Managers
{
    public class UpgradesManager
    {
        public Action<GetUserDataResult> OnGetUserUpgrades;

        public Action<UpdateUserDataResult> OnSavePlayerUpgrades;
        
        public void GetPlayerUpgrades()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetUserUpgradesSuccess, OnError);
        }

        public void SavePlayerUpgrades(Dictionary<UpgradeType, float> playersUpgrades)
        {
            var request = new UpdateUserDataRequest()
            {
                Data = new Dictionary<string, string>()
                {
                    {
                        PlayFabEnums.PlayerDataType.Upgrades.ToString(),
                        JsonConvert.SerializeObject(playersUpgrades)
                    }
                }
            };
        
            PlayFabClientAPI.UpdateUserData(request, OnSavePlayerUpgradesSuccess, OnError);
        }

        private void OnSavePlayerUpgradesSuccess(UpdateUserDataResult result)
        {
            OnSavePlayerUpgrades?.Invoke(result);
        }

        private void OnGetUserUpgradesSuccess(GetUserDataResult result)
        {
            OnGetUserUpgrades?.Invoke(result);
        }

        private void OnError(PlayFabError error)
        {
            Debug.LogError(error.GenerateErrorReport());
        }
        
        public Dictionary<UpgradeType, float> GetDefaultUpgrades()
        {
            return new Dictionary<UpgradeType, float>()
            {
                {UpgradeType.HP, 100},
                {UpgradeType.Damage, 5},
                {UpgradeType.Reload, 2},
                {UpgradeType.MoveSpeed, 5},
                {UpgradeType.BulletSpeed, 5},
            };
        }
    }
}
