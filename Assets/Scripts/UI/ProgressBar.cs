using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] public Image progressArea;

        public virtual void Fill(float value)
        {
            progressArea.fillAmount += value;
        }

        public virtual void Fill(float startValue, float endValue, float duration, Action callback = null)
        {
            gameObject.SetActive(true);
            
            progressArea.DOKill();
            progressArea.fillAmount = startValue;
            progressArea.DOFillAmount(endValue, duration).SetEase(Ease.Linear).onComplete += () => callback?.Invoke();
        }

        public void Refill()
        {
            progressArea.DOKill();
            progressArea.fillAmount = 0f;
        }

        public void Set(float value)
        {
            progressArea.DOKill();
            progressArea.fillAmount = value;
        }
    }
}
