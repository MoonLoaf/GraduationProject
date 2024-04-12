using System;
using System.Collections;
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

        private bool _metalIntact = false;

        public int LayersRemaining { get; private set; }
        
        private int _currentLayerHealth;
        private float _moveSpeed;

        private SpriteRenderer _renderer;
        private CircleCollider2D _collider;

        private readonly Dictionary<DamageType, Action<ProjectileType>> _damageHandlers = new();
        
        //Testing
        private SplineContainer _spline;
        private float _splineLength;
        public float DistanceAlongSpline { get; private set; }
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
            _collider = gameObject.GetComponent<CircleCollider2D>();
            _spline = LevelSpline.Instance.GetLevelSplineContainer();
            _splineLength = _spline.CalculateLength();
        }

        public void Initialize()
        {
            _renderer.sprite = _type.TypeSprite;
            _currentLayerHealth = _type.HpPerLayer;
            LayersRemaining = _type.Layers;
            _moveSpeed = _type.MovementSpeed;
            if (_type.IsMetal)
            {
                _metalIntact = true;
            }
            UpdateLayerColor();
            IsActive = true;
        }

        private void OnDisable()
        {
            DistanceAlongSpline = 0;
            transform.position = _spline.EvaluatePosition(DistanceAlongSpline);
        }

        void Update()
        {
            if (!IsActive) return;
            
            MoveAlongSpline();
        }

        private void MoveAlongSpline()
        {
            DistanceAlongSpline += _moveSpeed * Time.deltaTime / _splineLength;

            transform.position = _spline.EvaluatePosition(DistanceAlongSpline);
            
            if (DistanceAlongSpline <= 1f) return;
            
            DistanceAlongSpline = 0;
            IsActive = false;
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
                DecreaseHP(projectileType.Damage);
            }
        }

        private void HandlePunctureDamage(ProjectileType projectileType)
        {
            if (_metalIntact) { return;}
            
            LayersRemaining -= projectileType.LayersToPuncture;
            _currentLayerHealth -= projectileType.Damage;
        }

        private void HandleCorrosiveDamage(ProjectileType projectileType)
        {
            if (_metalIntact) { return;}
            
            StartCoroutine(DoCorrosiveDamageOverTime(projectileType));
        }

        private void HandleExplosiveDamage(ProjectileType projectileType)
        {
            //do nothing if damagetype is not explosive
            if((projectileType.DamageType & DamageType.Explosive) == 0) { return; }

            _metalIntact = false;
            
            DecreaseHP(projectileType.Damage);
        }
        
        private IEnumerator DoCorrosiveDamageOverTime(ProjectileType projectileType)
        {
            int ticksRemaining = projectileType.DotTickAmount;

            var wait = new WaitForSeconds(projectileType.DotTickRate);
            while (ticksRemaining > 0)
            {
                yield return wait;
        
                DecreaseHP(projectileType.DotDamage);

                ticksRemaining--;
            }
        }

        private void DecreaseHP(int amount)
        {
            _currentLayerHealth -= amount;

            if (_currentLayerHealth > 0) return;
            
            if (LayersRemaining > 0)
            {
                LayersRemaining--;
                UpdateLayerColor();
                _currentLayerHealth = _type.HpPerLayer;
            }
            else
            {
                TriggerDeath();
            }
        }
        
        private void TriggerDeath()
        {
            //TODO: Effects?
            IsActive = false;
            WaveManager.Instance.RemoveEnemy(this);
        }

        public int GetTotalHealth()
        {
            return _currentLayerHealth + LayersRemaining * _type.HpPerLayer;
        }

        private void UpdateLayerColor()
        {
            int i = Mathf.Clamp(LayersRemaining, 0, _type.Layers);
            _renderer.color = _type.LayerColors[i];
        }
    }
}
