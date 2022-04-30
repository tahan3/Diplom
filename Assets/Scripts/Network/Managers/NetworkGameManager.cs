using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Maps;
using Photon.Pun;
using Photon.Realtime;
using Player;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Network.Managers
{
    public class NetworkGameManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        [SerializeField] private MapsManager mapsManager;
        [SerializeField] private UIController uiController;
        [SerializeField] private NetworkUIManager uIManager;
        
        public GameObject playerPrefab;

        private PlayerManager playerManager;

        private List<int> deadActors;
        private readonly byte PlayerDiedEventCode = 1;

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void Awake()
        {
            deadActors = new List<int>();
        }

        private void Start()
        {
            GameMap map = mapsManager.GetRandomMap(PhotonNetwork.CurrentRoom.MaxPlayers);

            Instantiate(map.gameObject);

            var currentPlayer = PhotonNetwork.CurrentRoom.Players.Values.Single(x =>
                x.UserId == PhotonNetwork.LocalPlayer.UserId);

            Vector3 position = map.spawnPoints[currentPlayer.ActorNumber - 1].position;

            playerManager = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity)
                .GetComponent<PlayerManager>();

            playerManager.Init(uiController, () => SetLose(PhotonNetwork.LocalPlayer.ActorNumber));
        }

        private void SetWin()
        {
            PlayFabManager.Instance.SendLeaderboard(PlayFabEnums.LeaderboardType.MMR, 1);
        }

        private void SetLose(int actorNumber)
        {
            object content = actorNumber;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(PlayerDiedEventCode, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
        {
            SetLose(PhotonNetwork.LocalPlayer.ActorNumber);
        }
        
        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;

            if (eventCode == PlayerDiedEventCode)
            {
                int deadActorNumber = (int) photonEvent.CustomData;
                int roomMaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
                int currentActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

                deadActors.Add(deadActorNumber);

                uIManager.alivePlayersStatus.Set((byte) (roomMaxPlayers - deadActors.Count));
                
                if (deadActorNumber == currentActorNumber)
                {
                    uIManager.SetGameOver(false);
                }

                if (deadActors.Count == roomMaxPlayers - 1 && !deadActors.Contains(currentActorNumber))
                {
                    uIManager.SetGameOver(true);
                    SetWin();
                }
            }
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}
