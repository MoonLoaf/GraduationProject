using UnityEngine;

namespace Helpers
{
    public static class Layers
    {
        public static int Towers => LayerMask.NameToLayer("Tower");
        public static int Enemies => LayerMask.NameToLayer("Enemy");
        public static int Projectiles => LayerMask.NameToLayer("Projectile");
        
        public static bool IsOnLayer(this Collider2D collider, int layer)
        {
            return collider.gameObject.layer.Equals(layer);
        }

        public static bool IsNotOnLayer(this Collider2D collider, int layer)
        {
            return !collider.gameObject.layer.Equals(layer);
        }
    }
}
