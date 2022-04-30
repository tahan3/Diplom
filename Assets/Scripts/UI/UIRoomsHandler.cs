using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UnityEngine;

public class UIRoomsHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] private RoomListItem roomItemPrefab;

    private List<RoomListItem> currentRooms = new List<RoomListItem>();

    private void DeleteAll()
    {
        if (currentRooms != null)
        {
            for (var i = 0; i < currentRooms.Count; i++)
            {
                Destroy(currentRooms[i].gameObject);
            }
            
            currentRooms.Clear();
        }
    }
    
    private void ShowItems(List<RoomInfo> roomList)
    {
        DeleteAll();

        foreach (var room in roomList)
        {
            RoomListItem item = Instantiate(roomItemPrefab, itemsParent);

            item.Init(room.Name, room.PlayerCount, room.MaxPlayers);

            currentRooms.Add(item);
        }
    }

    public void RefreshRoomList()
    {
        PhotonNetwork.GetCustomRoomList(PhotonNetwork.CurrentLobby, "C0 BETWEEN 345 AND 475");
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ShowItems(roomList);
    }
}
