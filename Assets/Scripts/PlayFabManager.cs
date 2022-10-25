using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Network;
using Newtonsoft.Json;
using Player;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UI;

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

        PlayerStatistics = new PlayerStatistics(UpgradesManager.GetDefaultUpgrades());

        LoginManager.OnLogin += MoneyManager.GetVirtualCurrency;
        LoginManager.OnLogin += UpgradesManager.GetPlayerUpgrades;
        LoginManager.OnLogin += ServerConnectionManager.Instance.Init;
        LoginManager.OnGetAccountInfoSuccess += GetNicknameFirstLogin;

        UpgradesManager.OnGetUserUpgrades += GetUpgradesFirstLogin;

        if (InternetWorker.InternetConnectionCheck())
        {
            LoginManager.Login();
        }
        else
        {
            DialogWindowsManager.Instance.CreateDialogWindow(DialogWindowType.WarningWindow, "No Internet connection!");
            StartCoroutine(LoginManager.ReloginLoop(2.5f));
        }
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

    private void GetNicknameFirstLogin(GetAccountInfoResult result)
    {
        PlayerStatistics.nickname = result.AccountInfo.TitleInfo.DisplayName;
    }
}
