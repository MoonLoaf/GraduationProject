using System;
using Core;
using UnityEngine;

namespace UI.Menu
{
    public class EndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _parentGo;
        
        private void OnEnable()
        {
            GameManager.Instance.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameOver -= GameOver;
        }

        private void GameOver(GameStats gameStats)
        {
            _parentGo.SetActive(true);
        }
    }
}
