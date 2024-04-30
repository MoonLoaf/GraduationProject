using System;
using System.Collections;
using System.Collections.Generic;
using Core;
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

        public int LayersRemaining { get; private set; }
        private int _currentLayerHealth;

        public bool IsActive { get; set; }
        
        private SpriteRenderer _renderer;
        private CircleCollider2D _collider;

        //Testing
        private static SplineContainer _spline;

        private static float _splineLength;
        private float _moveSpeed;
        public float DistanceAlongSpline { get; private set; }
        private bool _metalIntact = false;
 
        private readonly Dictionary<DamageType, Action<ProjectileType>> _damageHandlers = new();
        private static DamageType[] _damageTypes;

        public EnemyBase()
        {
            _damageHandlers[DamageType.Explosive] = HandleExplosiveDamage;
            _damageHandlers[DamageType.Corrosive] = HandleCorrosiveDamage;
            _damageHandlers[DamageType.Puncture] = HandlePunctureDamage;
            
            if (_damageTypes == null)
            {
                InitializeDamageTypesCache();
            }
        }

        private void InitializeDamageTypesCache()
        {
            _damageTypes = (DamageType[])Enum.GetValues(typeof(DamageType));
        }

        private void Awake()
        {
            _renderer = gameObject.GetComponent<SpriteRenderer>();
            _collider = gameObject.GetComponent<CircleCollider2D>();

            if (_spline == null)
            {
                _spline = LevelSpline.Instance.GetLevelSplineContainer();
            }

            if (_splineLength == 0)
            {
                _splineLength = LevelSpline.Instance.SplineLength;
            }
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
            
            GameManager.Instance.OnEnemyCompleteTrack(_type.Damage);
            DistanceAlongSpline = 0;
            IsActive = false;
            WaveManager.Instance.DespawnEnemy(this);
        }

        public void SetEnemyType(EnemyType newType)
        {
            if(_type){return;}
            _type = newType;
        }

        public void TakeDamage(ProjectileType projectileType)
        {
            if(!IsActive){return;}

            if (projectileType.DamageType == DamageType.Normal)
            {
                DecreaseHP(projectileType.Damage);
                return;
            }
            
            foreach (DamageType damageType in _damageTypes)
            {
                if(!projectileType.DamageType.HasFlag(damageType)) continue;
                if(!_damageHandlers.TryGetValue(projectileType.DamageType, out Action<ProjectileType> damageHandler)) continue;

                damageHandler?.Invoke(projectileType);
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
            int ticksRemaining = projectileType.DotAmountOfTicks;

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
            
            if (LayersRemaining > 1)
            {
                LayersRemaining--;
                GameManager.Instance.IncrementMoney(_type.RewardPerLayerPopped);
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
            GameManager.Instance.IncrementMoney(_type.EnemyPoppedReward);
            WaveManager.Instance.DespawnEnemy(this);
        }

        public int GetTotalHealth()
        {
            return _currentLayerHealth + LayersRemaining * _type.HpPerLayer;
        }

        private void UpdateLayerColor()
        {
            int i = Mathf.Clamp(LayersRemaining - 1, 0, _type.Layers);
            _renderer.color = _type.LayerColors[i];
        }
    }
}
