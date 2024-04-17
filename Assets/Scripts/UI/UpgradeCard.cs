using System;
using System.Collections.Generic;
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
    
    
    public TowerBase TowerToUpgrade { private get; set; }
    private TowerUpgrade _upgrade;

    private void OnEnable()
    {
        _lock.enabled = false;
    }

    public override void OnClickInteraction()
    {
        TowerToUpgrade.GetComponent<TowerUpgradeManager>().UpgradeTower(_upgrade);
    }

    public void SetCardInfo(TowerUpgrade upgrade)
    {
        _upgrade = upgrade;
    }
}