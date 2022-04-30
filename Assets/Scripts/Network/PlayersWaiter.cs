using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class PlayersWaiter : MonoBehaviourPunCallbacks
    {
        [SerializeField] private List<GameObject> enableOnAllPlayersLoaded;

        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();

            SetCurrentPlayersText();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                SetCurrentPlayersText();
            }
            else
            {
                StartCurrentGame();
            }
        }

        private void SetCurrentPlayersText()
        {
            text.text = "Waiting for players... (" + PhotonNetwork.CurrentRoom.PlayerCount + '/' +
                        PhotonNetwork.CurrentRoom.MaxPlayers + ')';
        }
        
        private void StartCurrentGame()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            
            foreach (var item in enableOnAllPlayersLoaded)
            {
                item.SetActive(true);
            }
            
            Destroy(gameObject);
        }
    }
}
