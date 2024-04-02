using UnityEngine;

namespace Tower.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileType", menuName = "ProjectileType")]
    public class ProjectileType : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _damage;
        [SerializeField] private float _moveSpeed;
        
        [Header("Enemy Flags and Modifiers")]
        [SerializeField] private bool _isExplosive;
        [SerializeField] private bool _isCorrosive;
        [SerializeField] private bool _isPuncture;
        
        // Additional fields for explosion radius, DOT damage, DOT tick rate, and layers to puncture
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _dotDamage;
        [SerializeField] private float _dotTickRate;
        [SerializeField] private int _layersToPuncture;
    
        public int Damage => _damage;
        public float MoveSpeed => _moveSpeed;
        public Sprite TypeSprite => _sprite;
    
        public bool IsExplosive => _isExplosive;
        public bool IsCorrosive => _isCorrosive;
        public bool IsPuncture => _isPuncture;
        
        public float ExplosionRadius => _explosionRadius;
        public float DotDamage => _dotDamage;
        public float DotTickRate => _dotTickRate;
        public int LayersToPuncture => _layersToPuncture;
    }
}
