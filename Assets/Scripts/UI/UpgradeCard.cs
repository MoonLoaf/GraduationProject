using Core;
using TMPro;
using Tower;
using Tower.Upgrades;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : ClickableButton
{
    [SerializeField] private Sprite _filledStarImage;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Image _lock;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescription;
    [SerializeField] private TMP_Text _upgradeCostText;
    
    [SerializeField] private Image _upgradeImage;
    
    public TowerBase TowerToUpgrade { private get; set; }
    private TowerUpgrade _upgrade;

    private void OnEnable()
    {
        _lock.enabled = false;
    }

    public override void OnClickInteraction()
    {
        if(!GameManager.Instance.CanAfford(_upgrade.UpgradeCost)){return;}
        
        TowerToUpgrade.GetComponent<TowerUpgradeManager>().UpgradeTower(_upgrade);
        GameManager.Instance.DecrementMoney(_upgrade.UpgradeCost);
    }

    public void SetCardInfo(TowerUpgrade upgrade)
    {
        _upgrade = upgrade;
        _upgradeNameText.text = upgrade.UpgradeName;
        _upgradeDescription.text = upgrade.UpgradeDescription;
        _upgradeImage.sprite = upgrade.UpgradeSprite;
        _upgradeCostText.text = upgrade.UpgradeCost.ToString();
    }
}