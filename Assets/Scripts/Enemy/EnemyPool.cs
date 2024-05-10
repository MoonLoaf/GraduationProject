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
            enemy.gameObject.SetActive(true);
            enemy.Initialize(type);

            return enemy;
        }
    }
}



