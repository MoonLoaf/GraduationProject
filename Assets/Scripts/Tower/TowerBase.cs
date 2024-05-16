using System.Collections.Generic;
using Audio;
using Enemy;
using Helpers;
using Tower.Projectile;
using Tower.Upgrades;
using UI;
using UnityEngine;

namespace Tower
{
    public class TowerBase : MonoBehaviour
    {
        [SerializeField] protected TowerType _initialType;
        protected TowerType _currentType;
        public TowerType CurrentType => _currentType;
        protected ProjectileType _currentProjectile;
       
        [SerializeField] private GameObject _projectilePrefab;
        private TowerTargetPriority _targetPriority;
        public TowerTargetPriority TargetPriority => _targetPriority;
        
        [SerializeField] private EntityDetector _entityDetector;
        
        public ProjectilePool ProjectilePool { get; private set; }
        protected List<EnemyBase> _enemiesInRange;
        protected List<TowerBase> _towersInRange;
        
        protected SpriteRenderer _renderer;
        private TowerUpgradeManager _towerUpgradeManager;
        private RangeShaderController _shaderController;

        protected float _lastAttackTime;

        protected virtual void Awake()
        {
            _shaderController = GetComponentInChildren<RangeShaderController>();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _towerUpgradeManager = GetComponent<TowerUpgradeManager>();
            ProjectilePool = new ProjectilePool();
            _enemiesInRange = new List<EnemyBase>();
            _towersInRange = new List<TowerBase>();
        }

        protected virtual void Start()
        {
            _entityDetector.OnNewEnemyInRange += OnEnemyEnterRange;
            _entityDetector.OnEnemyOutOfRange += OnEnemyLeaveRange;
            _entityDetector.OnNewTowerInRange += OnTowerEnterRange;
            _entityDetector.OnTowerOutOfRange += OnTowerLeaveRange;
        }

        public void Initialize(TowerType type)
        {
            _initialType = type;
            _currentType = Instantiate(type);
            _renderer.sprite = _initialType.TypeSprite;
            _currentProjectile = Instantiate(_initialType.TypeProjectileType);
            _shaderController.SetDisplayRange(false);
            _shaderController.SetRange(_initialType.Range);
            ProjectilePool.Initialize(_projectilePrefab, 10, 25);
            _towerUpgradeManager.Initialize(_currentType.UpgradePaths, this);
        }

        public void UpdateRange(float newRange)
        {
            CurrentType.Range = newRange;
            _shaderController.SetRange(CurrentType.Range);
        }

        private Quaternion UpdateRotation(Vector3 position, Vector3 targetLocation)
        {
            Vector3 towerToTarget = targetLocation - position;
            float angle = Vector3.SignedAngle(Vector3.up, towerToTarget, Vector3.forward);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return rotation;
        }
        
        protected virtual void Update()
        {
            if (!ShouldAttack()) return;
            
            Attack(GetNewTarget());
        }

        private Vector3 GetNewTarget()
        {
            switch (_targetPriority)
            {
                case TowerTargetPriority.First:
                    return GetPriorityEnemy.First(_enemiesInRange);
                case TowerTargetPriority.Last:
                    return GetPriorityEnemy.Last(_enemiesInRange);
                case TowerTargetPriority.Strongest:
                    return GetPriorityEnemy.Strongest(_enemiesInRange);
                case TowerTargetPriority.Weakest:
                    return GetPriorityEnemy.Weakest(_enemiesInRange);
                case TowerTargetPriority.Closest:
                    return GetPriorityEnemy.Closest(_enemiesInRange, transform.position);
                default:
                    return Vector3.zero;
            }
        }

        protected virtual void Attack(Vector3 targetPos)
        {
            if (targetPos == Vector3.zero) { return; }

            Vector3 position = transform.position;
            gameObject.transform.rotation = UpdateRotation(position, targetPos);

            Vector3 dir = (targetPos - position).normalized;

            ProjectilePool.SpawnObject(_currentProjectile, position, dir, this);
            AudioManager.Instance.Play(_currentProjectile.OnFiredSound.name);
            _lastAttackTime = Time.time;
        }

        public ProjectileType GetProjectile()
        {
            return _currentProjectile;
        }
        
        protected bool ShouldAttack()
        {
            return _enemiesInRange.Count > 0 && Time.time - _lastAttackTime > CurrentType.AttackSpeed;
        }

        protected virtual void OnMouseDown()
        {
            if(UIEventManager.Instance.IsPreviewActive){return;}
            UpgradeTab.OnTowerDeselect?.Invoke(false);
            UpgradeTab.OnTowerPressed?.Invoke(this, _towerUpgradeManager);  
            _shaderController.SetDisplayRange(true);
        }

        public void SetTargetPriority(TowerTargetPriority newPriority)
        {
            _targetPriority = newPriority;
        }

        private void OnEnemyEnterRange(EnemyBase enemy)
        {
            if (enemy.Type.IsCamo && !CurrentType.CamoSeeing){return;}
                
            _enemiesInRange.Add(enemy);
        }

        private void OnEnemyLeaveRange(EnemyBase enemy)
        {
            if (_enemiesInRange.Contains(enemy))
            {
                _enemiesInRange.Remove(enemy);
            }
        }
        
        private void OnTowerEnterRange(TowerBase tower)
        {
            _towersInRange.Add(tower);
        }

        private void OnTowerLeaveRange(TowerBase tower)
        {
            _towersInRange.Remove(tower);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, CurrentType.Range);
        }
    }
}
