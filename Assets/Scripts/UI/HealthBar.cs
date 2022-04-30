using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class HealthBar : ProgressBar
    {
        [SerializeField] private float visibleDuration = 2f;
        
        private Coroutine currentLoop;
        
        public override void Fill(float startValue, float endValue, float duration, Action callback = null)
        {
            base.Fill(startValue, endValue, duration, callback);
        
            RestartVisibleLoop();
        }

        private void RestartVisibleLoop()
        {
            if (currentLoop != null)
            {
                StopCoroutine(currentLoop);
            }
        
            currentLoop = StartCoroutine(VisibleLoop());
        }
    
        private IEnumerator VisibleLoop()
        {
            yield return new WaitForSeconds(visibleDuration);
            gameObject.SetActive(false);
        }
    }
}
