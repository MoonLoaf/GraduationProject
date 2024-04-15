using Core;
using TMPro;
using UnityEngine;

namespace UI.Text
{
    [RequireComponent(typeof(TMP_Text))]
    public class WaveText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnWaveChanged += UpdateWaveText;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnWaveChanged -= UpdateWaveText;
        }

        private void UpdateWaveText(int newValue)
        {
            _text.text = newValue.ToString();
        }
    }
}
