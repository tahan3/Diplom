using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Damage
{
    public class NetworkRocket : ADamager, IPunObservable, IUpgrade
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private Rigidbody rigidbody;

        private void OnEnable()
        {
            var transform1 = transform;
            explosionParticle.transform.position = transform1.position;
            explosionParticle.transform.SetParent(transform1);
        }

        private void Awake()
        {
            OnTrigger += StopAllCoroutines;
            OnTrigger += PlayParticleOnCollision;
            OnTrigger += () => gameObject.SetActive(false);

            InitUpgrade();
        }

        public override void SetDamage(IGetDamager getDamager)
        {
            getDamager.GetDamage(damageValue);
        }

        public override void Move()
        {
            StartCoroutine(Movement());
        }

        private IEnumerator Movement()
        {
            Vector3 direction = transform.forward;

            while (enabled)
            {
                rigidbody.velocity = direction * moveSpeed;
                yield return Time.fixedTime;
            }
        }

        private void PlayParticleOnCollision()
        {
            explosionParticle.transform.SetParent(null);
            explosionParticle.Play();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(gameObject.activeSelf);
                stream.SendNext(rigidbody.position);
                stream.SendNext(rigidbody.rotation);
                stream.SendNext(rigidbody.velocity);
            }
            else
            {
                gameObject.SetActive((bool) stream.ReceiveNext());
                
                rigidbody.position = (Vector3) stream.ReceiveNext();
                rigidbody.rotation = (Quaternion) stream.ReceiveNext();
                rigidbody.velocity = (Vector3) stream.ReceiveNext();

                float lag = Mathf.Abs((float) (PhotonNetwork.Time - info.timestamp));
                rigidbody.position += rigidbody.velocity * lag;
            }
        }

        public void InitUpgrade()
        {
            damageValue = PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[UpgradeType.Damage];
            moveSpeed = PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[UpgradeType.BulletSpeed];
        }
    }
}
