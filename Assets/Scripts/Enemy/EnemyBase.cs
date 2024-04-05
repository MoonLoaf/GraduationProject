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

        private SpriteRenderer _renderer;

        private readonly Dictionary<DamageType, Action<ProjectileType>> _damageHandlers = new();
        
        //Testing
        private SplineContainer _spline;
        private float _splineLength;
        private float _distancePercent = 0;
        private bool _shouldMove = false;
        public bool IsActive { get; set; }

        public EnemyBase()
        {
            _damageHandlers[DamageType.Explosive] = HandleExplosiveDamage;
            _damageHandlers[DamageType.Corrosive] = HandleCorrosiveDamage;
            _damageHandlers[DamageType.Puncture] = HandlePunctureDamage;
        }

        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _spline = LevelSpline.Instance.GetLevelSplineContainer();
            _splineLength = _spline.CalculateLength();
        }

        void Start()
        {
            //tag = "Enemy";
        }

        public void Initialize()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentLayerHealth = _type.HpPerLayer;
            _layersRemaining = _type.Layers;
            _moveSpeed = _type.MovementSpeed;
            IsActive = true;
            _shouldMove = true;
        }

        private void OnDisable()
        {
            _distancePercent = 0;
            transform.position = _spline.EvaluatePosition(_distancePercent);
            _shouldMove = false;
        }

        void Update()
        {
            if (!_shouldMove) return;
            
            _distancePercent += _moveSpeed * Time.deltaTime / _splineLength;

            transform.position = _spline.EvaluatePosition(_distancePercent);

            if (_distancePercent <= 1f) return;
            
            _distancePercent = 0;
            _shouldMove = false;
            WaveManager.Instance.RemoveEnemy(this);
        }

        public void SetEnemyType(EnemyType newType)
        {
            if(_type){return;}
            _type = newType;
        }

        public void TakeDamage(ProjectileType projectileType)
        {
            if(!IsActive){return;}
            
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
            IsActive = false;
            WaveManager.Instance.RemoveEnemy(this);
        }
    }
}
