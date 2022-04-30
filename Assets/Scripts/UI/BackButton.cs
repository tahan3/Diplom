using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BackButton : MonoBehaviour
    {
        private List<PanelType> availablePanels = new List<PanelType>()
        {
            PanelType.SingleplayerPanel,
            PanelType.MultiplayerPanel,
            PanelType.DashboardPanel,
            PanelType.UpgradesPanel
        };

        public void SetActive(PanelType type)
        {
            gameObject.SetActive(availablePanels.Contains(type));
        }
    }
}