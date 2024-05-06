using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class HealthText : MonoBehaviour
    {
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnLivesChanged += UpdateHealthText;
        }

        private void UpdateHealthText(int newValue)
        {
            _text.text = newValue.ToString();
        }
    }
}
