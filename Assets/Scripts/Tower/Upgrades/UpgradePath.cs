namespace Tower.Upgrades
{
    public struct UpgradePath
    {
        public TowerUpgrade[] Path;
        public int ProgressIndex;
        public bool IsLocked;
    }
}
