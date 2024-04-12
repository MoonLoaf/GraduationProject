using System.Collections.Generic;
using Enemy;
using Helpers;
using Tower.Projectile;
using UnityEngine;

namespace Tower
{
    public class TowerBase : ClickableObject
    {
        [SerializeField] protected TowerType _type;
        public TowerType Type => _type;
       
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private TowerTargetPriority _targetPriority;
        [SerializeField] private EntityDetector entityDetector;
        
        public ProjectilePool ProjectilePool { get; private set; }
        protected List<EnemyBase> _enemiesInRange;
        protected List<TowerBase> _towersInRange;
        
        protected SpriteRenderer _renderer;

        protected float _attackSpeed;
        protected float _range;
        protected float _lastAttackTime;

        protected override void Awake()
        {
            base.Awake();
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            ProjectilePool = new ProjectilePool();
            _enemiesInRange = new List<EnemyBase>();
            _towersInRange = new List<TowerBase>();
        }

        public void Initialize(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _attackSpeed = _type.AttackSpeed;
            _range = _type.Range;
            _shaderController.SetDisplayRange(false);
            _shaderController.SetRange(_range);
            entityDetector.SetRange(_range);
            ProjectilePool.Initialize(_projectilePrefab, 10, 25);
            _enemiesInRange.Clear();
            _towersInRange.Clear();
            entityDetector.OnNewEnemyInRange += OnEnemyEnterRange;
            entityDetector.OnEnemyOutOfRange += OnEnemyLeaveRange;
            entityDetector.OnNewTowerInRange += OnTowerEnterRange;
            entityDetector.OnTowerOutOfRange += OnTowerLeaveRange;
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
            _lastAttackTime = Time.time;
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
            
            gameObject.transform.rotation = UpdateRotation(transform.position, targetPos);

            Vector3 dir = (targetPos - transform.position).normalized;

            ProjectilePool.SpawnObject(_type.TypeProjectileType, transform.position, dir, this);
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        protected bool ShouldAttack()
        {
            return _enemiesInRange.Count > 0 && Time.time - _lastAttackTime > _attackSpeed;
        }

        private void OnEnemyEnterRange(EnemyBase enemy)
        {
            _enemiesInRange.Add(enemy);
        }

        private void OnEnemyLeaveRange(EnemyBase enemy)
        {
            _enemiesInRange.Remove(enemy);
        }
        
        private void OnTowerEnterRange(TowerBase tower)
        {
            _towersInRange.Add(tower);
        }

        private void OnTowerLeaveRange(TowerBase tower)
        {
            _towersInRange.Remove(tower);
        }
    }
}
