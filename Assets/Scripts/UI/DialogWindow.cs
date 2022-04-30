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

        public void Init(string description, UnityAction yesButtonCallback)
        {
            descriptionText.text = description;
            button.onClick.AddListener(yesButtonCallback);
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
