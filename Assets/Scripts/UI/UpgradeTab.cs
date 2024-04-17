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
            _BackButton.onClick.AddListener(FadeOutFunc);
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

            StartCoroutine(FadeIn());
        }

        private void FadeOutFunc()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            if(_moving) { yield return null;}
            _moving = true;
            
            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                float t = elapsedTime / _fadeDuration;
                _mainImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(_targetXValue, _initialXValue, t), _mainImage.rectTransform.anchoredPosition.y);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _mainImage.rectTransform.anchoredPosition = new Vector2(_initialXValue, _mainImage.rectTransform.anchoredPosition.y);
            _moving = false;
        }

        private IEnumerator FadeIn()
        {
            if(_moving) { yield return null;}
            _moving = true;
            
            _initialXValue = _mainImage.rectTransform.anchoredPosition.x;
            float elapsedTime = 0f;

            while (elapsedTime < _fadeDuration)
            {
                float t = elapsedTime / _fadeDuration;
                _mainImage.rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(_initialXValue, _targetXValue, t), _mainImage.rectTransform.anchoredPosition.y);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _mainImage.rectTransform.anchoredPosition = new Vector2(_targetXValue, _mainImage.rectTransform.anchoredPosition.y);
            _moving = false;
        }
    }
}
