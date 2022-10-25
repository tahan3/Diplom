using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuItem : MonoBehaviour
    {
        public Button button;
        public GameObject panel;
        public PanelType type;

        public void Init(Action callback)
        {
            button.onClick.AddListener(() =>
            {
                Show(callback);
            });
        }

        protected virtual void Show(Action callback = null)
        {
            callback?.Invoke();
            panel.SetActive(true);
        }
        
        public virtual void Hide()
        {
            panel.SetActive(false);
        }
    }
}