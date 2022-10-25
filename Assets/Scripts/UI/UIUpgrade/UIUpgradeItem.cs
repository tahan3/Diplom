using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIUpgrade
{
    public class UIUpgradeItem : MonoBehaviour
    {
        public Text nameText;
        public Text currentLvlText;
        public Text maxLvlText;
        public ProgressBar progressBar;
        public Button buyButton;
        
        public void Init(Action callback)
        {
            buyButton.onClick.AddListener(() => callback?.Invoke());
        }

        public void UpdateView(UpgradeItem item, int lvl)
        {
            nameText.text = item.name;
            maxLvlText.text = item.maxLvl.ToString();
            currentLvlText.text = lvl.ToString();
            progressBar.Set((float)lvl / item.maxLvl);

            if (item.maxLvl <= lvl)
            {
                buyButton.onClick.RemoveAllListeners();
                buyButton.gameObject.SetActive(false);
            }
        }
    }
}
