using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyPool : GenericPool<EnemyBase>
    {
        public EnemyBase SpawnObject(EnemyType type, Vector3 spawnPos)
        {
            EnemyBase enemy = _pool.Get();
            
            enemy.transform.position = spawnPos;
            enemy.SetEnemyType(type);
            enemy.Initialize();
            enemy.gameObject.SetActive(true);

            return enemy;
        }
    }
}



