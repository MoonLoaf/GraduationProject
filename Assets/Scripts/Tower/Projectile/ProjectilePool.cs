using UnityEngine;
using Utility;
using Vector3 = UnityEngine.Vector3;

namespace Tower.Projectile
{
    public class ProjectilePool : GenericPool<ProjectileBase>
    {
        public ProjectileBase SpawnObject(ProjectileType type, Vector3 spawnPos, Vector3 direction, TowerBase tower)
        {
            ProjectileBase projectile = _pool.Get();

            projectile.transform.position = spawnPos;
            projectile.Initialize(type, direction, tower);
            projectile.gameObject.SetActive(true);

            return projectile;
        }
    }
}
