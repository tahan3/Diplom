using System;
using Photon.Pun;
using UnityEngine;

namespace Damage
{
    public class NetworkHealth : Health, IPunObservable, IUpgrade
    {
        [SerializeField] private PhotonView photonView;

        private new void Awake()
        {
            if (photonView.IsMine)
            {
                InitUpgrade();
            }
            
            base.Awake();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentHealth);
            }
            else
            {
                float health = (float) stream.ReceiveNext();
                
                if (Math.Abs(CurrentHealth - health) > 0f)
                {
                    GetDamage(CurrentHealth - health);
                }
            }
        }
        
        public void InitUpgrade()
        {
            maxHealth = PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[UpgradeType.HP];

            CurrentHealth = maxHealth;
        }
    }
}
