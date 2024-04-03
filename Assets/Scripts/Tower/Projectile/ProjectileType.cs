using UnityEngine;

namespace Tower.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileType", menuName = "ProjectileType")]
    public class ProjectileType : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _lifetime;
        
        [Header("Enemy Flags and Modifiers")]
        [SerializeField] private DamageType _damageType;
        
        // Additional fields for explosion radius, DOT damage, DOT tick rate, and layers to puncture
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _dotDamage;
        [SerializeField] private float _dotTickRate;
        [SerializeField] private int _layersToPuncture;
    
        public int Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public Sprite TypeSprite => _sprite;
        public float Lifetime => _lifetime;
        public DamageType DamageType => _damageType;        
        public float ExplosionRadius => _explosionRadius;
        public float DotDamage => _dotDamage;
        public float DotTickRate => _dotTickRate;
        public int LayersToPuncture => _layersToPuncture;
    }
}
