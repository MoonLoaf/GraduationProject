using UnityEngine;
using Utility;

namespace Core
{
    public delegate void GameStatChangeHandlerInt(int newValue);
    public delegate void GameEventHandler();
    public class GameManager : GenericSingleton<GameManager>
    {
        [SerializeField] private GameStats _startingGameStatistics;
        [SerializeField] private GameStats _curGameStatistics;
        
        public event GameStatChangeHandlerInt OnWaveChanged;
        public event GameStatChangeHandlerInt OnMoneyChanged;
        public event GameStatChangeHandlerInt OnLivesChanged;

        public event GameEventHandler OnWaveStart;
        public event GameEventHandler OnWaveEnd;

        protected override void Awake()
        {
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

        private void DecrementLives(int livesToLose)
        {
            _curGameStatistics.Lives -= livesToLose;
            OnLivesChanged?.Invoke(_curGameStatistics.Lives);
            if (_curGameStatistics.Lives <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            throw new System.NotImplementedException();
        }

        public bool CanAfford(int cost)
        {
            return cost <= _curGameStatistics.Money;
        }
    }
}
