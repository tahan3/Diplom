using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class DialogWindow : MonoBehaviour
    {
        [SerializeField] private Text descriptionText;
        [SerializeField] private Button button;

        public void Init(string description, UnityAction yesButtonCallback = null)
        {
            descriptionText.text = description;

            if (yesButtonCallback != null)
            {
                button.onClick.AddListener(yesButtonCallback);
            }

            button.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
