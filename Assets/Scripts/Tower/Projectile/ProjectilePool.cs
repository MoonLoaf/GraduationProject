using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace Tower.Projectile
{
    public class ProjectilePool : Object
    {
        private ObjectPool<ProjectileBase> _projectilePool;

        public void Initialize()
        {
            _projectilePool = new ObjectPool<ProjectileBase>(
                OnProjectileCreate, OnProjectileGet, OnProjectileRelease, OnProjectileDestroy,
                false, 10, 50);
        }

        public ProjectileBase SpawnProjectile(ProjectileType type, Vector3 spawnPos, EnemyBase target, TowerBase tower)
        {
            ProjectileBase projectile = _projectilePool.Get();

            projectile.transform.position = spawnPos;
            projectile.Initialize(type, target, tower);
            projectile.gameObject.SetActive(true);

            return projectile;
        }

        public void DespawnProjectile(ProjectileBase projectile)
        {
            projectile.gameObject.SetActive(false);
            _projectilePool.Release(projectile);
        }

        private ProjectileBase OnProjectileCreate()
        {
            GameObject projectileGO = new GameObject("Projectile");
            ProjectileBase projectileComp = projectileGO.AddComponent<ProjectileBase>();
            projectileGO.SetActive(false);

            return projectileComp;
        }
        
        private void OnProjectileGet(ProjectileBase projectile)
        {
            //Nothing needed here at the moment
        }

        private void OnProjectileRelease(ProjectileBase projectile)
        {
            projectile.SetType(null);
            projectile.gameObject.SetActive(false);
        }
        
        private void OnProjectileDestroy(ProjectileBase projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}
