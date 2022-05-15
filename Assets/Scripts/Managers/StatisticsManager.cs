using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class StatisticsManager
{
    public Action OnUpdateLeaderboard;
    
    public Action<GetLeaderboardResult> OnGetLeaderboard;
    
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

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
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
}
