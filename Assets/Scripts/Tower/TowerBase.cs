using Enemy;
using Tower.Projectile;
using UnityEngine;
using Utility.EnemyWaveLogic;

namespace Tower
{
    public class TowerBase : ClickableObject
    {
        [SerializeField] protected TowerType _type;
        [SerializeField] private GameObject _projectilePrefab;
        
        public TowerType Type => _type;
        public ProjectilePool ProjectilePool { get; private set; }
        protected SpriteRenderer _renderer;

        protected float _attackSpeed;
        protected float _range;
        protected float _lastAttackTime;

        protected EnemyBase _target;
        

        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            ProjectilePool = new ProjectilePool();
        }

        public void Initialize(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _attackSpeed = _type.AttackSpeed;
            _range = _type.Range;
            ProjectilePool.Initialize(_projectilePrefab, 10, 25);
        }


        private void UpdateRotation()
        {
            if(_target == null) { return; }

            Vector3 enemyPos = _target.transform.position;

            Vector3 direction = (enemyPos - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle - 100f, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }


        protected virtual void FixedUpdate()
        {
            if (IsTargetOutsideRange())
            {
                _target = null;
            }
            if (!_target)
            {
                GetNewTarget();
            }
            UpdateRotation();
            
            if (!ShouldAttack()) return;
            
            Attack();
            _lastAttackTime = Time.time;
        }

        protected bool GetNewTarget()
        {
            foreach (var enemy in WaveManager.Instance.ActiveEnemies)
            {
                if(Vector2.Distance(enemy.transform.position, transform.position) >= _range) {continue;}

                _target = enemy;
                return true;
            }
            //No target found
            return false;
        }

        protected virtual void Attack()
        {
            ProjectilePool.SpawnObject(_type.TypeProjectileType, transform.position, _target, this);
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
            
            Gizmos.color = Color.red;
            if (_target != null)
            {
                Gizmos.DrawLine(transform.position, _target.transform.position);
            }
        }

        protected bool IsTargetOutsideRange()
        {
            return _target != null && Vector2.Distance(_target.transform.position, transform.position) >= _range;
        }

        protected bool ShouldAttack()
        {
            return _target != null && (Time.time - _lastAttackTime > _attackSpeed);
        }
    }
}
