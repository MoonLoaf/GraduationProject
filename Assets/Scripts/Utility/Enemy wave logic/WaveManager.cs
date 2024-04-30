using System.Collections;
using System.Collections.Generic;
using Core;
using Enemy;
using UnityEngine;

namespace Utility.EnemyWaveLogic
{
    public class WaveManager : GenericSingleton<WaveManager>
    {
        private EnemyPool _enemyPool;
        [SerializeField] private GameObject _enemyPrefab;
        
        [SerializeField] private List<Wave> _waves = new();

        private int _waveIndex = 0;
        private Vector3 _spawnPoint;

        public bool IsWaveActive { get; private set; }

        protected override void Awake()
        {
            _enemyPool = new EnemyPool();
            _enemyPool.Initialize(_enemyPrefab, 50, 200);
        }

        private void Start()
        {
            _spawnPoint = LevelSpline.Instance.GetStartPositionWorldSpace();
            GameManager.Instance.OnWaveStart += StartWaveFunc;
            _enemyPool.OnActivePoolEmpty += CheckGameOver;
        }

        private void CheckGameOver()
        {
            //Only gets called when pool is empty
            if (_waveIndex >= _waves.Count)
            {
                GameManager.Instance.GameOver(true);
            }
        }

        private void StartWaveFunc()
        {
            if(IsWaveActive){return;}
            StartCoroutine(StartWave());
        }

        private IEnumerator StartWave()
        {
            IsWaveActive = true;
            Wave wave = _waves[_waveIndex];

            for (int eventIndex = 0; eventIndex < wave.SpawnEvents.Count; eventIndex++)
            {
                var spawnEvent = wave.SpawnEvents[eventIndex];

                yield return StartCoroutine(TriggerSpawnEvent(spawnEvent));

                if (eventIndex < wave.SpawnEvents.Count - 1)
                {
                    yield return new WaitForSeconds(wave.SpawnEvents[eventIndex + 1].Delay);
                }
            }
            
            GameManager.Instance.WaveEnd(wave.EndOfWaveReward);
            IsWaveActive = false;
            _waveIndex++;
        }

        private IEnumerator TriggerSpawnEvent(Wave.SpawnEvent spawnEvent)
        {
            float timePassed = 0f;

            WaitForSeconds wait = new WaitForSeconds(spawnEvent.SpawnTickRate);

            for (int i = 0; i < spawnEvent.Count; i++)
            {
                var type = spawnEvent.Type;
                float timeToWait = (i + 1) * spawnEvent.SpawnTickRate - timePassed;

                if (timeToWait > 0f)
                {
                    yield return wait;
                    timePassed += spawnEvent.SpawnTickRate;
                }

                _enemyPool.SpawnObject(type, _spawnPoint);
            }
        }

        public void DespawnEnemy(EnemyBase enemy)
        {
            _enemyPool.DespawnObject(enemy);
        }
    }
}
