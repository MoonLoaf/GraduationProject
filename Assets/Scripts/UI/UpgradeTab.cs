using System.Collections;
using System.Collections.Generic;
using Tower;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void TowerPressedHandler(TowerBase tower);
    public class UpgradeTab : MonoBehaviour
    {
        public static TowerPressedHandler OnTowerPressed;
        [SerializeField] private Image _mainImage;
        [SerializeField] private float _targetXValue;
        [SerializeField] private float  _fadeDuration = 1;
        [SerializeField] private List<UpgradeCard> _cards;
        [SerializeField] private Button _BackButton;
        
        private float _initialXValue;
        private bool _moving;

        private void Awake()
        {
            _BackButton.onClick.AddListener(ButtonFadeFunc);
        }

        private void OnEnable() 
        {
            OnTowerPressed += SetCards;
        }

        private void SetCards(TowerBase tower)
        {
            Debug.Log(tower.name);
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].TowerToUpgrade = tower;
                int pathIndex = tower.CurrentType.UpgradePaths.Paths[i].ProgressIndex;
                var upgrade = tower.CurrentType.UpgradePaths.Paths[i].Path[pathIndex];
                _cards[i].SetCardInfo(upgrade);
            }

            if(_moving){return;}

            StartCoroutine(Fade(true));
        }

        private void ButtonFadeFunc()
        {
            if(_moving){return;}

            StartCoroutine(Fade(false));
        }

        private IEnumerator Fade(bool fadeIn)
        {
            _moving = true;

            float startValue = fadeIn ? _initialXValue : _targetXValue;
            float endValue = fadeIn ? _targetXValue : _initialXValue;

            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                float t = elapsedTime / _fadeDuration;
                _mainImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(startValue, endValue, t), _mainImage.rectTransform.anchoredPosition.y);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _mainImage.rectTransform.anchoredPosition = new Vector2(endValue, _mainImage.rectTransform.anchoredPosition.y);
            _moving = false;
        }
    }
}
