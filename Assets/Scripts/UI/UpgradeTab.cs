using System.Collections;
using System.Collections.Generic;
using Tower;
using Tower.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void TowerPressedHandler(TowerUpgradeManager towerUpgradeManager);

    public delegate void TowerInteractionHandler(bool interaction);
    public class UpgradeTab : MonoBehaviour
    {
        public static TowerPressedHandler OnTowerPressed;
        public static TowerInteractionHandler OnTowerDeselect;
        [SerializeField] private Image _mainImage;
        [SerializeField] private float _targetXValue;
        [SerializeField] private float  _fadeDuration = 1;
        [SerializeField] private List<UpgradeCard> _cards;
        [SerializeField] private Button _BackButton;
        
        private float _initialXValue;
        private bool _moving;
        private bool _visible;

        private void Awake()
        {
            _BackButton.onClick.AddListener(ButtonFadeFunc);
        }

        private void OnEnable()
        {
            _initialXValue = _mainImage.rectTransform.anchoredPosition.x;
            OnTowerPressed += SetCards;
        }

        private void SetCards(TowerUpgradeManager towerUpgradeManager)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].TowerToUpgrade = towerUpgradeManager;
                _cards[i].Path = towerUpgradeManager.UpgradePaths.Paths[i];
                _cards[i].SetCardInfo();
            }
            if(_moving || _visible){return;}

            StartCoroutine(Fade(true));
        }

        private void ButtonFadeFunc()
        {
            if(_moving){return;}

            StartCoroutine(Fade(false));
            OnTowerDeselect?.Invoke(false);
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

            _visible = fadeIn;
            _mainImage.rectTransform.anchoredPosition = new Vector2(endValue, _mainImage.rectTransform.anchoredPosition.y);
            _moving = false;
        }
    }
}
