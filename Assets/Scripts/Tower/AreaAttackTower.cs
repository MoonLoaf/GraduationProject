using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Tower
{
    public class AreaAttackTower : TowerBase
    {
        [SerializeField] private int _numProjectiles;
        private float _angleStep;
        private Vector3 _position;

        protected override void Start()
        {
            base.Start();
            _angleStep = 360 / _numProjectiles;
            _position = transform.position;

        }

        protected override void Update()
        {
            if(!ShouldAttack()){return;}
            
            Attack();
        }

        private void Attack()
        {
            Debug.Log("attack");
            for (int i = 0; i < _numProjectiles; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, _angleStep * i);
                Vector3 dir = rotation * transform.right;

                ProjectilePool.SpawnObject(_currentProjectile, _position, dir, this);
            }

            _lastAttackTime = Time.time;
        }
    }
}
