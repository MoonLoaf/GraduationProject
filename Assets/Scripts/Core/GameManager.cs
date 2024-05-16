using System;
using UnityEngine;
using Utility;
using Utility.EnemyWaveLogic;

namespace Core
{
    public delegate void GameStatChangeHandlerInt(int newValue);
    public delegate void GameOverHandler(GameStats gameStats, bool win);
    [DefaultExecutionOrder(-1)]
    public class GameManager : GenericSingletonDOL<GameManager>
    {
        [SerializeField] private GameStats _startingGameStatistics;
        [SerializeField] private GameStats _curGameStatistics;

        public float TowerSellMultiplier => _curGameStatistics.SellTowerMultiplier;
        public event GameStatChangeHandlerInt OnWaveChanged;
        public event GameStatChangeHandlerInt OnMoneyChanged;
        public event GameStatChangeHandlerInt OnLivesChanged;

        public event Action OnWaveStart;
        public event Action OnWaveEnd;

        public event GameOverHandler OnGameOver;

        protected override void Awake()
        {
            base.Awake();
            _curGameStatistics = ScriptableObject.CreateInstance<GameStats>();
            _curGameStatistics.SetGameStatistics(_startingGameStatistics);
        }

        private void Start()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            OnWaveChanged?.Invoke(_curGameStatistics.CurrentWave);
            OnMoneyChanged?.Invoke(_curGameStatistics.Money);
            OnLivesChanged?.Invoke(_curGameStatistics.Lives);
        }

        public void BeginNewWave()
        {
            if (_curGameStatistics.CurrentWave >= WaveManager.Instance.MaxWaveAmount) return;
            
            _curGameStatistics.CurrentWave++;
            OnWaveStart?.Invoke();
            OnWaveChanged?.Invoke(_curGameStatistics.CurrentWave);
        }

        public void WaveEnd(int waveEndReward)
        {
            OnWaveEnd?.Invoke();
            IncrementMoney(waveEndReward);
        }

        public void IncrementMoney(int amount)
        {
            _curGameStatistics.Money += amount;
            OnMoneyChanged?.Invoke(_curGameStatistics.Money);
        }

        public void DecrementMoney(int amount)
        {
            _curGameStatistics.Money -= amount;
            OnMoneyChanged?.Invoke(_curGameStatistics.Money);
        }

        public void OnEnemyCompleteTrack(int damage)
        {
            DecrementLives(damage);
        }
        
        public void GameOver(bool win)
        {
            OnGameOver?.Invoke(_curGameStatistics, win);
        }

        private void DecrementLives(int livesToLose)
        {
            _curGameStatistics.Lives -= livesToLose;
            OnLivesChanged?.Invoke(_curGameStatistics.Lives);
            if (_curGameStatistics.Lives <= 0)
            {
                GameOver(false);
            }
        }

        public bool CanAfford(int cost)
        {
            return cost <= _curGameStatistics.Money;
        }
    }
}
