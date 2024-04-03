using Enemy;
using Tower.Projectile;
using UnityEngine;
using Utility.EnemyWaveLogic;

namespace Tower
{
    public class TowerBase : MonoBehaviour
    {
        [SerializeField] private TowerType _type;
        public TowerType Type => _type;

        public ProjectilePool ProjectilePool { get; private set; }

        private float _attackSpeed;
        private int _damage;
        private float _range;
        private float _lastAttackTime;

        private EnemyBase _target;
        
        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
            ProjectilePool = new ProjectilePool();
        }

        public void Initialize(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _attackSpeed = _type.AttackSpeed;
            _range = _type.Range;
            ProjectilePool.Initialize();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        private void UpdateRotation()
        {
            if(_target == null) { return; }

            Vector3 enemyPos = _target.transform.position;

            Vector3 direction = (enemyPos - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.back);

            var rotation = transform.rotation;
            Vector3 currentEulerAngles = rotation.eulerAngles;

            Quaternion targetZRotation = Quaternion.Euler(currentEulerAngles.x, currentEulerAngles.y, targetRotation.eulerAngles.z);

            Quaternion lerpedRotation = Quaternion.Lerp(rotation, targetZRotation, 0.1f);

            rotation = lerpedRotation;
            transform.rotation = rotation;
        }

        private void GetNewTarget()
        {
            foreach (var enemy in WaveManager.Instance.ActiveEnemies)
            {
                if(Vector2.Distance(enemy.transform.position, transform.position) >= _range) {continue;}

                _target = enemy;
                break;
            }
        }

        private void FixedUpdate()
        {
            if (_target != null && Vector2.Distance(_target.transform.position, transform.position) >= _range)
            {
                _target = null;
            }
            if (_target == null)
            {
                GetNewTarget();
            }
            UpdateRotation();
            if (_target != null && Time.time - _lastAttackTime >= _attackSpeed)
            {
                Attack();
                _lastAttackTime = Time.time;
            }
        }

        private void Attack()
        {
            if (_target != null)
            {
                Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;

                ProjectilePool.SpawnProjectile(_type.TypeProjectileType, transform.position, directionToTarget, this);
            }
        }
    }
}
