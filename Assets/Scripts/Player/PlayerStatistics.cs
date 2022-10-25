using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatistics
    {
        public string nickname;

        public Dictionary<UpgradeType, float> PlayersUpgrades { get; set; }

        public PlayerStatistics(Dictionary<UpgradeType, float> upgrades)
        {
            PlayersUpgrades = upgrades;
        }
    }
}