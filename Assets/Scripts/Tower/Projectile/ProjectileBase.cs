using System;
using Enemy;
using UnityEngine;
using Utility.Math;

namespace Tower.Projectile
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] private ProjectileType _type;
        private TowerBase _tower;
        public ProjectileType Type => _type;

        private EnemyBase _target;
        private Vector3 _targetPos;
        private bool _shouldMove = false;
        private float _currentLifetime = 0;

        private SpriteRenderer _renderer;
        
        private void Awake()
        {
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(ProjectileType type, EnemyBase target, TowerBase tower)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _target = target;
            _targetPos = target.transform.position;
            _tower = tower;
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

            transform.position = EasingFunctions.LerpWithEase(transform.position, _targetPos, _type.MoveSpeed * Time.deltaTime, Ease.EaseOut);

            if (Vector3.Distance(transform.position, _targetPos) < 0.1f)
            {
                OnCollision(transform.position);
            }
            
            _currentLifetime += Time.deltaTime;

            if (_currentLifetime < _type.Lifetime) return;
            
            _currentLifetime = 0;
            _tower.ProjectilePool.DespawnProjectile(this);
        }

        private void OnCollision(Vector3 collisionPoint)
        {
            if ((_type.DamageType & DamageType.Explosive) != 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPoint, _type.ExplosionRadius);
        
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
                if (_target == null) return;
                
                _target.TakeDamage(_type);
                _tower.ProjectilePool.DespawnProjectile(this);
            }
        }
    }
}
