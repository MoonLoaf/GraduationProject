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

        }

        protected override void Update()
        {
            if(!ShouldAttack()){return;}
            
            Attack();
        }

        private void Attack()
        {
            for (int i = 0; i < _numProjectiles; i++)
            {
                Quaternion rotation = Quaternion.Euler(0f, _angleStep * i, 0f);
                Vector3 dir = rotation * transform.forward;

                ProjectilePool.SpawnObject(_currentProjectile, _position, dir, this);
            }

            _lastAttackTime = Time.time;
        }
    }
}
