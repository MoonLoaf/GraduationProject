using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using Utility.EnemyWaveLogic;

namespace UI
{
    public class EndOfRoundTextController : MonoBehaviour
    {
        [SerializeField] private GameObject _endOfRoundUI;
        
        private TMP_Text _text;
        private WaitForSeconds _wait;
        
        private void Awake()
        {
            _text = _endOfRoundUI.GetComponentInChildren<TMP_Text>();
        }
        
        private void OnEnable()
        {
            GameManager.Instance.OnWaveEnd += UpdateText;
            GameManager.Instance.OnWaveStart += () => { _endOfRoundUI.gameObject.SetActive(false); };
            UIEventManager.Instance.OnSettingsPressed += () => { _endOfRoundUI.gameObject.SetActive(false); };
        }
        
        private void UpdateText()
        {
            string endOfWaveText = WaveManager.Instance.GetEndOfWaveText();
            if(endOfWaveText == string.Empty){return;}

            _text.text = endOfWaveText;
            _endOfRoundUI.gameObject.SetActive(true);
        }
    }
}
