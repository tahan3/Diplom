using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIUpgrade
{
    public class UIUpgradeItem : MonoBehaviour
    {
        public Text nameText;
        public ProgressBar progressBar;
        public Button buyButton;


        public void Init(UpgradeItem item, Action callback)
        {
            nameText.text = item.name;
            buyButton.onClick.AddListener(() => callback?.Invoke());
        }
    }
}
