using Core;
using Tower.Hero;
using UnityEngine;

namespace UI.Buttons
{
    public class HeroPreviewCreatorButton : TowerPreviewCreatorButton
    {
        protected override void OnEnable()
        {
            _imageRef.sprite = _typeToSpawn.TypeSprite;
            _costText.text = _typeToSpawn.Cost.ToString();
            SetTraitImg();
            UIEventManager.Instance.OnSettingsPressed += () => { _button.interactable = false; };
            UIEventManager.Instance.OnGameContinue += () => { _button.interactable = true; };
            UIEventManager.Instance.HeroSoldEvent += OnHeroSold;
        }

        public override void OnClickInteraction()
        {
            base.OnClickInteraction();
            if (UIEventManager.Instance.IsPreviewActive || !GameManager.Instance.CanAfford(_typeToSpawn.Cost)) {return;}

            GameObject heroPreviewObject = Instantiate(_towerPreviewPrefab, Vector3.zero, Quaternion.identity);
            HeroPreview heroPreviewComp = heroPreviewObject.GetComponent<HeroPreview>();
            heroPreviewComp.SetType(_typeToSpawn);
            _button.interactable = false;
            
            UIEventManager.Instance.StartTowerPreview();
        }

        private void OnHeroSold(Hero hero)
        {
            _button.interactable = true;
        }
    }
}
