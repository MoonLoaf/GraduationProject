using System;
using System.Collections.Generic;
using Tower.Projectile;
using UnityEngine;

namespace Tower.Upgrades
{
    public class TowerUpgradeManager : MonoBehaviour
    {
        public TowerUpgradeCollection UpgradePaths { get; private set; }
        private TowerBase _tower;
        private Dictionary<UpgradeType, Action<TowerUpgrade>> _upgradeHandlers;
        private static UpgradeType[] _upgradeTypes;

        private void Awake()
        {
            _upgradeHandlers = new Dictionary<UpgradeType, Action<TowerUpgrade>>
            {
                [UpgradeType.None] = _ => { },
                [UpgradeType.Range] = ApplyRangeUpgrade,
                [UpgradeType.AttackSpeed] = ApplyAttackSpeedUpgrade,
                [UpgradeType.ProjectileDamage] = ApplyProjectileDamageUpgrade,
                [UpgradeType.ProjectileSpeed] = ApplyProjectileSpeedUpgrade,
                [UpgradeType.MakeProjectileExplosive] = ApplyExplosiveUpgrade,
                [UpgradeType.MakeProjectileCorrosive] = ApplyCorrosiveUpgrade,
                [UpgradeType.MakeProjectilePuncture] = ApplyPunctureUpgrade,
                [UpgradeType.ExplosionRadius] = ApplyExplosionRadiusUpgrade,
                [UpgradeType.DotDamage] = ApplyDotDamageUpgrade,
                [UpgradeType.DotTickRate] = ApplyDotTickRateUpgrade,
                [UpgradeType.DotAmountTicks] = ApplyDotAmountTicksUpgrade,
                [UpgradeType.LayersToPuncture] = ApplyLayersToPunctureUpgrade
            };
            if (_upgradeTypes == null)
            {
                InitializeUpgradeTypesCache();
            }
        }
        
        private static void InitializeUpgradeTypesCache()
        {
            _upgradeTypes = (UpgradeType[])Enum.GetValues(typeof(UpgradeType));
        }

        public void Initialize(TowerUpgradeCollection paths, TowerBase tower)
        {
            UpgradePaths = paths;
            _tower = tower;
        }

        public void UpgradeTower(TowerUpgrade upgrade)
        {
            foreach (UpgradeType upgradeType in _upgradeTypes)
            {
                if (!upgrade.Type.HasFlag(upgradeType)) continue;
                if (!_upgradeHandlers.TryGetValue(upgradeType, out Action<TowerUpgrade> upgradeHandler)) continue;
                upgradeHandler?.Invoke(upgrade);
            }
        }

        private void ApplyRangeUpgrade(TowerUpgrade upgrade)
        {
            float newRange = _tower.CurrentType.Range + upgrade.RangeUpgrade;
            _tower.UpdateRange(newRange);
        }

        private void ApplyAttackSpeedUpgrade(TowerUpgrade upgrade)
        {
            _tower.CurrentType.AttackSpeed -= upgrade.AttackSpeedUpgrade;
        }

        private void ApplyProjectileDamageUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().Damage += upgrade.ProjectileDamageUpgrade;
        }

        private void ApplyProjectileSpeedUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().MoveSpeed += upgrade.ProjectileSpeedUpgrade;
        }

        private void ApplyExplosiveUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DamageType = DamageType.Explosive;
        }

        private void ApplyCorrosiveUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DamageType = DamageType.Corrosive;
        }

        private void ApplyPunctureUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DamageType = DamageType.Puncture;
        }

        private void ApplyExplosionRadiusUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().ExplosionRadius += upgrade.ExplosionRadiusUpgrade;
        }

        private void ApplyDotDamageUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DotDamage += upgrade.DotDamageUpgrade;
        }

        private void ApplyDotTickRateUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DotTickRate -= upgrade.DotTickRate;
        }

        private void ApplyDotAmountTicksUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().DotAmountOfTicks += upgrade.DotAmountOfTicks;
        }

        private void ApplyLayersToPunctureUpgrade(TowerUpgrade upgrade)
        {
            _tower.GetProjectile().LayersToPuncture += upgrade.LayersToPuncture;
        }
    }
}
