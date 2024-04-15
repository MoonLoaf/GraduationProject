using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameStatistics", menuName = "Game Stats")]
    public class GameStats : ScriptableObject
    {
        [SerializeField] private int _money;
        [SerializeField] private int _lives;
        [SerializeField] private int _currentWave;
        [Range(0.1f, 1f)]
        [SerializeField] private float _sellTowerMultiplier;
        
        private int _enemiesPopped;
        
        public int Money
        {
            get => _money;
            set => _money = Mathf.Clamp(value, 0, 999999);
        }

        public int Lives
        {
            get => _lives;
            set => _lives = value;
        }
        
        public int CurrentWave
        {
            get => _currentWave;
            set => _currentWave = Mathf.Max(0, value);
        }

        public float SellTowerMultiplier
        {
            get => _sellTowerMultiplier;
            set => _sellTowerMultiplier = Mathf.Clamp(value, 0.1f, 1f);
        }

        public int NumEnemiesPopped
        {
            get => _enemiesPopped;
            set => _enemiesPopped = Mathf.Max(0, value);
        }
        
        public void SetGameStatistics(GameStats newGameStats)
        {
            Money = newGameStats.Money;
            Lives = newGameStats.Lives;
            SellTowerMultiplier = newGameStats.SellTowerMultiplier;
        }
    }
}