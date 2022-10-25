using System;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IInput, IMoveHandler, IRotateHandler, IMovement, IUpgrade
    {
        public Joystick joystick;
        public float moveSpeed;

        private Rigidbody rigidbody;

        private Vector3 input;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            InitUpgrade();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        public void GetInput()
        {
            input = new Vector3(joystick.Horizontal * moveSpeed, 0f,
                joystick.Vertical * moveSpeed);
        }

        public virtual void HandleMovement()
        {
            GetInput();
            Move();
            HandleRotation();
        }

        public void HandleRotation()
        {
            if (IsInput())
            {
                transform.rotation = Quaternion.LookRotation(input);
            }
        }

        public void Move()
        {
            if (IsInput())
            {
                rigidbody.MovePosition(transform.position + input * Time.deltaTime);
            }
        }

        private bool IsInput()
        {
            return joystick.Direction != Vector2.zero;
        }

        public void InitUpgrade()
        {
            moveSpeed = Convert.ToSingle(PlayFabManager.Instance.PlayerStatistics.PlayersUpgrades[UpgradeType.MoveSpeed]);
        }
    }
}
