using System;

namespace Tower.Projectile
{
    [Flags]
    public enum DamageType
    {
        Normal = 0,
        Explosive = 1 << 0,
        Corrosive = 1 << 1,
        Puncture = 1 << 2
    }
}
