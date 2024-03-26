using UnityEngine;
using UnityEngine.Pool;

namespace Enemy
{
    public class EnemyPool : Object
    {
        private ObjectPool<EnemyBase> _enemyPool;

        public void Initialize()
        {
            _enemyPool = new ObjectPool<EnemyBase>(
                OnEnemyCreate, OnEnemyGet, OnEnemyRelease, OnEnemyDestroy, false, 40, 500);
        }

        public EnemyBase SpawnEnemy(EnemyType type, Vector3 spawnPos)
        {
            EnemyBase enemy = _enemyPool.Get();
            
            enemy.transform.position = spawnPos;
            enemy.SetEnemyType(type);
            enemy.Initialize();
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        public void DespawnEnemy(EnemyBase enemy)
        {
            _enemyPool.Release(enemy);
        }

        private EnemyBase OnEnemyCreate()
        {
            GameObject enemyObject = new GameObject("Enemy");
            EnemyBase enemy = enemyObject.AddComponent<EnemyBase>();
            enemyObject.SetActive(false);
    
            return enemy;
        }

        private void OnEnemyGet(EnemyBase enemy)
        {
            //Nothing needed here at the moment
        }

        private void OnEnemyRelease(EnemyBase enemy)
        {
            enemy.SetEnemyType(null);
            enemy.gameObject.SetActive(false);
        }

        private void OnEnemyDestroy(EnemyBase enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}



