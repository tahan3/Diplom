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
        [SerializeField] private CameraFollow cameraFollow;
        
        public GameObject playerPrefab;

        private PlayerManager playerManager;

        private List<int> deadActors = new List<int>();

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
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

            cameraFollow.enabled = true;
            cameraFollow.Target = playerManager.transform;
        }

        private void SetWin()
        {
            PlayFabManager.Instance.StatisticsManager.SendLeaderboard(PlayFabEnums.LeaderboardType.MMR, 1);
        }

        private void SetLose(int actorNumber)
        {
            object content = actorNumber;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCodes.PlayerDiedEventCode, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
        {
            SetLose(newPlayer.ActorNumber);
        }
        
        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;

            if (eventCode == (byte)EventCodes.PlayerDiedEventCode)
            {
                int deadActorNumber = (int) photonEvent.CustomData;
                int roomMaxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
                int currentActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

                deadActors.Add(deadActorNumber);

                uIManager.alivePlayersStatus.Set((byte) (roomMaxPlayers - deadActors.Count));
                
                if (deadActorNumber == currentActorNumber)
                {
                    PlayFabManager.Instance.MoneyManager.AddVirtualCurrency(CurrencyType.GC, 10);
                    uIManager.SetGameOver(false);
                }

                if (deadActors.Count == roomMaxPlayers - 1 && !deadActors.Contains(currentActorNumber))
                {
                    PlayFabManager.Instance.MoneyManager.AddVirtualCurrency(CurrencyType.GC, 100);
                    uIManager.SetGameOver(true);
                    SetWin();
                }
            }
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}
