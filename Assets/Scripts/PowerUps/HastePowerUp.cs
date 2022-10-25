using System;
using System.Collections;
using Movement;
using UnityEngine;

namespace PowerUps
{
    public class HastePowerUp : MonoBehaviour
    {
        public float moveSpeedMultiplier = 1.5f;
        public float duration = 5f;

        private IEnumerator StartTimer(PlayerMovement playerMovement)
        {
            playerMovement.moveSpeed *= 1.5f;
            yield return new WaitForSecondsRealtime(duration);
            playerMovement.moveSpeed /= 1.5f;

            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(StartTimer(other.GetComponent<PlayerMovement>()));
            }
        }
    }
}