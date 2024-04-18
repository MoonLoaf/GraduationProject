using Tower.Projectile;
using Tower.Upgrades;
using UnityEngine;

namespace Tower
{
    [CreateAssetMenu(fileName = "TowerType", menuName = "TowerType")]
    public class TowerType : ScriptableObject
    {
        [Header("Visual")] 
        [SerializeField] private Sprite _sprite;

        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private TowerUpgradeCollection _upgrades;
        
        [Header("Tower Stats")]
        [SerializeField] private int _cost;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _range;
        
        private TowerUpgradeCollection _upgradesInstance;
    
        public float AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }

        public float Range
        {
            get => _range;
            set => _range = value;
        }

        public int Cost => _cost;
        public Sprite TypeSprite => _sprite;
        public ProjectileType TypeProjectileType => _projectileType;
        public TowerUpgradeCollection UpgradePaths
        {
            get
            {
                if (_upgradesInstance == null)
                {
                    _upgradesInstance = Instantiate(_upgrades);
                }
                return _upgradesInstance;
            }
        }
    }
}