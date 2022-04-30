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
                callback?.Invoke();
                Show();
            });
        }

        protected virtual void Show()
        {
            panel.SetActive(true);
        }
        
        public virtual void Hide()
        {
            panel.SetActive(false);
        }
    }
}