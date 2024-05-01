using Core;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _winText;
        [SerializeField] private TMP_Text _roundsText;
        [SerializeField] private TMP_Text _enemiesText;
        [SerializeField] private TMP_Text _healthText;

        public void SetTexts(GameStats stats, bool win)
        {
            _winText.text = win ? "You Win!" : "You Lose!";
            _roundsText.text = "Rounds Completed: " + stats.CurrentWave;
            _enemiesText.text = "Enemies Destroyed: " + stats.NumEnemiesPopped;
            _healthText.text = "Health: " + stats.Lives;
        }
    }
}
