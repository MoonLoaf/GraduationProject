using Core;
using TMPro;
using Tower;
using Tower.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeCard : ClickableButton
    {
        [SerializeField] private Sprite _filledStarImage;
        [SerializeField] private Sprite _emptyStarImage;
        [SerializeField] private Image[] _stars;
        [SerializeField] private Image _lock;
        [SerializeField] private TMP_Text _upgradeNameText;
        [SerializeField] private TMP_Text _upgradeDescription;
        [SerializeField] private TMP_Text _upgradeCostText;
        public UpgradePath Path { private get; set; }
    
        [SerializeField] private Image _upgradeImage;
        public TowerBase TowerToUpgrade { private get; set; }
        private TowerUpgrade _upgrade;

        private void OnEnable()
        {
            _lock.enabled = false;
        }

        public override void OnClickInteraction()
        {
            if(!GameManager.Instance.CanAfford(_upgrade.UpgradeCost) || Path.ProgressIndex == 3){return;}
        
            TowerToUpgrade.GetComponent<TowerUpgradeManager>().UpgradeTower(_upgrade);
            GameManager.Instance.DecrementMoney(_upgrade.UpgradeCost);
            Path.ProgressIndex++;
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
                Debug.Log(i);
            }
        }

        public void SetCardInfo()
        {
            SetStars();
            _upgrade = GetCurrentUpgrade();
            _upgradeNameText.text = _upgrade.UpgradeName;
            _upgradeDescription.text = _upgrade.UpgradeDescription;
            _upgradeImage.sprite = _upgrade.UpgradeSprite;
            _upgradeCostText.text = _upgrade.UpgradeCost.ToString();
        }
    }
}