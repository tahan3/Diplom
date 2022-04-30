using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Player;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance { get; private set; }

    public Action OnLogin;
    
    public Action OnUpdateLeaderboard;
    
    public Action<GetLeaderboardResult> OnGetLeaderboard;

    public Action OnNicknameChange;

    public Action<GetUserInventoryResult> OnGetUserInventory;

    public Action<GetUserDataResult> OnGetUserUpgrades;

    public Action<UpdateUserDataResult> OnSavePlayerUpgrades;

    public PlayerStatistics playerStatistics;
    
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

        playerStatistics = new PlayerStatistics();

        OnLogin += GetVirtualCurrency;
        OnLogin += GetPlayerUpgrades;

        OnGetUserUpgrades += GetUpgradesFirstLogin;

        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest()
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
        };
        
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
    }

    private void OnSuccessLogin(LoginResult result)
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest {PlayFabId = result.PlayFabId},
            OnGetAccountInfoResult, OnError);

        OnLogin?.Invoke();
    }

    private void OnGetAccountInfoResult(GetAccountInfoResult result)
    {
        playerStatistics.nickname = result.AccountInfo.TitleInfo.DisplayName;
    }

    public void SendLeaderboard(PlayFabEnums.LeaderboardType leaderboardTypeType, int score)
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardTypeType.ToString(),
                    Value = score
                }
            }
        };
        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdateSuccess, OnError);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    private void OnLeaderBoardUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        OnUpdateLeaderboard?.Invoke();
    }

    public void GetLeaderboard(PlayFabEnums.LeaderboardType leaderboardTypeType, int firstIndex, int lastIndex)
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = leaderboardTypeType.ToString(),
            StartPosition = firstIndex,
            MaxResultsCount = lastIndex
        };
        
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGetSuccess, OnError);
    }

    private void OnLeaderBoardGetSuccess(GetLeaderboardResult result)
    {
        OnGetLeaderboard?.Invoke(result);
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
        playerStatistics.nickname = result.DisplayName;
        
        OnNicknameChange?.Invoke();
    }

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
        playerStatistics.goldCoins = result.VirtualCurrency[CurrencyType.GC.ToString()];
        
        OnGetUserInventory?.Invoke(result);
    }

    public void GetPlayerUpgrades()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnGetUserUpgradesSuccess, OnError);
    }

    public void SavePlayerUpgrades()
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {
                    PlayFabEnums.PlayerDataType.Upgrades.ToString(),
                    JsonConvert.SerializeObject(playerStatistics.PlayersUpgrades)
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

    private void GetUpgradesFirstLogin(GetUserDataResult result)
    {
        OnGetUserUpgrades -= GetUpgradesFirstLogin;
        
        if (result.Data.ContainsKey(PlayFabEnums.PlayerDataType.Upgrades.ToString()))
        {
            playerStatistics.PlayersUpgrades =
                JsonConvert.DeserializeObject<Dictionary<UpgradeType, float>>(result
                    .Data[PlayFabEnums.PlayerDataType.Upgrades.ToString()].Value);
        }
        else
        {
            SavePlayerUpgrades();
        }
    }
}
