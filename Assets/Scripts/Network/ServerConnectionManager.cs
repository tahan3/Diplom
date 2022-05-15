using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Network
{
    public class ServerConnectionManager : MonoBehaviourPunCallbacks
    {
        public const string ELO_PROP_KEY = "C0";
        public const string MAP_PROP_KEY = "C1";
        
        private TypedLobby typedLobby = new TypedLobby("customSqlLobby", LobbyType.SqlLobby);
        
        public static ServerConnectionManager Instance { get; private set; }
        
        public bool InLobbyStatus { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InLobbyStatus = false;
        
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby(typedLobby);
        }

        public override void OnJoinedLobby()
        {
            InLobbyStatus = true;
            
            PhotonNetwork.LocalPlayer.NickName = PlayFabManager.Instance.PlayerStatistics.nickname;
        }

        public bool JoinRoom(string roomName)
        {
            return PhotonNetwork.JoinRoom(roomName);
        }

        public bool CreateRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions() {MaxPlayers = 2};
            roomOptions.CustomRoomProperties = new Hashtable { { ELO_PROP_KEY, 400 }, { MAP_PROP_KEY, "Map3" } };
            roomOptions.CustomRoomPropertiesForLobby = new string[] {ELO_PROP_KEY, MAP_PROP_KEY};
            return PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby);
        }
        
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
