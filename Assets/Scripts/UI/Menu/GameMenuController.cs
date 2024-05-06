using Core;
using UnityEngine;

namespace UI.Menu
{
    public class GameMenuController : MonoBehaviour
    {
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
       
        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += GameOver;
            UIEventManager.Instance.OnSettingsPressed += DisplaySettings;
            UIEventManager.Instance.OnGameContinue += ContinueGame;
        }

        private void GameOver(GameStats gameStats, bool win)
        {
            _endScreen.SetTexts(gameStats, win);
            _endScreen.gameObject.SetActive(true);
        }

        private void DisplaySettings()
        {
            Time.timeScale = 0;
            _settingsScreen.gameObject.SetActive(true);
        }

        private void ContinueGame()
        {
            _settingsScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
