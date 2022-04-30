using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AlivePlayersDisplayer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text playersAliveText;

        private void Awake()
        {
            playersAliveText.gameObject.SetActive(true);
            Set(PhotonNetwork.CurrentRoom.PlayerCount);
        }

        public void Set(byte currentPlayers)
        {
            playersAliveText.text = "ALIVE: " + currentPlayers + '/' +
                                    PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            Set(PhotonNetwork.CurrentRoom.PlayerCount);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
        {
            Set(PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}
