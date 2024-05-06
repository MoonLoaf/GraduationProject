using Core;
using TMPro;
using UnityEngine;

namespace UI.Text
{
    [RequireComponent(typeof(TMP_Text))]
    public class CurrencyText : MonoBehaviour
    {
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnMoneyChanged += UpdateCurrencyText;
        }
        
        private void UpdateCurrencyText(int newValue)
        {
            _text.text = newValue.ToString();
        }
    }
}
