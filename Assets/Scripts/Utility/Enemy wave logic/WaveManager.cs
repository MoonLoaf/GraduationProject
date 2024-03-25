using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Utility.EnemyWaveLogic
{
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
            StartCoroutine(StartWave());
        }

        private IEnumerator StartWave()
        {
            var wave = _waves[_currentWave];

            for (int eventIndex = 0; eventIndex < wave.SpawnEvents.Count; eventIndex++)
            {
                var spawnEvent = wave.SpawnEvents[eventIndex];

                yield return StartCoroutine(TriggerSpawnEvent(spawnEvent));

                if (eventIndex < wave.SpawnEvents.Count - 1)
                {
                    yield return new WaitForSeconds(wave.SpawnEvents[eventIndex + 1].Delay);
                }
            }

            _currentWave++;;
        }

        private IEnumerator TriggerSpawnEvent(Wave.SpawnEvent spawnEvent)
        {
            for (int i = 0; i < spawnEvent.Count; i++)
            {
                var type = spawnEvent.Type;
                float time = spawnEvent.SpawnTickRate * i;

                yield return new WaitForSeconds(time);

                _enemyPool.SpawnEnemy(type, _spawnPoint);
            }
        }
    }
}
