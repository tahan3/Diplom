using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Damage
{
    public class DamageManager : AShooter, IUpgrade
    {
        [SerializeField] private ADamager damagePrefab;
        [SerializeField] private Transform shootPoint;

        private bool canShoot = true;

        private List<ADamager> bullets = new List<ADamager>();
        private int index = 0;

        private void Awake()
        {
            InitUpgrade();
        
            for (var i = 0; i < 5; i++)
            {
                ADamager bullet = PhotonNetwork
                    .Instantiate(damagePrefab.gameObject.name, shootPoint.position, shootPoint.rotation)
                    .GetComponent<ADamager>();

                bullet.gameObject.SetActive(false);

                bullets.Add(bullet);
            }
        }
    
        public override void Shoot()
        {
            if (canShoot)
            {
                PreShoot();
                StartCoroutine(Reload());
            }
        }

        private void PreShoot()
        {
            if (index >= bullets.Count)
            {
                index = 0;
            }
        
            bullets[index].transform.position = shootPoint.position;
            bullets[index].transform.rotation = shootPoint.rotation;
        
            bullets[index].gameObject.SetActive(true);
            bullets[index++].Move();
        }
    
        private IEnumerator Reload()
        {
            canShoot = false;
            yield return new WaitForSeconds(reloadDuration);
            canShoot = true;
        }

        public void InitUpgrade()
        {
            reloadDuration = Convert.ToSingle(PlayFabManager.Instance.playerStatistics.PlayersUpgrades[UpgradeType.Reload]);
        }
    }
}
