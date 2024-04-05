using Enemy;
using UnityEngine;
using Utility;

namespace Tower.Projectile
{
    public class ProjectilePool : GenericPool<ProjectileBase>
    {
        public void Initialize(GameObject prefab, int initialCapacity, int maxCapacity)
        {
            InitializePool(prefab, initialCapacity, maxCapacity);
        }

        public ProjectileBase SpawnObject(ProjectileType type, Vector3 spawnPos, EnemyBase target, TowerBase tower)
        {
            ProjectileBase projectile = _pool.Get();

            projectile.transform.position = spawnPos;
            projectile.Initialize(type, target, tower);
            projectile.gameObject.SetActive(true);

            return projectile;
        }
    }
}
