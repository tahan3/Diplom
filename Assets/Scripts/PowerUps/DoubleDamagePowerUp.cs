using System;
using System.Collections;
using Damage;
using UnityEngine;

namespace PowerUps
{
    public class DoubleDamagePowerUp : MonoBehaviour
    {
        public float duration = 5f;

        private IEnumerator StartTime(ADamager damager)
        {
            damager.damageValue *= 2f;
            yield return new WaitForSecondsRealtime(duration);
            damager.damageValue /= 2f;

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartTime(other.GetComponent<ADamager>()));
            }
        }
    }
}