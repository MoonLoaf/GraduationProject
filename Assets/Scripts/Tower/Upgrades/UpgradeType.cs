using System;

namespace Tower.Upgrades
{
    [Flags]
    public enum UpgradeType
    {
        None = 0,                           // 0000 0000 - Decimal: 0
        Range = 1 << 0,                     // 0000 0001 - Decimal: 1
        AttackSpeed = 1 << 1,               // 0000 0010 - Decimal: 2
        ProjectileDamage = 1 << 2,          // 0000 0100 - Decimal: 4
        ProjectileSpeed = 1 << 3,           // 0000 1000 - Decimal: 8
        MakeProjectileExplosive = 1 << 4,   // 0001 0000 - Decimal: 16
        MakeProjectileCorrosive = 1 << 5,   // 0010 0000 - Decimal: 32
        MakeProjectilePuncture = 1 << 6,    // 0100 0000 - Decimal: 64
        
        //These need to be applied if changes to DamageType are made
        
        ExplosionRadius = 1 << 7,           // 1000 0000 - Decimal: 128
        DotDamage = 1 << 8,                 // 0001 0000 0000 - Decimal: 256
        DotTickRate = 1 << 9,               // 0010 0000 0000 - Decimal: 512
        DotAmountTicks = 1 << 10,           // 0100 0000 0000 - Decimal: 1024
        LayersToPuncture = 1 << 11,         // 1000 0000 0000 - Decimal: 2048
        MakeCamoSeeing = 1 << 12,           // 0001 0000 0000 0000 - Decimal: 4096
    }
}
