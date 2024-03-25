using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    private ObjectPool<EnemyBase> _enemyPool;

    public ObjectPool<EnemyBase> GetEnemyPool()
    {
        return _enemyPool;
    }

    private void Awake()
    {
        _enemyPool = new ObjectPool<EnemyBase>(
            OnEnemyCreate, OnEnemyGet, OnEnemyRelease, OnEnemyDestroy, false, 40, 500);
    }

    public EnemyBase SpawnEnemy(EnemyType type, Vector3 spawnPos)
    {
        EnemyBase enemy = _enemyPool.Get();
    
        if (enemy == null)
        {
            enemy = OnEnemyCreate();
        }
    
        enemy.SetEnemyType(type);
        enemy.transform.position = spawnPos;
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    private EnemyBase OnEnemyCreate()
    {
        GameObject enemyObject = new GameObject("Enemy");
        EnemyBase enemy = enemyObject.AddComponent<EnemyBase>();
    
        return enemy;
    }

    private void OnEnemyGet(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnEnemyRelease(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnEnemyDestroy(EnemyBase enemy)
    {
        Destroy(enemy.gameObject);
    }
}



