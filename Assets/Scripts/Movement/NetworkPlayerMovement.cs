using System;
using Photon.Pun;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PhotonRigidbodyView))]
    public class NetworkPlayerMovement : PlayerMovement
    {
        private PhotonView photonView;

        private void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        public override void HandleMovement()
        {
            if (photonView.IsMine)
            {
                base.HandleMovement();
            }
        }
        
    }
}
