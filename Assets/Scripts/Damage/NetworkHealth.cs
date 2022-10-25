using System;
using Photon.Pun;
using UnityEngine;

namespace Damage
{
    public class NetworkHealth : Health, IUpgrade, IPunObservable
    {
        [SerializeField] private PhotonView photonView;

        private new void Awake()
        {
            if (photonView.IsMine)
            {
                InitUpgrade();
                photonView.RPC("InitHealth", RpcTarget.AllBuffered, CurrentHealth);
            }
            
            base.Awake();
        }

        public override void GetDamage(float damage)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("GetDamageRPC", RpcTarget.All, damage);
            }
        }

        [PunRPC]
        public void InitHealth(float value)
        {
            CurrentHealth = value;
            maxHealth = value;
        }
        
        [PunRPC]
        public void GetDamageRPC(float damage)
        {
            base.GetDamage(damage);
        }

        private void FixedUpdate()
        {
            progressBar.Set(CurrentHealth / maxHealth);
        }

        public void InitUpgrade()
        {
            maxHealth = PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[UpgradeType.HP];

            CurrentHealth = maxHealth;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurrentHealth);
            }
            else
            {
                CurrentHealth = (float) stream.ReceiveNext();
                
                progressBar.Set(CurrentHealth / maxHealth);
            }
        }
    }
}
