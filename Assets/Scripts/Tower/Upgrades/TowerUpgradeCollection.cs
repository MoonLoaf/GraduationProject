using UnityEngine;

namespace Tower.Upgrades
{
    [CreateAssetMenu(fileName = "TowerUpgradeCollection", menuName = "TowerUpgradeCollection")]
    public class TowerUpgradeCollection : ScriptableObject
    {
        public UpgradePath[] Paths;
    }
}
