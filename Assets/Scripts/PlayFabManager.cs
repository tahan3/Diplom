using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Newtonsoft.Json;
using Player;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance { get; private set; }
    
    public StatisticsManager StatisticsManager { get; private set; }
    public UpgradesManager UpgradesManager { get; private set; }
    public MoneyManager MoneyManager { get; private set; }
    public LoginManager LoginManager { get; private set; }
    public PlayerStatistics PlayerStatistics { get; private set; }

    public Action OnNicknameChange;
    
    private void Awake()
    {
        #region Singleton

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        #endregion

        InitManagers();
        
        PlayerStatistics = new PlayerStatistics();

        LoginManager.OnLogin += MoneyManager.GetVirtualCurrency;
        LoginManager.OnLogin += UpgradesManager.GetPlayerUpgrades;

        UpgradesManager.OnGetUserUpgrades += GetUpgradesFirstLogin;

        LoginManager.Login();
    }

    private void InitManagers()
    {
        StatisticsManager = new StatisticsManager();
        UpgradesManager = new UpgradesManager();
        MoneyManager = new MoneyManager();
        LoginManager = new LoginManager();
    }
    
    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
    
    public void UpdatePlayersName(string newName)
    {
        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = newName
        };
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUserNameUpdateSuccess, OnError);
    }

    private void OnUserNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        PlayerStatistics.nickname = result.DisplayName;
        
        OnNicknameChange?.Invoke();
    }
    
    private void GetUpgradesFirstLogin(GetUserDataResult result)
    {
        UpgradesManager.OnGetUserUpgrades -= GetUpgradesFirstLogin;
        
        if (result.Data.ContainsKey(PlayFabEnums.PlayerDataType.Upgrades.ToString()))
        {
            PlayerStatistics.PlayersUpgrades =
                JsonConvert.DeserializeObject<Dictionary<UpgradeType, float>>(result
                    .Data[PlayFabEnums.PlayerDataType.Upgrades.ToString()].Value);
        }
        else
        {
            UpgradesManager.SavePlayerUpgrades(PlayerStatistics.PlayersUpgrades);
        }
    }
}
