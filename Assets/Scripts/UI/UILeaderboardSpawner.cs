using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILeaderboardSpawner : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem leaderboardPrefab;
        [SerializeField] private RectTransform itemsParent;
        
        private List<LeaderboardItem> currentItems = new List<LeaderboardItem>();

        private float itemHeight = 165f;

        private void OnEnable()
        {
            ShowStatistics(PlayFabEnums.LeaderboardType.MMR);
        }

        private void ShowStatistics(PlayFabEnums.LeaderboardType type)
        {
            SpawnItems(type);
        }
        
        private void SpawnItems(PlayFabEnums.LeaderboardType leaderboardTypeType)
        {
            if (currentItems.Count > 0)
            {
                DeleteItems();
            }
            
            PlayFabManager.Instance.OnGetLeaderboard += GetLeaderBoardItems;
            PlayFabManager.Instance.GetLeaderboard(leaderboardTypeType, 0, 20);
        }

        private void GetLeaderBoardItems(GetLeaderboardResult result)
        {
            PlayFabManager.Instance.OnGetLeaderboard -= GetLeaderBoardItems;

            foreach (var item in result.Leaderboard)
            {
                LeaderboardItem leaderboardItem = Instantiate(leaderboardPrefab, itemsParent);
                leaderboardItem.Init(item.Position + 1, item.DisplayName, item.StatValue);

                currentItems.Add(leaderboardItem);
            }

            itemsParent.sizeDelta = new Vector2(itemsParent.sizeDelta.x, itemHeight * currentItems.Count);
        }
        
        private void DeleteItems()
        {
            for (var i = 0; i < currentItems.Count; i++)
            {
                Destroy(currentItems[i].gameObject);
            }
            
            currentItems.Clear();
        }

        private void OnDisable()
        {
            DeleteItems();
        }
    }
}
