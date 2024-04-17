using System;

namespace Tower.Upgrades
{
    [Serializable]
    public struct UpgradePath
    {
        public TowerUpgrade[] Path;
        public int ProgressIndex;
        public bool IsLocked;
    }
}
