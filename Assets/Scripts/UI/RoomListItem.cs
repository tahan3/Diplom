using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Network;

namespace UI
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private Text roomNameText;
        [SerializeField] private Text playersCountText;
        [SerializeField] private Button button;

        public void Init(string roomName, int currentPlayers, int maxPlayers)
        {
            roomNameText.text = roomName;
            playersCountText.text = currentPlayers.ToString() + '/' + maxPlayers.ToString();

            button.onClick.AddListener(() => ServerConnectionManager.Instance.JoinRoom(roomName));
        }
    }
}
