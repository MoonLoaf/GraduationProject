
using System;
using UnityEngine;

namespace Tower.Upgrades
{
    [Serializable]
    public struct TowerUpgrade
    {
        public UpgradeType Type;
        public float RangeUpgrade;
        public float AttackSpeedUpgrade;
        public int ProjectileDamageUpgrade;
        public float ProjectileSpeedUpgrade;

        [Space] [Header("If upgrade changes projectile damage type")]
        public float ExplosionRadiusUpgrade;
        public int DotDamageUpgrade;
        public float DotTickRate;
        public int DotAmountOfTicks;
        public int LayersToPuncture;

        public TowerUpgrade(UpgradeType type)
        {
            Type = type;
            RangeUpgrade = 0;
            AttackSpeedUpgrade = 0;
            ProjectileDamageUpgrade = 0;
            ProjectileSpeedUpgrade = 0;
            ExplosionRadiusUpgrade = 0;
            DotDamageUpgrade = 0;
            DotTickRate = 0;
            DotAmountOfTicks = 0;
            LayersToPuncture = 0;
        }
    }
}
