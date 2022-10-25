using System;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Damage
{
    public class Health : MonoBehaviour, IGetDamager, ICurable
    {
        [Header("View")]
        [SerializeField] protected ProgressBar progressBar;

        public UnityEvent OnDie;

        protected float maxHealth;

        public float CurrentHealth;

        protected void Awake()
        {
            progressBar.Set(1f);
        }

        public virtual void GetDamage(float damage)
        {
            float currentFill = CurrentHealth / maxHealth;

            progressBar.Set(currentFill - damage / 100f);
            CurrentHealth -= damage;

            if (CurrentHealth <= 0f)
            {
                OnDie?.Invoke();
            }
        }

        public void Cure(float value)
        {
            progressBar.Fill(CurrentHealth, CurrentHealth + value, 0.5f);
            CurrentHealth += value;
        }
    }
}
