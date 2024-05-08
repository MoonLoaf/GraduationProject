using Audio;
using UnityEngine;

namespace Tower.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileType", menuName = "ProjectileType")]
    public class ProjectileType : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _moveSpeed = 2;
        [SerializeField] private float _lifetime = 1;
        
        [Header("Enemy Flags and Modifiers")]
        [SerializeField] private DamageType _damageType;
        
        // Additional fields for explosion radius, DOT damage, DOT tick rate, and layers to puncture
        [SerializeField] private float _explosionRadius;
        [SerializeField] private int _dotDamage;
        [SerializeField] private float _dotTickRate;
        [SerializeField] private int _dotAmountOfTicks;
        [SerializeField] private int _layersToPuncture;
        [SerializeField] private Sound _onFiredSound;
        
    
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }

        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        public Sprite TypeSprite => _sprite;
        public float Lifetime => _lifetime;

        public Sound OnFiredSound => _onFiredSound;
        public DamageType DamageType
        {
            get => _damageType;
            set => _damageType = value;
        }

        public float ExplosionRadius
        {
            get => _explosionRadius;
            set => _explosionRadius = value;
        }

        public int DotDamage
        {
            get => _dotDamage;
            set => _dotDamage = value;
        }

        public float DotTickRate
        {
            get => _dotTickRate;
            set => _dotTickRate = value;
        }

        public int DotAmountOfTicks
        {
            get => _dotAmountOfTicks;
            set => _dotAmountOfTicks = value;
        }

        public int LayersToPuncture
        {
            get => _layersToPuncture;
            set => _layersToPuncture = value;
        }
    }
}
