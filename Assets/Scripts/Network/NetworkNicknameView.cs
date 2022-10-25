using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Network
{
    public class NetworkNicknameView : MonoBehaviour, IOnEventCallback
    {
        [SerializeField] private Text nicknameText;
        [SerializeField] private PhotonView photonView;
        
        private void Start()
        { 
            object content = PlayFabManager.Instance.PlayerStatistics.nickname;
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)EventCodes.PlayerDiedEventCode, content, raiseEventOptions, SendOptions.SendReliable);
        }
        
        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;

            if (eventCode == (byte)EventCodes.NicknameEventCode)
            {
                if (!photonView.IsMine)
                {
                    nicknameText.text = photonEvent.CustomData.ToString();
                }
            }
        }
    }
}
