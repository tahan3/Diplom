using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NetworkUIManager : UIManager
    {
        [SerializeField] public AlivePlayersDisplayer alivePlayersStatus;
    }
}
