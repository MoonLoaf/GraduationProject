using System;
using Core;
using TMPro;
using Tower.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeCard : ClickableButton
    {
        public Action OnUpgradePurchased;
        
        [SerializeField] private Sprite _filledStarImage;
        [SerializeField] private Sprite _emptyStarImage;
        [SerializeField] private Image[] _stars;
        [SerializeField] private Image _lock;
        [SerializeField] private TMP_Text _upgradeNameText;
        [SerializeField] private TMP_Text _upgradeDescription;
        [SerializeField] private TMP_Text _upgradeCostText;
        public UpgradePath Path;
    
        [SerializeField] private Image _upgradeImage;
        public TowerUpgradeManager TowerToUpgrade { private get; set; }
        private TowerUpgrade _upgrade;

        private void OnEnable()
        {
            _lock.enabled = false;
        }

        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            if(Path.IsLocked || Path.ProgressIndex == 3 || !GameManager.Instance.CanAfford(_upgrade.UpgradeCost)){return;}
        
            TowerToUpgrade.UpgradeTower(_upgrade);
            GameManager.Instance.DecrementMoney(_upgrade.UpgradeCost);
            Path.ProgressIndex++;
            OnUpgradePurchased?.Invoke();
            SetCardInfo();
        }

        private TowerUpgrade GetCurrentUpgrade()
        {
            return Path.Upgrades[Mathf.Min(Path.ProgressIndex, 2)];
        }

        private void SetStars()
        {
            for (int i = 0; i <= 2; i++)
            {
                _stars[i].sprite = i < Path.ProgressIndex ? _filledStarImage : _emptyStarImage;
            }
        }

        public void SetPathLocked()
        {
            Path.IsLocked = true;
            _lock.enabled = true;
        }

        public void SetCardInfo()
        {
            SetStars();
            _lock.enabled = Path.IsLocked;
            _upgrade = GetCurrentUpgrade();
            _upgradeNameText.text = _upgrade.UpgradeName;
            _upgradeDescription.text = FormatText(_upgrade.UpgradeDescription);
            _upgradeImage.sprite = _upgrade.UpgradeSprite;
            _upgradeCostText.text = _upgrade.UpgradeCost.ToString();
        }

        private string FormatText(string text)
        {
            return text.Replace("\\n", "\n");
        }
    }
}