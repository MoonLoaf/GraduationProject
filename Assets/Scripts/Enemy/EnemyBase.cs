using System;
using System.Collections.Generic;
using Tower.Projectile;
using UnityEngine;
using UnityEngine.Splines;
using Utility;
using Utility.EnemyWaveLogic;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyType _type;
        public EnemyType Type => _type;

        private int _layersRemaining;
        
        private int _currentLayerHealth;
        private float _moveSpeed;

        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;

        private Dictionary<DamageType, Action<ProjectileType>> _damageHandlers = new();
        
        //Testing
        private SplineContainer _spline;
        private float _splineLength;
        private float _distancePercent = 0;
        private bool _shouldMove = false;

        public EnemyBase()
        {
            _damageHandlers[DamageType.Explosive] = HandleExplosiveDamage;
            _damageHandlers[DamageType.Corrosive] = HandleCorrosiveDamage;
            _damageHandlers[DamageType.Puncture] = HandlePunctureDamage;
        }

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        void Start()
        {
            _spline = LevelSpline.Instance.GetLevelSplineContainer();
            _splineLength = _spline.CalculateLength();
        }

        public void Initialize()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentLayerHealth = _type.HpPerLayer;
            _layersRemaining = _type.Layers;
            _moveSpeed = _type.MovementSpeed;
            _shouldMove = true;
        }

        void Update()
        {
            if (_spline != null && _shouldMove)
            {
                _distancePercent += _moveSpeed * Time.deltaTime / _splineLength;

                transform.position = _spline.EvaluatePosition(_distancePercent);

                if (_distancePercent >= 1f)
                {
                    _distancePercent = 0;
                    _shouldMove = false;
                    WaveManager.Instance.RemoveEnemy(this);
                }
            }
        }

        public void SetEnemyType(EnemyType newType)
        {
            if(_type != null){return;}
            _type = newType;
        }

        public void TakeDamage(ProjectileType projectileType)
        {
            if (_damageHandlers.TryGetValue(projectileType.DamageType, out Action<ProjectileType> damageHandler))
            {
                damageHandler?.Invoke(projectileType);
            }
            else
            {
                _currentLayerHealth -= projectileType.Damage;
            }

            if (_currentLayerHealth > 0) return;
            
            if (_layersRemaining > 0)
            {
                _layersRemaining--;
                _currentLayerHealth = _type.HpPerLayer;
            }
            else
            {
                TriggerDeath();
            }
        }

        private void HandlePunctureDamage(ProjectileType projectileType)
        {
            _layersRemaining -= projectileType.LayersToPuncture;
            _currentLayerHealth -= projectileType.Damage;
        }

        private void HandleCorrosiveDamage(ProjectileType projectileType)
        {
            //TODO: Implementation
        }

        private void HandleExplosiveDamage(ProjectileType projectileType)
        {
            //TODO: Implementation
        }
        
        private void TriggerDeath()
        {
            //TODO: Effects?
            WaveManager.Instance.RemoveEnemy(this);
        }
    }
}
