
using System;
using UnityEngine;

namespace Tower.Upgrades
{
    [Serializable]
    public struct TowerUpgrade
    {
        public string UpgradeName;
        public string UpgradeDescription;
        public int UpgradeCost;
        public Sprite UpgradeSprite;
        public UpgradeType Type;
        public bool ApplyCamoSeeing;
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
    }
}
