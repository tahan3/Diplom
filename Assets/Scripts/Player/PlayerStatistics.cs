using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerStatistics
    {
        public int goldCoins;

        public string nickname;

        public Dictionary<UpgradeType, float> PlayersUpgrades { get; set; }

        public PlayerStatistics()
        {
            PlayersUpgrades = SetDefaultUpgrades();
        }
        
        private Dictionary<UpgradeType, float> SetDefaultUpgrades()
        {
            return new Dictionary<UpgradeType, float>()
            {
                {UpgradeType.HP, 100},
                {UpgradeType.Damage, 5},
                {UpgradeType.Reload, 2},
                {UpgradeType.MoveSpeed, 5},
                {UpgradeType.BulletSpeed, 5},
                {UpgradeType.ShieldHP, 10}
            };
        }
    }
}