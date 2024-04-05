using System;
using Enemy;
using UnityEngine;
using UnityEngine.Splines;
using Utility;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        private TowerBase _tower;
        public ProjectileType Type => _type;

        private EnemyBase _target;
        private Vector3 _targetPos;
        private Vector3 _direction;
        private Vector3 _spawnPos;
        private bool _shouldMove = false;
        private float _currentLifetime = 0;

        private bool _hitTarget;

        private SpriteRenderer _renderer;
        private CircleCollider2D _collider;
            
        
        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _collider = gameObject.GetComponent<CircleCollider2D>();
        }

        public void Initialize(ProjectileType type, EnemyBase target, TowerBase tower)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _target = target;
            _tower = tower;
            _spawnPos = transform.position;
            _shouldMove = true;
            _direction = (_target.transform.position - transform.position).normalized;
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

            if (!_hitTarget)
            {
                // Calculate direction towards the target if not already hit
                Vector3 direction = (_target.transform.position - transform.position).normalized;
                transform.Translate(direction * (_type.MoveSpeed * Time.deltaTime), Space.World);
            }
            else
            {
                // Continue moving in the original direction after hitting the target
                transform.Translate(_direction * (_type.MoveSpeed * Time.deltaTime), Space.World);
            }

            _currentLifetime += Time.deltaTime;

            if (_currentLifetime < _type.Lifetime) return;
            
            _currentLifetime = 0;
            _tower.ProjectilePool.DespawnObject(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collided");
            if(!other.gameObject.CompareTag("Enemy")){return;}

            if (!_hitTarget)
            {
                _direction = (transform.position - _spawnPos).normalized;
                _hitTarget = true;
            }
            
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
                if (_target == null) return;
            
                _target.TakeDamage(_type);
            }
        }
    }
}
