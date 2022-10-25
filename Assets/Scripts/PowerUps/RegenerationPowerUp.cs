using System;
using Damage;
using UnityEngine;

namespace PowerUps
{
    public class RegenerationPowerUp : MonoBehaviour
    {
        public float regenHealthValue = 25f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ICurable>().Cure(regenHealthValue);
                Destroy(gameObject);
            }
        }
    }
}