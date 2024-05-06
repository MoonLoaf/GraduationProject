using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using Tower;
using Tower.Hero;
using Tower.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public delegate void TowerPressedHandler(TowerBase tower, TowerUpgradeManager towerUpgradeManager);

    public delegate void TowerInteractionHandler(bool interaction);

    public class UpgradeTab : MonoBehaviour
    {
        public static TowerPressedHandler OnTowerPressed;
        public static TowerInteractionHandler OnTowerDeselect;
        [SerializeField] private TMP_Text _towerNameText;
        [SerializeField] private Image _mainImage;
        [SerializeField] private float _targetXValue;
        [SerializeField] private float  _fadeDuration = 1;
        [SerializeField] private List<UpgradeCard> _cards;
        [Space] 
        [SerializeField] private TMP_Dropdown _targetSelectionDropdown;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _sellButton;

        private TowerBase _activeTower;
        private float _initialXValue;
        private bool _moving;
        private bool _visible;

        private void Awake()
        {
            _backButton.onClick.AddListener(ButtonFadeFunc);
            _sellButton.onClick.AddListener(OnTowerSell);
            _targetSelectionDropdown.onValueChanged.AddListener(OnTargetPriorityChanged);
            _targetSelectionDropdown.ClearOptions();
            
            TowerTargetPriority[] enumValues = (TowerTargetPriority[])Enum.GetValues(typeof(TowerTargetPriority));

            string[] options = new string[enumValues.Length];
            for (int i = 0; i < enumValues.Length; i++)
            {
                options[i] = enumValues[i].ToString();
            }

            _targetSelectionDropdown.AddOptions(new List<string>(options));
        }

        private void OnEnable()
        {
            _initialXValue = _mainImage.rectTransform.anchoredPosition.x;
            OnTowerPressed += SetCards;
            foreach (var card in _cards)
            {
                card.OnUpgradePurchased += CheckLocks;
            }
        }

        private void OnDisable()
        {
            OnTowerPressed -= SetCards;
            foreach (var card in _cards)
            {
                card.OnUpgradePurchased -= CheckLocks;
            }
        }

        private void OnTowerSell()
        {
            int money = Mathf.CeilToInt(_activeTower.CurrentType.Cost * GameManager.Instance.TowerSellMultiplier);
            GameManager.Instance.IncrementMoney(money);
            
            if (_activeTower.CurrentType.IsHero)
            {
                UIEventManager.Instance.HeroSoldEvent?.Invoke((Hero)_activeTower);
            }
            
            Destroy(_activeTower.transform.parent.gameObject);
            _activeTower = null;
            ButtonFadeFunc();
        }

        private void OnTargetPriorityChanged(int index)
        {
            _activeTower.SetTargetPriority((TowerTargetPriority)index);
        }

        private void SetCards(TowerBase tower, TowerUpgradeManager towerUpgradeManager)
        {
            _activeTower = tower;
            _towerNameText.text = tower.CurrentType.TowerName;
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].TowerToUpgrade = towerUpgradeManager;
                _cards[i].Path = towerUpgradeManager.UpgradePaths.Paths[i];
                _cards[i].SetCardInfo();
            }
            _targetSelectionDropdown.value = (int)_activeTower.TargetPriority;
            CheckLocks();
            if(_moving || _visible){return;}

            StartCoroutine(Fade(true));
        }

        private void CheckLocks()
        {
            int activeCount = 0;
            UpgradeCard inactiveCard = null;

            foreach (var card in _cards)
            {
                //If a path is already locked
                if (card.Path.IsLocked)
                {
                    card.SetPathLocked();
                    return;
                }

                //Check if a path should be locked
                if (card.Path.IsActive)
                {
                    activeCount++;
                }
                else
                {
                    inactiveCard = card;
                }
            }

            if (activeCount == 2 && inactiveCard != null)
            {
                inactiveCard.SetPathLocked();
            }
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
