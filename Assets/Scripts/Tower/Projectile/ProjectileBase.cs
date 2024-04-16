using Enemy;
using Helpers;
using UnityEngine;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        private TowerBase _tower;
        public ProjectileType Type => _type;

        private Vector3 _targetPos;
        private Vector3 _direction;
        private Vector3 _spawnPos;
        private bool _shouldMove = false;
        private float _currentLifetime = 0;


        private SpriteRenderer _renderer;
        private CircleCollider2D _collider;
            
        
        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _collider = gameObject.GetComponent<CircleCollider2D>();
        }

        public void Initialize(ProjectileType type, Vector3 direction, TowerBase tower)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _direction = direction;
            _tower = tower;
            _spawnPos = transform.position;
            _shouldMove = true;
        }

        private void OnDisable()
        {
            _shouldMove = false;
        }

        public void SetType(ProjectileType type)
        {
            if(_type){return;}
            _type = type;
        }

        private void Update()
        {
            if(!_shouldMove){return;}

            transform.position += _direction * (_type.MoveSpeed * Time.deltaTime);

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
