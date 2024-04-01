using System;
using System.Collections;
using Enemy;
using UnityEngine;
using Utility.EnemyWaveLogic;

namespace Tower
{
    public class TowerBase : MonoBehaviour
    {
        [SerializeField] private TowerType _type;

        public TowerType Type => _type;

        private float _attackSpeed;
        private int _damage;
        private float _range;

        private EnemyBase _target;
        
        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _collider = gameObject.AddComponent<BoxCollider2D>();
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }

        public void Initialize(TowerType type)
        {
            _type = type;
            _renderer.sprite = _type.TypeSprite;
            _attackSpeed = _type.AttackSpeed;
            _damage = _type.Damage;
            _range = _type.Range;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        private void UpdateRotation()
        {
            if(_target == null){return;}

            Vector3 enemyPos = _target.transform.position;

            Vector3 direction = (enemyPos - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.back);

            Vector3 currentEulerAngles = transform.rotation.eulerAngles;

            Quaternion targetZRotation = Quaternion.Euler(currentEulerAngles.x, currentEulerAngles.y, targetRotation.eulerAngles.z);

            float lerpSpeed = 0.1f;
            Quaternion lerpedRotation = Quaternion.Lerp(transform.rotation, targetZRotation, lerpSpeed);

            transform.rotation = lerpedRotation;
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
            Attack();
        }

        private IEnumerator Attack()
        {
            yield return new WaitForSeconds(_attackSpeed);
            
            _target.TakeDamage(_damage);
        }
    }
}
