using Enemy;
using Helpers;
using Unity.Mathematics;
using UnityEngine;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        [SerializeField] private GameObject _explosionPrefab;
        private TowerBase _tower;

        private Vector3 _targetPos;
        private Vector3 _direction;
        private bool _shouldMove = false;
        private float _currentLifetime = 0;

        private SpriteRenderer _renderer;
            
        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
        }

        public void Initialize(ProjectileType type, Vector3 direction, TowerBase tower)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _direction = direction;
            _tower = tower;
            
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
    
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            _shouldMove = true;
        }

        private void OnDisable()
        {
            _shouldMove = false;
        }

        private void Update()
        {
            if(!_shouldMove){return;}

            transform.position += _direction * ((_type.MoveSpeed + _tower.CurrentType.Range) * Time.deltaTime);

            _currentLifetime += Time.deltaTime;

            if (_currentLifetime < _type.Lifetime) return;
            
            _currentLifetime = 0;
            _tower.ProjectilePool.DespawnObject(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.IsNotOnLayer(Layers.Enemies)){return;}

            if ((_type.DamageType & DamageType.Explosive) != 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _type.ExplosionRadius);
                GameObject explosion = Instantiate(_explosionPrefab, transform.position, quaternion.identity);
                explosion.transform.localScale = new Vector3(_type.ExplosionRadius, _type.ExplosionRadius, 1);
    
                foreach (Collider2D collider in colliders)
                {
                    EnemyBase enemy = collider.GetComponent<EnemyBase>();
                
                    if (enemy == null) continue;
                
                    enemy.TakeDamage(_type);
                    _tower.ProjectilePool.DespawnObject(this);
                }
            }
            else
            {
                EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
                enemy.TakeDamage(_type);
            }
        }
    }
}
