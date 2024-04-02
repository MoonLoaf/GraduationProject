using Tower.Projectile;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerType", menuName = "TowerType")]
    public class TowerType : ScriptableObject
    {
        [Header("Visual")] 
        [SerializeField] private Sprite _sprite;

        [SerializeField] private ProjectileType _projectileType;
    
        [Header("Tower Stats")]
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _range;
    
        public float AttackSpeed => _attackSpeed;
        public float Range => _range;
        public Sprite TypeSprite => _sprite;
        public ProjectileType TypeProjectileType => _projectileType;
    }
}