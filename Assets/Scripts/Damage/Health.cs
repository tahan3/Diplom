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

        public float CurrentHealth { get; protected set; }

        protected void Awake()
        {
            progressBar.Set(1f);
        }

        public void GetDamage(float damage)
        {
            float currentFill = CurrentHealth / maxHealth;

            progressBar.Fill(currentFill, currentFill - damage / 100f, 0.5f);
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
