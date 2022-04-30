using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Network
{
    public class MultiplayerMenu : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputField createRoomInput;
        [SerializeField] private InputField connectRoomInput;

        [Header("Buttons")] 
        [SerializeField] private Button createRoomButton;
        [SerializeField] private Button connectRoomButton;

        private void Start()
        {
            connectRoomButton.onClick.AddListener(ConnectToRoom);
            createRoomButton.onClick.AddListener(CreateRoom);
        }

        private void ConnectToRoom()
        {
            if (!string.IsNullOrWhiteSpace(connectRoomInput.text))
            {
                ServerConnectionManager.Instance.JoinRoom(connectRoomInput.text);
            }
        }

        private void CreateRoom()
        {
            if (!string.IsNullOrWhiteSpace(createRoomInput.text))
            {
                ServerConnectionManager.Instance.CreateRoom(createRoomInput.text);
            }
        }
    }
}
