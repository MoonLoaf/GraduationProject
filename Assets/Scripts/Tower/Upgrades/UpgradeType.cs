using System;

namespace Tower.Upgrades
{
    [Flags]
    public enum UpgradeType
    {
        None = 0,
        Range = 1 << 0,
        AttackSpeed = 1 << 1,
        ProjectileDamage = 1 << 2,
        ProjectileSpeed = 2 << 2,
        MakeProjectileExplosive = 2 << 3,
        MakeProjectileCorrosive = 3 << 3,
        MakeProjectilePuncture = 3 << 4,
        
        //These need to be applied if changes to DamageType are made
        
        ExplosionRadius = 4 << 4,
        DotDamage = 4 << 5,
        DotTickRate = 5 << 5,
        DotAmountTicks = 5 << 6,
        LayersToPuncture = 6 << 6,
        MakeCamoSeeing = 6 << 7,
    }
}
