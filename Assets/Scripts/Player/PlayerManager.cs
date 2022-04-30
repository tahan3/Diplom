using System;
using Damage;
using Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private AShooter shootManager;
        [SerializeField] private Health healthManager;

        public void Init(UIController controller, UnityAction OnDieCallback)
        {
            playerMovement.joystick = controller.leftJoystick;
            controller.actionButton.onClick.AddListener(shootManager.Shoot);

            healthManager.OnDie.AddListener(OnDieCallback);
        }
    }
}
