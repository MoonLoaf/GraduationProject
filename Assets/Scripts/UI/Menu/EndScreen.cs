using System;
using Core;
using UnityEngine;

namespace UI.Menu
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _endScreen;

        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= GameOver;
        }

        private void GameOver(GameStats gameStats, bool win)
        {
            _endScreen.SetActive(true);
        }
    }
}
