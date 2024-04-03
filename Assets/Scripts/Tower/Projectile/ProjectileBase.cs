using Enemy;
using UnityEngine;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        private TowerBase _tower;
        public ProjectileType Type => _type;

        private Vector3 _direction;
        private bool _shouldMove = false;
        private float _currentLifetime = 0;

        private SpriteRenderer _renderer;
        
        private void Awake()
        {
            gameObject.AddComponent<CircleCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(ProjectileType type, Vector3 dir, TowerBase tower)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _direction = dir;
            _tower = tower;
            _shouldMove = true;
        }

        public void SetType(ProjectileType type)
        {
            if(_type != null){return;}
            _type = type;
        }

        private void Update()
        {
            if(!_shouldMove){return;}
            
            Vector3 movement = _direction * (_type.MoveSpeed * Time.deltaTime);

            transform.position += movement;

            _currentLifetime += Time.deltaTime;

            if (_currentLifetime < _type.Lifetime) return;
            
            _currentLifetime = 0;
            _tower.ProjectilePool.DespawnProjectile(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Enemy")) return;

            if ((_type.DamageType & DamageType.Explosive) != 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(other.transform.position, _type.ExplosionRadius);
        
                foreach (Collider2D collider in colliders)
                {
                    EnemyBase enemy = collider.GetComponent<EnemyBase>();
                    
                    if (enemy == null) continue;
                    
                    enemy.TakeDamage(_type);
                    _tower.ProjectilePool.DespawnProjectile(this);
                }
            }
            else
            {
                EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
                if (enemy == null) return;
                
                enemy.TakeDamage(_type);
                _tower.ProjectilePool.DespawnProjectile(this);
            }
        }
    }
}
