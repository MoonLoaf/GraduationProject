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
        public List<EnemyBase> ActiveEnemies { get; private set; }

        private int _currentWave = 0;
        private Vector3 _spawnPoint;

        private bool _isWaveActive;
        public bool IsWaveActive => _isWaveActive;

        protected override void Awake()
        {
            _enemyPool = new EnemyPool();
            ActiveEnemies = new List<EnemyBase>();
            _enemyPool.Initialize();
        }

        private void Start()
        {
            _spawnPoint = LevelSpline.Instance.GetStartPositionWorldSpace();
            StartCoroutine(StartWave());
        }

        private IEnumerator StartWave()
        {
            var wave = _waves[_currentWave];
            _isWaveActive = true;

            for (int eventIndex = 0; eventIndex < wave.SpawnEvents.Count; eventIndex++)
            {
                var spawnEvent = wave.SpawnEvents[eventIndex];

                yield return StartCoroutine(TriggerSpawnEvent(spawnEvent));

                if (eventIndex < wave.SpawnEvents.Count - 1)
                {
                    yield return new WaitForSeconds(wave.SpawnEvents[eventIndex + 1].Delay);
                }
            }

            _currentWave++;
            _isWaveActive = false;
        }

        private IEnumerator TriggerSpawnEvent(Wave.SpawnEvent spawnEvent)
        {
            float startTime = Time.time;

            for (int i = 0; i < spawnEvent.Count; i++)
            {
                var type = spawnEvent.Type;
                float elapsedTime = Time.time - startTime;

                float timeToWait = spawnEvent.SpawnTickRate * (i + 1) - elapsedTime;

                if (timeToWait > 0f)
                {
                    yield return new WaitForSeconds(timeToWait);
                }

                var enemy = _enemyPool.SpawnEnemy(type, _spawnPoint);
                AddActiveEnemy(enemy);
            }
        }

        public void AddActiveEnemy(EnemyBase enemy)
        {
            ActiveEnemies.Add(enemy);
        }

        public void RemoveEnemy(EnemyBase enemy)
        {
            ActiveEnemies.Remove(enemy);
            _enemyPool.DespawnEnemy(enemy);
        }
    }
}
