using System;
using System.Collections.Generic;
using Tower.Projectile;
using UnityEngine;

namespace Tower.Upgrades
{
    public class TowerUpgradeManager : MonoBehaviour
    {
        private TowerUpgradeCollection Paths;
        private TowerBase _tower;
        private readonly Dictionary<UpgradeType, Action<TowerUpgrade>> _upgradeHandlers = new ();

        public TowerUpgradeManager()
        {
            _upgradeHandlers[UpgradeType.None] = _ => { };
            _upgradeHandlers[UpgradeType.Range] = ApplyRangeUpgrade;
            _upgradeHandlers[UpgradeType.AttackSpeed] = ApplyAttackSpeedUpgrade;
            _upgradeHandlers[UpgradeType.ProjectileDamage] = ApplyProjectileDamageUpgrade;
            _upgradeHandlers[UpgradeType.ProjectileSpeed] = ApplyProjectileSpeedUpgrade;
            _upgradeHandlers[UpgradeType.MakeProjectileExplosive] = ApplyExplosiveUpgrade;
            _upgradeHandlers[UpgradeType.MakeProjectileCorrosive] = ApplyCorrosiveUpgrade;
            _upgradeHandlers[UpgradeType.MakeProjectilePuncture] = ApplyPunctureUpgrade;
            _upgradeHandlers[UpgradeType.ExplosionRadius] = ApplyExplosionRadiusUpgrade;
            _upgradeHandlers[UpgradeType.DotDamage] = ApplyDotDamageUpgrade;
            _upgradeHandlers[UpgradeType.DotTickRate] = ApplyDotTickRateUpgrade;
            _upgradeHandlers[UpgradeType.DotAmountTicks] = ApplyDotAmountTicksUpgrade;
            _upgradeHandlers[UpgradeType.LayersToPuncture] = ApplyLayersToPunctureUpgrade;
        }
        
        public void Initialize(TowerUpgradeCollection paths, TowerBase tower)
        {
            Paths = paths;
        }

        public void UpgradeTower(TowerUpgrade upgrade)
        {
            if (_upgradeHandlers.TryGetValue(upgrade.Type, out Action<TowerUpgrade> upgradeHandler))
            {
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
