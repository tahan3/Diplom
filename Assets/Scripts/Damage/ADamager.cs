using System;
using Movement;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Damage
{
    public abstract class ADamager : MonoBehaviour, ISetDamager, IMovement
    {
        [SerializeField] public float damageValue;
        [SerializeField] protected float moveSpeed;

        public Action OnTrigger;

        public abstract void SetDamage(IGetDamager getDamager);
        
        public abstract void Move();

        protected virtual void OnTriggerEnter(Collider collider)
        {
            OnTrigger?.Invoke();
            
            if (collider.transform.CompareTag("Player"))
            {
                SetDamage(collider.transform.GetComponent<IGetDamager>());
            }
        }
    }
}
