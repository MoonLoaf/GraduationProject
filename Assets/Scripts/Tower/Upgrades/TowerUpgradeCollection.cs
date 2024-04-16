using UnityEngine;

namespace Tower.Upgrades
{
    [CreateAssetMenu(fileName = "TowerUpgradeCollection", menuName = "TowerUpgradeCollection")]
    public class TowerUpgradeCollection : ScriptableObject
    {
        public TowerUpgrade[] Path1;
        public TowerUpgrade[] Path2;
        public TowerUpgrade[] Path3;
    }
}
