using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
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
                photonView.Owner.NickName = PlayFabManager.Instance.PlayerStatistics.nickname;
                nicknameText.text = photonView.Owner.NickName;
                photonView.RPC("SetNicknameRPC", RpcTarget.AllBuffered, photonView.Owner.NickName);
            }
        }

        [PunRPC]
        public void SetNicknameRPC(string nickname)
        {
            nicknameText.text = nickname;
        }
    }
}
