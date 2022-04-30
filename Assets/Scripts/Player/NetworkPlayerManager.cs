using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class NetworkPlayerManager : PlayerManager
    {
        [SerializeField] public PhotonView photonView;
        [SerializeField] private Text nicknameText;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                nicknameText.text = PhotonNetwork.LocalPlayer.NickName;
            }
        }
    }
}
