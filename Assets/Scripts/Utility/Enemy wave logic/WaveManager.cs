using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : GenericSingleton<WaveManager>
{
    private EnemyPool _enemyPool;
    [SerializeField] private List<Wave> _waves = new();
    private int _currentWave = 0;
    private Vector3 _spawnPoint;

    protected override void Awake()
    {
        _enemyPool = new EnemyPool();
        _enemyPool.Initialize();
    }

    private void Start()
    {
        _spawnPoint = LevelSpline.Instance.GetLevelSpline()[0].Position;
        StartWave();
    }

    public void StartWave()
    {
        int eventIndex = 0;
        var waveEvents = _waves[_currentWave].SpawnEvents;
        int enemyCount = waveEvents[eventIndex].Count;
        
        //TODO: If there is a spawnEvent at this time
        foreach (var spawnEvent in waveEvents)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var type = spawnEvent.Type;
                float time = spawnEvent.SpawnTickRate;
                StartCoroutine(TriggerSpawnEvent(type, time));
            }
        }
    }

    private IEnumerator TriggerSpawnEvent(EnemyType type, float time)
    {
        EnemyBase newEnemy = _enemyPool.SpawnEnemy(type, _spawnPoint);
        yield return new WaitForSeconds(time);
    }
}
